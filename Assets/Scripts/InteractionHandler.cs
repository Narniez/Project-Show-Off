using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Interactable currentInteractable;
    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput playerInput;

    public static bool canInteract = true;

    public CameraControls cameraController;
    public float maxDistance = 5f;

    public bool isCameraLocked = false;
    public Transform target;

    public bool camMove;
    public bool camMoveBack;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {

        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInput = this.GetComponentInParent<PlayerInput>();
        mainCamera = playerInput.camera;

    }

    private void OnEnable()
    {
        player.FindAction("Interaction").started += Interact;
    }


    private void OnDisable()
    {
        player.FindAction("Interaction").started -= Interact;
    }
    // Update is called once per frame
    void Update()
    {
        if (CameraController.isLocked)
        {
            interactionRaypoint = new Vector3(-0.2f, 0.5f, 0f);
        }
        else
        {
            interactionRaypoint = new Vector3(0.5f, 0.5f, 0f);
        }
        if (canInteract)
        {
            HandleInteractionCheck();
            //HandleInteractionInput();
        }
    }

    void HandleInteractionCheck()
    {
        var ray = mainCamera.ViewportPointToRay(interactionRaypoint);
        if (Physics.Raycast(ray, out RaycastHit hit, /*1.5f*/interactionDistance))
        {
              //CameraInteraction(hit);
             Debug.DrawLine(ray.origin, mainCamera.transform.forward * 5000, Color.red);
            if (/*hit.collider.gameObject.layer == 9 && */(currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
            else if (currentInteractable)
            {
                currentInteractable.OnLoseFocus();
                currentInteractable = null;
            }
        }
    }

    void CameraInteraction(RaycastHit hit)
    {
        Vector3 camPos = mainCamera.transform.position;
        Quaternion camRot = mainCamera.transform.rotation;

        if (hit.collider.CompareTag("PPiece"))
        {
            Transform p = hit.collider.transform.parent;
            int childCount = p.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (p.GetChild(i).CompareTag("Target"))
                {
                    target = p.GetChild(i);
                    if (Input.GetKeyDown(KeyCode.M) && !isCameraLocked)
                    {
                        originalPosition = mainCamera.transform.position;
                        originalRotation = mainCamera.transform.rotation;
                        
                        camMove = true;
                        break;
                    }
                }
            }
        }
        else
        {
            target = null;
            camMove = false;
        }
        if (camMove)
        {
            if (target == null) return;

            PlayerControls.canLook = false;
            PlayerControls.canMove = false;
            mainCamera.transform.position = Vector3.Lerp(camPos, target.position, 5f * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(camRot, target.rotation, 5f * Time.deltaTime);
            // Smoothly move the camera to the target position and rotation
            if (Vector3.Distance(mainCamera.transform.position, target.position) < 0.1f && Quaternion.Angle(mainCamera.transform.rotation, target.rotation) < 0.05f)
            {
                
                Debug.Log("Lock camera");
                camMove = false;
                isCameraLocked = true;
            }
        }

        if (isCameraLocked  && Input.GetKeyDown(KeyCode.M))
        {
            camMoveBack = true;
            isCameraLocked = false;
        }

        if (camMoveBack)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, 10f * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalRotation, 10f * Time.deltaTime);
            if (Vector3.Distance(mainCamera.transform.position, originalPosition) < 0.05f && Quaternion.Angle(mainCamera.transform.rotation, originalRotation) < 0.5f)
            {
                camMoveBack = false;
                isCameraLocked = false;
                target = null;
                PlayerControls.canMove = true;
                PlayerControls.canLook = true;
            }
        }
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        HandleInteractionInput();
    }

    void HandleInteractionInput()
    {
        if (currentInteractable != null /*&& Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionLayer)*/)
        {
            // Debug.Log("Should interact");
            currentInteractable.OnInteract();
        }
    }
}
