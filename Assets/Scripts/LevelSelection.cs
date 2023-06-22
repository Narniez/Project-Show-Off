using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int index;

    private InputActionMap player;
    private PlayerInput playerInput;

    private int controllerNumber;

    [SerializeField] private List<GameObject> scenes;
    [SerializeField] private Sprite retroImage;
    [SerializeField] private Sprite vaporImage;
    [SerializeField] private Sprite spiraImage;

   [SerializeField] private List<Gamepad> gamepads = new List<Gamepad>();

    private Sprite retroNormal;
    private Sprite vaporNormal;
    private Sprite spiralNormal;

    private void Start()
    {
        gamepads.AddRange(Gamepad.all);
        Debug.Log(gamepads.Count);
        InputSystem.DisableDevice(gamepads[1]);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerInput = this.GetComponent<PlayerInput>();
        player = playerInput.currentActionMap;

        retroNormal = scenes[0].GetComponent<Image>().sprite;
        vaporNormal = scenes[1].GetComponent<Image>().sprite;
        spiralNormal = scenes[2].GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (player.FindAction("GoLeft").triggered && index != 1)
        {
            index--;
            Debug.Log(index);
        }
        if (player.FindAction("GoRight").triggered && index != 3)
        {
            index++;
            Debug.Log(index);
        }

        for (int i = 0; i < scenes.Count; i++)
        {
            if (index == i + 1)
            {
                Image sceneImage = scenes[i].GetComponent<Image>();
                sceneImage.sprite = GetSceneSprite(i + 1);

                if (player.FindAction("Interaction").triggered)
                {
                    scenes[i].GetComponent<Button>().onClick.Invoke();
                }
            }
            else
            {
                Image sceneImage = scenes[i].GetComponent<Image>();
                sceneImage.sprite = GetNormalSprite(i + 1);
            }
        }
    }

    private Sprite GetSceneSprite(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                return retroImage;
            case 2:
                return vaporImage;
            case 3:
                return spiraImage;
            default:
                return null;
        }
    }

    private Sprite GetNormalSprite(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                return retroNormal;
            case 2:
                return vaporNormal;
            case 3:
                return spiralNormal;
            default:
                return null;
        }
    }
}