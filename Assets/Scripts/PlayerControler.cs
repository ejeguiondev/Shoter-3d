using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Camera playerCam;
    public Vector3 rotateInput = Vector3.zero;
    public float rotacionSensibility;
    public Vector3 moveInput = Vector3.zero;
    private float camVerticalAngle;

    public Rigidbody rg;

    public float movementSpeed;
    public float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotacionSensibility = 1000f;
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;

        Vector3 direccion = (transform.forward * ver + transform.right * hor).normalized;
        if (hor != 0 || ver != 0)
        {
            velocity = direccion * movementSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && trigerGround.isGrounded)
        {
            rg.velocity = new Vector3(rg.velocity.x, jumpSpeed, rg.velocity.z);
        }

        velocity.y = rg.velocity.y;
        rg.velocity = velocity;

        Look();
    }

    void Look()
    {
        // agarrar el angulo X e Y
        rotateInput.x = Input.GetAxis("Mouse X") * rotacionSensibility * Time.deltaTime;
        rotateInput.y = Input.GetAxis("Mouse Y") * rotacionSensibility * Time.deltaTime;
        // hacer que no se pase de vista mirando hacia arriba
        camVerticalAngle += rotateInput.y;
        camVerticalAngle = Mathf.Clamp(camVerticalAngle, -70, 70);
        // agregar
        transform.Rotate(Vector3.up * rotateInput.x);
        playerCam.transform.localRotation = Quaternion.Euler(-camVerticalAngle, 0, 0);
    }
}
