using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[Serializable]
public class Int2Event : UnityEvent<int,int>
{

}
public class EventManager : MonoBehaviour
{
    #region Signgleton
    public static EventManager current;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        } else if (current != null)
        {
            Destroy(this);
        }
    }
    #endregion
     
    // Evento puede hacer una funcion y elegir que scripts lo van a escuchar deveria ver un poco mas del tema en el manual de unity
    public Int2Event updateBalas = new Int2Event();

    public UnityEvent NewGun = new UnityEvent();
}
