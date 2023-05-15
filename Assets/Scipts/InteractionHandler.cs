using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public Camera mainCamera;
    public Interactable currentInteractable;
    public Vector3 interactionRaypoint = default;
    public float interactionDistance = default;
    public LayerMask interactionLayer = default;
    public KeyCode interactKey = KeyCode.F;
    public KeyCode interactButton = KeyCode.Joystick1Button2;

    public static bool canInteract = true;

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
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, /*1.5f*/interactionDistance))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, mainCamera.transform.forward * 50000000, Color.red);

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

    void HandleInteractionInput()
    {
        if ((Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactButton)) && currentInteractable != null /*&& Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionLayer)*/)
        {
            Debug.Log("Should interact");
            currentInteractable.OnInteract();
        }
    }


}
