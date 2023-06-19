using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private GameObject pressF;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private GameObject rotDiscUI;
    //private bool canInteract = true;
    public TextMeshProUGUI textUI;

    private IPlayer playerControls;
    private CameraController camController;

    public Vector3 uiPosition = new Vector3(0, 0, 0);
    [SerializeField] private Interactable currentInteractable;
    void Start()
    {
        rotDiscUI.gameObject.transform.position += uiPosition;
        towerUI.gameObject.transform.position += uiPosition;
        playerControls = this.GetComponentInParent<IPlayer>();
        camController = this.GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentInteractable = this.GetComponentInParent<InteractionHandler>().currentInteractable;

        HandleCubeUI();
        HandlePuzzlesUI();
    }


 

    void HandleCubeUI()
    {
        if (currentInteractable != null && currentInteractable.CompareTag("CompanionCube") && currentInteractable.GetComponent<CompanionCube>().isPicked && !currentInteractable.GetComponent<CompanionCube>().isPlaced)
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Drop Cube";
        }
        if (currentInteractable != null && currentInteractable.CompareTag("HoldButton"))
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Hold ";
        }
        if (currentInteractable != null && currentInteractable.CompareTag("CompanionCube") && !currentInteractable.GetComponent<CompanionCube>().isPicked)
        {
            pressF.GetComponentInChildren<TextMeshProUGUI>().text = "Pick Up Cube";
        }
    }

    void HandlePuzzlesUI()
    {
        if (playerControls.IsLockedOnTower)
        {
            towerUI.SetActive(true);
            pressF.SetActive(false);
        }
        else if (camController.IsLockedOnDisk || camController.IsLockedOnDiskLeft)
        {
            pressF.SetActive(false);
            rotDiscUI.SetActive(true);
        }
        //Otherwise keep it in the middle 
        else
        {
            towerUI.SetActive(false);
            rotDiscUI.SetActive(false);
        }
    }
}
