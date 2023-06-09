using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayer
{
    bool CanLook { get; set; }
    bool CanMove { get; set; }
    bool IsLockedOnTower { get; set; }

    InputActionMap PlayerAction { get; set; }
}

public class PlayerControls : MonoBehaviour, IPlayer
{
    public Rigidbody rb;
    float speed;
    public float sensitivity;
    public float maxForce;

    public float sprintSpeed;
    public float normalSpeed;

    private Vector2 move;
    private Vector2 look;
    private float lookRotation;

    private InputActionMap player;
    private PlayerInput playerInput;

    private bool isLockedOnTower = false;
    private bool canLook = true;
    [SerializeField] private bool canMove = true;

    [SerializeField] public bool canMoveAtStart = false;

    public bool CanLook { get => canLook; set => canLook = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool IsLockedOnTower { get => isLockedOnTower; set => isLockedOnTower = value; }
    public InputActionMap PlayerAction { get => player; set => player = value; }

    [SerializeField] int jumpForce = 2;
    [SerializeField] bool canJump = false;

    // Start is called before the first frame update
    private Animator anim;
    private void Awake()
    {
        playerInput = this.GetComponent<PlayerInput>();
        player = playerInput.currentActionMap;
        anim = GetComponent<Animator>();
        //cameraHolder = playerInput.camera;

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (canLook)
            Look();
    }

    private void FixedUpdate()
    {
        if (move.x != 0 || move.y != 0)
        {
            anim.SetTrigger("isMoving");
        }
        else
        {
            anim.SetTrigger("isIdling");
        }

        if (canMove && canMoveAtStart)
        {
            Move();
        }
        if (canJump && player.FindAction("Jump").triggered)
        {
            Jump();
        }

        if (player.FindAction("Sprint").IsInProgress())
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }


    private void Move()
    {
        Vector3 currentVelocity = rb.velocity;
        //Find target velocity
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Align directions
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate force
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Look()
    {
        //Turn player
        transform.Rotate(Vector3.up * look.x * sensitivity);

        //Look up and down
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        playerInput.camera.transform.eulerAngles = new Vector3(lookRotation, playerInput.camera.transform.eulerAngles.y, playerInput.camera.transform.eulerAngles.z);
    }
}
