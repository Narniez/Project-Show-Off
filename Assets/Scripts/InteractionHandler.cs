using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

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
            HandleInteractionInput();
        }
    }

    void HandleInteractionCheck()
    {
        var ray = mainCamera.ViewportPointToRay(interactionRaypoint);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.CompareTag("PPiece"))
            {
                Debug.Log("Looking at puzzle");
            }
            if (hit.collider.CompareTag("RotaryDisk"))
            {
                Debug.Log("Looking at disk");
            }


            // ?!?!?!?!????
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
            currentInteractable.OnInteract();
        }
    }


}
