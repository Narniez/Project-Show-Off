using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    private InputMaster controls;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction moveA;


    public float mouseSensitivity = 150f;

    private Vector2 mouseLook;
    private float xRotation = 0f;

    public Transform playerBody;

    public static bool canLook = true;

    private void Awake()
    {
        controls = new InputMaster();
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (canLook)
        {
            Look();
        }
    }

    private void Look()
    {
        mouseLook = player.FindAction("Look").ReadValue<Vector2>();
        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
