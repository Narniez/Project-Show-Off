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

    private bool isCameraLocked = false;

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
           Debug.DrawLine(ray.origin, mainCamera.transform.forward * 5000, Color.red);
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isCameraLocked)
                {
                    isCameraLocked = false;
                    cameraController.UnlockCamera();
                }
                else
                {
                    if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
                    {
                        if (hit.collider.CompareTag("PPiece"))
                        {
                            isCameraLocked = true;
                            cameraController.LockCamera(hit.collider.transform); // Lock camera and set the target
                        }
                    }
                }
            }

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
