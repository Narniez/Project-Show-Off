using UnityEngine;

public class MultipleButtonsBehaviour : MonoBehaviour
{
    private IButton[] buttons;

    private void Start()
    {
        buttons = GetComponentsInChildren<IButton>();   //Add all the children that have IButton interface 
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonState();
    }

    /// <summary>
    /// This method checks if all the children objects that are buttons are pressed.
    /// if not, break the loop
    /// </summary>
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