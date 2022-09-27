using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerGround : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
