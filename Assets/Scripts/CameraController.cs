using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private InputActionAsset inputAsset;
    private InputActionMap player;

    public Transform target;  // The object you want to reach and focus on
    public Transform snapTarget;

    public float transitionSpeed = 5f;  // Speed of the camera transition
    public float verticalMoveSpeed = 2f;  // Speed of vertical camera movement
    public float minY = 1f;  // Minimum y-axis position of the camera
    public float maxY = 0.01f;
    public float snapAmount = 0.03f;

    private bool isSnapping = false;
    private bool isTransitioning = false;  // Flag to check if camera is transitioning
    private Vector3 targetPosition;  // Target position for the camera
    private Quaternion targetRotation;  // Target rotation for the camera

    bool isLocked = false;

    bool isTransitioningBack = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        targetPosition = transform.position;
        targetRotation = transform.rotation;

    }

    private void Update()
    {
        if (isTransitioning)
        {
            // Smoothly move the camera to the target position and rotation
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);

            // If camera is close enough to the target position and rotation, stop transitioning
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f && Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
                isTransitioning = false;
        }
        if (isTransitioningBack)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, transitionSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originalPosition) < 0.05f && Quaternion.Angle(transform.rotation, originalRotation) < 0.5f)
            {
                Debug.Log("Stop transition");
                isTransitioningBack = false;

            }

        }
        else
        {

            if (!MouseLook.canLook)
            {

                // Handle user input for vertical camera movement

                Vector2 verticalInput = player.FindAction("Movement").ReadValue<Vector2>();

                float desiredY = transform.position.y + verticalInput.y * snapAmount;
                //desiredY = Mathf.Clamp(desiredY, minY, maxY);
                transform.position = new Vector3(transform.position.x, desiredY, transform.position.z);

                // Limit the camera's y-axis position
                Vector3 clampedPosition = transform.position;
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
                transform.position = clampedPosition;

                // Lock camera position and rotation
                transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);

            }

            if (isLocked && Input.GetKeyDown(KeyCode.K))
            {
                isTransitioningBack = true;
                MouseLook.canLook = true;
                PlayerController.canMove = true;
                if (!isTransitioningBack)
                {

                }
                    isLocked = false;
            }

            // Check if the user presses a button to initiate camera transition
            if (Input.GetKeyDown(KeyCode.P) && !isLocked)
            {
                originalPosition = transform.position;
                originalRotation = transform.rotation;
                PlayerController.canMove = false;
                MouseLook.canLook = false;
                targetPosition = target.position;
                targetRotation = target.rotation;
                isTransitioning = true;
                isLocked = true;
            }

        }
    }
}
