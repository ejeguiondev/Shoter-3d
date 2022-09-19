using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.XR;

public enum ShotType
{
    manual,
    automatic
}
public class WeaponControler : MonoBehaviour
{
    [Header("General")]
    public ShotType shotType;

    public Transform boquilla;
    public GameObject flashEfect;

    public LayerMask hittabLayers;
    public GameObject balaAgujeroPrefap;
    [Header("shot parametros")]
    public float fireRange = 200;
    public float recoilForce = 4f;
    public float fireRate = 0.1f;
    public int maxAmmo = 8;
    private float lastTimeShot = Mathf.NegativeInfinity;
    public int currentAmmo { get; private set; }

    public float reloadTime = 1.5f;

    private Transform camPlayerTransform;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        // poner los parametros del evento
        EventManager.current.updateBalas.Invoke(currentAmmo, maxAmmo);
    }
    // Start is called before the first frame update
    void Start()
    {
        camPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (shotType == ShotType.automatic)
        {
            if (Input.GetButton("Fire1"))
            {
                sePuedeDisparar();
            }
        } else if (shotType == ShotType.manual)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                sePuedeDisparar();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);

        // poner los parametros del evento
        EventManager.current.updateBalas.Invoke(currentAmmo, maxAmmo);
    }

    private bool sePuedeDisparar()
    {
        if (lastTimeShot + fireRate < Time.time)
        {
            if (currentAmmo >= 1)
            {
                HandleShot();
                currentAmmo -= 1;
                return true;
            }
        }

        return false;
    }

    void HandleShot()
    {
        GameObject flashClon = Instantiate(flashEfect, boquilla.position, Quaternion.Euler(boquilla.forward));
        Destroy(flashClon, 4);

        addAnimation();

        RaycastHit hit;
        if (Physics.Raycast(camPlayerTransform.position, camPlayerTransform.forward, out hit))
        {
            Debug.Log(hit.transform.name); //te dice contra que chcocaste: pilar, pilar 2
            //Debug.Log(hit.colliderInstanceID); id de con quien coliciono si se puede detectar la id entonces sirve
            //Debug.Log(hit.transform.tag); //el tag de con quien coliciono
            GameObject balaAgujeroClone = Instantiate(balaAgujeroPrefap, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
            Destroy(balaAgujeroClone, 4f);

            GameObject objeto = GameObject.Find(hit.transform.name);
            if (hit.transform.name == "suelo")
            {
                // nada
            } else
            {
                Destroy(objeto, 1f);
            }

        }

        lastTimeShot = Time.time;

    }
    
    void addAnimation()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position -= transform.forward * (recoilForce / 50f);
    }

    IEnumerator Reload()
    {
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Recargado!");
    }

}
