using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    public float radiusAlert;
    public LayerMask playerMask;
    public bool alert;
    public bool translate;

    public int routine;
    public float counter;

    public Quaternion angulo;
    public float grado;

    public NavMeshAgent agente;
    public Transform player;

    public static int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        alert = Physics.CheckSphere(transform.position,radiusAlert,playerMask);
        if (!alert)
        {
            movementRandom();
        } else
        {
            followPlayer();
        }

    }
    public void movementRandom()
    {
        agente.enabled = false;
        counter += 1 * Time.deltaTime;
        if (counter >= 4)
        {
            translate = false;
            routine = Random.Range(0, 2);
            counter = 0;
            switch (routine)
            {
                case 0:
                    // nothing
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 90f);
                    translate = true;
                    break;
            }

        }

        if (translate)
        {
            transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }

    }
    public void followPlayer()
    {
        agente.enabled = true;
        agente.destination = player.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusAlert);
    }

}
