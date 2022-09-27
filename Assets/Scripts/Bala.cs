using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bala : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject objeto = other.gameObject;
        if (objeto.transform.tag == "Zombie")
        {
            objeto.GetComponent<Rigidbody>().freezeRotation = false;
            Destroy(objeto.GetComponent<EnemyControler>());
            Destroy(objeto.GetComponent<NavMeshAgent>());
            Destroy(objeto, 10);
        } else
        {
            Destroy(gameObject, 3);
        }

    }

}
