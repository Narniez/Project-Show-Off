using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int buttonIndex; // Index of the button in the sequence
    public LightController lightController; // Reference to the LightController script

    private void OnMouseDown()
    {
        // Check if the button index matches the current index in the light sequence
        if (buttonIndex == lightController.GetCurrentIndex())
        {
            lightController.IncrementIndex(); // Increment the index in the light sequence

            if (lightController.GetCurrentIndex() == lightController.GetLightSequenceLength())
            {
                // Puzzle completed, handle puzzle completion here
                Debug.Log("Puzzle completed!");
            }
        }
        else
        {
            // Incorrect button pressed, handle the incorrect input (e.g., reset the puzzle)
            lightController.ResetSequence();
        }
    }
}
