﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerControls : NetworkBehaviour {

    //Mouse look
    private Transform cam;
    private Transform gui;
    private float yRotation, xRotation, currentXRotation, currentYRotation, yRotationV, xRotationV;
    public float lookSensitivity = 5;
    public float lookSmoothnes = 0.1f;
    public float bottom = 60F;
    public float top = -60f;

    //Movement
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    //Timers
    public float attackTimer = 1.0f;

    //Menu
    public GameObject menu;
    private bool menuOpen = false;

    // Use this for initialization
    void Start() {
        cam = GetComponentInChildren<Camera>().transform;
        gui = transform.Find("GUI");
        if (!isLocalPlayer)
        {
            cam.gameObject.SetActive(false);
            gui.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

        if (isLocalPlayer)
        {
            Mouselook();
            Movement();
            Timers();

            if (Input.GetMouseButtonDown(0) && !menuOpen)
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenu();
            }
        }

    }

    void MainMenu()
    {
        menu.SetActive(!menu.activeSelf);
        menuOpen = !menuOpen;
        Debug.Log("Menu opened or closed.");
    }

    void Movement()
    {
        //Movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Mouselook()
    {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 100);
        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothnes);
        if (currentXRotation > bottom)
        {
            currentXRotation = bottom;
            xRotation = bottom;
        }

        if (currentXRotation < top)
        {
            currentXRotation = top;
            xRotation = top;
        }

        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        cam.transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
    }

    void Attack()
    {
        if (attackTimer <= 0)
        {
            Debug.Log(gameObject.name + " attacked.");
            attackTimer = 0.0f;

            RaycastHit melee = new RaycastHit();
            if (Physics.Raycast(transform.position, cam.transform.forward, out melee, 5.0f))
            {
                Debug.DrawLine(transform.position, melee.transform.position, Color.cyan, 10f);
                CmdDoDamage(melee.transform.gameObject, 5);
            }
        }
    }

    [Command]
    void CmdDoDamage(GameObject t, int dam)
    {
        t.transform.SendMessage(("TakeDamage"), dam, SendMessageOptions.DontRequireReceiver);
    }

    void Timers()
    {
        attackTimer -= Time.deltaTime;
    }
}