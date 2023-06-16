using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int index;

    private InputActionMap player;
    private PlayerInput playerInput;

    [SerializeField] private List<GameObject> scenes;

    public Sprite retroImage;
    public Sprite vaporImage;
    public Sprite spiraImage;

    private Sprite retroNormal;
    private Sprite vaporNormal;
    private Sprite spiralNormal;

    private void Start()
    {
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
        }
        if(player.FindAction("GoRight").triggered && index != 3)
        {
            index++;
        }

        if(index == 1)
        {
            scenes[0].GetComponent<Image>().sprite = retroImage;
            scenes[1].GetComponent<Image>().sprite = vaporNormal;
            scenes[2].GetComponent<Image>().sprite = spiralNormal;
            if (player.FindAction("Interaction").triggered)
            {
                scenes[0].GetComponent<Button>().onClick.Invoke();
            }

            //  scenes[1].GetComponent<Button>().Select();
        }
        if (index == 2)
        {
            scenes[1].GetComponent<Image>().sprite = vaporImage;
            scenes[0].GetComponent<Image>().sprite = retroNormal;
            scenes[2].GetComponent<Image>().sprite = spiralNormal;

            if (player.FindAction("Interaction").triggered)
            {
                scenes[1].GetComponent<Button>().onClick.Invoke();
            }
            //  scenes[1].GetComponent<Button>().Select();
        }
        if (index == 3)
        {
            scenes[2].GetComponent<Image>().sprite = spiraImage;
            scenes[0].GetComponent<Image>().sprite = retroNormal;
            scenes[1].GetComponent<Image>().sprite = vaporNormal;
            if (player.FindAction("Interaction").triggered)
            {
                scenes[2].GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
