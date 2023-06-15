using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;

    [SerializeField] private Interactable currentInteractable;

    private CameraController camController;

    private Animator anim;
    private Camera mainCamera;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private PlayerInput playerInput;

    private IPlayer playerControls;

    public LayerMask layerMask;

    public Vector3 uiPosition = new Vector3(0, 0, 0);

    [SerializeField]private GameObject pressF;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private GameObject rotDiscUI;
    //private bool canInteract = true;
    public TextMeshProUGUI textUI;
    UIElement uiElement;
    //Get reference's to the PlayerInput action map and camera
    private void Awake()
    {
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        playerInput = this.GetComponentInParent<PlayerInput>();
        mainCamera = playerInput.camera;
    }

    //Get reference's to the camera controller and the player controls
    private void Start()
    {
        rotDiscUI.gameObject.transform.position += uiPosition;
        towerUI.gameObject.transform.position += uiPosition;
        camController = this.GetComponentInChildren<CameraController>();
        playerControls = this.GetComponentInParent<IPlayer>();
    }

    //Subscirbe to the started event of the interaction action
    private void OnEnable()
    {
        player.FindAction("Interaction").started += Interact;
    }

    //Unsubscirbe
    private void OnDisable()
    {
        player.FindAction("Interaction").started -= Interact;
    }

    // Update is called once per frame
    void Update()
    {
        IsHeld();
        HandleInteractionCheck();
        HandleRaycastPosition();
        if (currentInteractable != null && currentInteractable.CompareTag("CompanionCube") && currentInteractable.GetComponent<CompanionCube>().isPicked && !currentInteractable.GetComponent<CompanionCube>().isPlaced)
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Drop Cube";
        }
        if(currentInteractable != null && currentInteractable.CompareTag("HoldButton"))
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Hold ";
        }
        if (currentInteractable != null && currentInteractable.CompareTag("CompanionCube") && !currentInteractable.GetComponent<CompanionCube>().isPicked)
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Up Cube";
        }


    }

    //Check and assign current interactable using raycast
    void HandleInteractionCheck()
    {
        var ray = mainCamera.ViewportPointToRay(interactionRaypoint);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance,layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            //If there is no currentInteractable or the hit object is an interactable different from the current one 
            if (currentInteractable == null || hit.collider.gameObject != currentInteractable.gameObject)
            {
                //If we had an interactable before hitting the new one call the onLoseFocus method for that interactable
                if (currentInteractable != null)
                {
                    //button.IsPressed = false;
                    currentInteractable.OnLoseFocus();
                    pressF.SetActive(false);

                }

                hit.collider.TryGetComponent(out currentInteractable);
                hit.collider.TryGetComponent(out anim);
                //Check if the object we are hitting is an interactable and assign the current interactable 
                if (hit.collider.TryGetComponent(out currentInteractable))
                {
                    //If the current interactable is in range of the raycast call the OnFocus method 
                    currentInteractable.OnFocus();
                    pressF.SetActive(true);

                    if (currentInteractable.CompareTag("CompanionCube"))
                    {
                        pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Up Cube";
                    }
                    
                    //Assign the camera controller to the rotary disk the raycast hits
                    if (currentInteractable != null && (currentInteractable.CompareTag("RotaryDisk") || currentInteractable.CompareTag("RotaryDiskLeft")))
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

        else if (currentInteractable == null)
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Press ";
        }

        //If we are not looking at an interactable nullify the last current interactable 
        else if (currentInteractable != null)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
            pressF.SetActive(false);
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Press ";
        }
    }

    //Method that gets called when the interaction action has started 
    private void Interact(InputAction.CallbackContext obj)
    {
        HandleInteractionInput();
    }

    void IsHeld()
    {
        if (currentInteractable == null) return;
        ButtonScript button;
        if (currentInteractable.TryGetComponent(out button)) // try to get ButtonScript from currentInteractable object and assign it
        {
            button.IsPressed = player.FindAction("Interaction").IsInProgress(); // if the Interaction action is in progress the button is pressed
        }
    }

    //Change raycast position based on what we are looking at 
    void HandleRaycastPosition()
    {
        //Move raycast to the left when we are locked on the Disk puzzle
        //if (camController.IsLockedOnDisk)
        //{
        //    interactionRaypoint = new Vector3(-0.2f, 0.5f, 0f);
        //}
         if (camController.IsLockedOnDiskLeft)
        {
            interactionRaypoint = new Vector3(0.8f, 0.5f, 0f);
        }
        if (playerControls.IsLockedOnTower)
        {
            towerUI.SetActive(true);
            pressF.SetActive(false);
        }
        else if(camController.IsLockedOnDisk || camController.IsLockedOnDiskLeft)
        {
            pressF.SetActive(false);
            rotDiscUI.SetActive(true);
        }
        //Otherwise keep it in the middle 
        else
        {
            towerUI.SetActive(false);
            rotDiscUI.SetActive(false);
            interactionRaypoint = new Vector3(0.5f, 0.5f, 0f);
        }
    }

    //If we have an interactable call the OnInteract method for the current interactable
    void HandleInteractionInput()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }
}
