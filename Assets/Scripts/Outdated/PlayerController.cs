using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    private PlayerInput playerInput;

    Rigidbody rb;
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
    public static bool canMove = true;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
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

    private void FixedUpdate()
    {
        if (canMove)
        {
            move = moveA.ReadValue<Vector2>();

            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * moveSpeed * Time.deltaTime);
            
        }
    }

    private void OnEnable()
    {
        player.FindAction("Jump").started += DoJump;
        moveA = player.FindAction("Movement");
        player.Enable();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        //if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeigh * -fallSpeed * gravity);
    }

    private void OnDisable()
    {
        player.FindAction("Jump").started -= DoJump;
        player.Disable();
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -fallSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
