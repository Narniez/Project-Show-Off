using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Interactable currentInteractable;
    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;

    public GameObject playerA;

    private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput playerInput;



    public static bool canInteract = true;

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
            HandleRaycastPosition();
            //HandleInteractionInput();
        }
    }

    void HandleInteractionCheck()
    {
        var ray = mainCamera.ViewportPointToRay(interactionRaypoint);
        if (Physics.Raycast(ray, out RaycastHit hit, /*1.5f*/interactionDistance))
        {
             Debug.DrawLine(ray.origin, mainCamera.transform.forward * 5000, Color.red);
            if (/*hit.collider.gameObject.layer == 9 && */(currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }

                if (currentInteractable != null && currentInteractable.CompareTag("RotaryDisk"))
                {
                    Debug.Log("Diskaaa");
                    currentInteractable.GetComponent<RotaryDiskHolder>().SetCameraController(playerA.GetComponentInChildren<CameraController>());
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

    void HandleRaycastPosition()
    {
        if (CameraController.isLockedOnTower)
        {
            interactionRaypoint = new Vector3(-0.2f, 0.5f, 0f);
        }
        else
        {
            interactionRaypoint = new Vector3(0.5f, 0.5f, 0f);
        }
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
