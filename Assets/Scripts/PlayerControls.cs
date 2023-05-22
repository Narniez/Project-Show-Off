using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cameraHolder;
    public float speed;
    public float sensitivity;
    public float maxForce;
    private Vector2 move;
    private Vector2 look;
    private float lookRotation;

    private InputActionMap player;
    private PlayerInput playerInput;

    public  bool canLook = true;
    public  bool canMove = true;
    // Start is called before the first frame update

    private void Awake()
    {
        playerInput = this.GetComponent<PlayerInput>();
        //cameraHolder = playerInput.camera;

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(canLook)
        Look();
    
    }

    private void FixedUpdate()
    {
        if (canMove)
        Move();
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

        //Calculate foces
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //Limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
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
