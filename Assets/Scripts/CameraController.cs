using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput playerInput;
    public Camera mainCamera;

    public Transform target;  // The object you want to reach and focus on

    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;

    public float transitionSpeed = 5f;  // Speed of the camera transition
    public float verticalMoveAmount = 0.5f;  // Speed of vertical camera movement
    public float maxY = 10f;  // Minimum y-axis position of the camera
    public float minY = 1f;
    public float snapAmount = 0.03f;

    //private bool isSnapping = false;
    public bool isTransitioning = false;  // Flag to check if camera is transitioning
    private Vector3 targetPosition;  // Target position for the camera
    private Quaternion targetRotation;  // Target rotation for the camera

    public static bool isLocked = false;

    public bool isTransitioningBack = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;


    private void Start()
    {
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInput = this.GetComponentInParent<PlayerInput>();
        mainCamera = playerInput.camera;
    }

    private void Update()
    {
        CameraMovement();

        var ray = mainCamera.ViewportPointToRay(interactionRaypoint);
        if (Physics.Raycast(ray, out RaycastHit hit, /*1.5f*/interactionDistance))
        {
            if (hit.collider.CompareTag("PPiece"))
            {
                Transform p = hit.collider.transform.parent;
                int childCount = p.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    if (p.GetChild(i).CompareTag("Target"))
                    {
                        target = p.GetChild(i);
                        if (Input.GetKeyDown(KeyCode.P) && !isTransitioningBack)
                        {
                            originalPosition = transform.position;
                            originalRotation = transform.rotation;
                            PlayerControls.canLook = false;
                            PlayerControls.canMove = false;
                            targetPosition = target.position;
                            targetRotation = target.rotation;
                            isTransitioning = true;
                            isLocked = true;
                            break;
                        }
                    }

                }
            }
            else
            {
                target = null;
            }
        }
    }

    void CameraMovement()
    {
        if (target == null) return;
        if (isTransitioning)
        {
            // Smoothly move the camera to the target position and rotation
            transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, transitionSpeed * Time.deltaTime);

            // If camera is close enough to the target position and rotation, stop transitioning
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f && Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {
                isTransitioning = false;
                //isLocked = true;
            }
        }
        if (isTransitioningBack)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, transitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, transitionSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originalPosition) < 0.05f && Quaternion.Angle(transform.rotation, originalRotation) < 0.5f)
            {
                Debug.Log("Stop transition");
                isTransitioningBack = false;
                PlayerControls.canLook = true;
                PlayerControls.canMove = true;
            }

        }
        else
        {
            if (isLocked && Input.GetKeyDown(KeyCode.P))
            {
                isTransitioningBack = true;
                isLocked = false;

            }

            // Check if the user presses a button to initiate camera transition
            if (isLocked)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    CameraControls(Vector3.up * verticalMoveAmount);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    CameraControls(Vector3.down * verticalMoveAmount);
                }
            }
        }
    }

    void CameraControls(Vector3 offset)
    {
        Vector3 newPos = transform.position += offset;
        Debug.Log(newPos.y);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;

    }
}
