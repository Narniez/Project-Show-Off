using UnityEngine;

public class MultipleButtonsBehaviour : MonoBehaviour
{
    private IButton[] buttons;

    private void Start()
    {
        buttons = GetComponentsInChildren<IButton>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonState();
    }

    void CheckButtonState()
    {
        bool allButtonsPressed = true;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (!buttons[i].IsPressed)
            {
                allButtonsPressed = false;
                break;
            }
        }

        if (allButtonsPressed)
        {
            Debug.Log("All buttons are pressed");
        }
    }

}