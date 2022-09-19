using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Quaternion originalLocalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        updateRotation();
    }

    void updateRotation()
    {
        float t_xlook = Input.GetAxis("Mouse X");
        float t_ylook = Input.GetAxis("Mouse Y");

        Quaternion txAngle = Quaternion.AngleAxis(-t_xlook * 10.45f, Vector3.up);
        Quaternion tYAngle = Quaternion.AngleAxis(t_ylook * 10.45f, Vector3.right);
        Quaternion finalAngle = originalLocalRotation * txAngle * tYAngle;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, finalAngle, Time.deltaTime * 10f);

    }

}
