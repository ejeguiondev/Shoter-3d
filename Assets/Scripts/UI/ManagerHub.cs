using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerHub : MonoBehaviour
{
    public GameObject weaponInfoPrefap;

    private void OnEnable()
    {
        EventManager.current.NewGun.AddListener(createWeaponInfo);
    }
    private void OnDisable()
    {
        EventManager.current.NewGun.RemoveListener(createWeaponInfo);
    }

    void createWeaponInfo()
    {
        Instantiate(weaponInfoPrefap, transform);
    }

}
