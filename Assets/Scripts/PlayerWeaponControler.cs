using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControler : MonoBehaviour
{
    // lista de todas las armas que tienen ese script (WeaponControler)
    public List<WeaponControler> startingWeapons = new List<WeaponControler>();
    //variables
    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;
    //los weapos (1 = arma 2 = otra arma)
    public int activeWeaponIndex { get; private set; }
    // la cantidad de armas
    private WeaponControler[] weaponSlots = new WeaponControler[2];
    public GameObject owner { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        //inciar sin armas
        activeWeaponIndex = -1;
        // agregar weapons
        foreach (WeaponControler startingWeapon in startingWeapons)
        {
            addWeapon(startingWeapon);
        }

        SwhichesWeapon();
        EventManager.current.NewGun.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // al tocar 1 que aparesca la arma en la posicion 0
            SwhichesWeapon();
        }
    }

    void SwhichesWeapon()
    {
        int tempIndex = (activeWeaponIndex + 1) % weaponSlots.Length;
        // aparecer arma
        if (weaponSlots[tempIndex] == null)
            return;

        foreach (WeaponControler weapon in weaponSlots)
        {
            if (weapon != null)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        weaponSlots[tempIndex].gameObject.SetActive(true);
        weaponParentSocket.position = defaultWeaponPosition.position;
        activeWeaponIndex = tempIndex;
    }
    // p_weaponPrefap es el modelo del arma
    void addWeapon(WeaponControler p_weaponPrefap)
    {
        // añadir un arma
        // agregar a la lista las armas
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponParentSocket.position = defaultWeaponPosition.position;
            if (weaponSlots[i] == null)
            {
                WeaponControler weaponClone = Instantiate(p_weaponPrefap, weaponParentSocket);
                weaponClone.gameObject.SetActive(false);

                weaponSlots[i] = weaponClone;
                return;
            }

        }


    }

}
