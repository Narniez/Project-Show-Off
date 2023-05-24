using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;

    private Camera mainCamera;
    [SerializeField]
    private Interactable currentInteractable;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput playerInput;

    public Animator anim;
    private IPlayer playerControls;

    private bool canInteract = true;

    private void Awake()
    {
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInput = this.GetComponentInParent<PlayerInput>();
        mainCamera = playerInput.camera;
    }
    private void Start()
    {
        playerControls = this.GetComponentInParent<IPlayer>();
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
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            if (currentInteractable == null || hit.collider.gameObject != currentInteractable.gameObject)
            {
                hit.collider.TryGetComponent(out currentInteractable);
                hit.collider.TryGetComponent(out anim);

                if (hit.collider.TryGetComponent(out currentInteractable))
                {
                    currentInteractable.OnFocus();

                    if (currentInteractable != null && currentInteractable.CompareTag("RotaryDisk"))
                    {
                        RotaryDiskHolder tempHolder;
                        if (currentInteractable.TryGetComponent(out tempHolder))
                        {
                            CameraController tempController = transform.GetChild(0).GetComponent<CameraController>();
                            tempHolder.SetCameraController(tempController, playerControls);
                        }
                    }
                }
            }
        }
        else if (currentInteractable != null)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
    private void Interact(InputAction.CallbackContext obj)
    {
        HandleInteractionInput();
    }

    void HandleRaycastPosition()
    {
        //if (playerControls.IsLockedOnTower)
        //{
        //    interactionRaypoint = new Vector3(-0.2f, 0.5f, 0f);
        //}
        //else
        //{
        //}
            interactionRaypoint = new Vector3(0.5f, 0.5f, 0f);
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
