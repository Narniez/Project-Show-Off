using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public int index = 0;
    [SerializeField] private List<GameObject> options;

    [SerializeField] private Sprite optionDefault;

    [SerializeField] private Sprite optionSelected;

    [SerializeField] private GameObject pressF;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private GameObject rotDiscUI;
    [SerializeField] private GameObject pauseMenu;
    //private bool canInteract = true;
    public TextMeshProUGUI textUI;

    private IPlayer playerControls;
    private CameraController camController;

    private InputActionMap player;
    private PlayerInput playerInput;

    [SerializeField] private GameObject crossHair;

    public Vector3 uiPosition = new Vector3(0, 0, 0);
    [SerializeField] private Interactable currentInteractable;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player = playerInput.currentActionMap;

        rotDiscUI.gameObject.transform.position += uiPosition;
        towerUI.gameObject.transform.position += uiPosition;

        playerControls = this.GetComponentInParent<IPlayer>();
        camController = this.GetComponentInChildren<CameraController>();

        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentInteractable = this.GetComponentInParent<InteractionHandler>().currentInteractable;

        HandleCubeUI();
        HandlePuzzlesUI();

        HandlePauseMenuUI();
 
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

    void HandlePauseMenuUI()
    {
        if (player.FindAction("PauseMenu").triggered)
        {
            pauseMenu.SetActive(true);
            crossHair.SetActive(false);
            Time.timeScale = 0;
        }

        if (Time.timeScale == 0 && player.FindAction("CameraUp").triggered && index != 1)
        {
            index--;
            Debug.Log(index);
            Debug.Log("Up");
        }
        if (Time.timeScale == 0 && player.FindAction("CameraDown").triggered && index != 3)
        {
            index++;
            Debug.Log(index);
            Debug.Log("Down");
        }

        for (int i = 0; i < options.Count; i++)
        {
            if (index == i + 1)
            {
                Image sceneImage = options[i].GetComponent<Image>();
                sceneImage.sprite = GetSceneSprite(i + 1);

                if (player.FindAction("Interaction").triggered)
                {
                    options[i].GetComponent<Button>().onClick.Invoke();
                }
            }
            else
            {
                Image sceneImage = options[i].GetComponent<Image>();
                sceneImage.sprite = GetNormalSprite(i + 1);
            }
        }
    }

    private Sprite GetSceneSprite(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                return optionDefault;
            case 2:
                return optionDefault;
            case 3:
                return optionDefault;
            default:
                return null;
        }
    }

    private Sprite GetNormalSprite(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                return optionSelected;
            case 2:
                return optionSelected;
            case 3:
                return optionSelected;
            default:
                return null;
        }
    }

    public void DesactivateUI()
    {
        crossHair.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
