using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    //private InputMaster controls;
    private PlayerInput playerInput;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction moveA;

    public float moveSpeed = 6f;
    public float jumpHeigh = 2f;

    private Vector3 velocity;

    private float gravity = -9.81f;

    private Vector2 move;

    private CharacterController controller;

    public Transform ground;

    public float distanceToGround = 0.4f;
    public LayerMask groundLayer;

    private bool isGrounded;

    public float fallSpeed = 2f;

    private void Awake()
    {

        //controls = new InputMaster();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        controller = GetComponent<CharacterController>();
        
     
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.camera = cam;
    }

    private void Update()
    {
        Gravity();
        //Movement();
    }

    private void FixedUpdate()
    {
        move = moveA.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        //controls.Enable();
        player.FindAction("Jump").started += DoJump;
        moveA = player.FindAction("Movement");
        player.Enable();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        velocity.y = Mathf.Sqrt(jumpHeigh * -fallSpeed * gravity);
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= DoJump;
        player.Disable();
        //controls.Disable();
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -fallSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
