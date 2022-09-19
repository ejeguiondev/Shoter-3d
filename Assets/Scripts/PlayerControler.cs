using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// el script requiere el characterControler
[RequireComponent(typeof(CharacterController))]

public class PlayerControler : MonoBehaviour
{
    [Header("Referencias")]
    public Camera playerCam;
    [Header("General")]
    public float gravityScale;
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;

    public Vector3 rotateInput = Vector3.zero;
    public float rotacionSensibility;
    public Vector3 moveInput = Vector3.zero;
    public CharacterController characterControler;

    private float camVerticalAngle;
    // Start is called before the first frame update
    void Start()
    {
        characterControler = GetComponent<CharacterController>();
        rotacionSensibility = 1000f;
        walkSpeed = 5f;
        runSpeed = 20f;
        jumpHeight = 1.9f;
        gravityScale = -20f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    void Move()
    {
        // si estamos encima de suelo
        if (characterControler.isGrounded)
        {
            // moverse hacia los lados Sprint
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // no se pase de velocidad de un frame
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            // correr
            if (Input.GetButton("Sprint"))
            {
                // correr
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            } else
            {
                // normal
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            // si tocamos la tecla asociada a el salto (espacio, la x de un mando, etc...)
            // getButtondown detecta si se toco una ves getbutton si se mantiene
            if (Input.GetButtonDown("Jump"))
            {
                // magia del salto
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }
        // gravedad por que en character controler no se permiten rigidbodies.
        moveInput.y += gravityScale * Time.deltaTime;

        // mover jugador
        characterControler.Move(moveInput * Time.deltaTime);
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
        playerCam.transform.localRotation = Quaternion.Euler(-camVerticalAngle,0,0);
    }

}
