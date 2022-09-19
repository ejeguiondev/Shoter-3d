using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;

public class WeaponInfo_UI : MonoBehaviour
{
    public TMP_Text CurrenBalas;
    public TMP_Text TotalBalas;
    private void OnEnable()
    {
        EventManager.current.updateBalas.AddListener(CurrentUpdate);
    }
    private void OnDisable()
    {
        EventManager.current.updateBalas.RemoveListener(CurrentUpdate);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurrentUpdate(int newBalas, int newTotalBalas)
    {
        CurrenBalas.text = newBalas.ToString();
        TotalBalas.text = newTotalBalas.ToString();

        if (newBalas <= 0)
        {
            CurrenBalas.color = new UnityEngine.Color(1, 0, 0, 1);
        } else
        {
            CurrenBalas.color = new UnityEngine.Color(200, 200, 200);
        }

    }

}
