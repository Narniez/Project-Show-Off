using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : Interactable, IButton
{
    [SerializeField] GameObject companionCube;

    [SerializeField] Transform cubeSpawnPos;
    public int buttonIndex; // Index of the button in the sequence
    public LightController lightController; // Reference to the LightController script

    public AudioClip clip;

    public AudioClip puzzleDone;

    public AudioClip soundEffect { get => clip; set => clip = value; }
    public bool IsPressed { get; set; }

    private void Start()
    {
        companionCube.SetActive(false);
    }

    public override void OnInteract()
    {
        SoundEffects.instance.PlaySoundEffect(soundEffect);
        // Check if the button index matches the current index in the light sequence
        if (buttonIndex == lightController.GetCurrentIndex())
        {
            lightController.IncrementIndex(); // Increment the index in the light sequence

            if (lightController.GetCurrentIndex() == lightController.GetLightSequenceLength())
            {
                // Puzzle completed, handle puzzle completion here
                companionCube.transform.position = cubeSpawnPos.position;
                companionCube.SetActive(true);
                SoundEffects.instance.PlaySoundEffect(puzzleDone);
                Debug.Log("Puzzle completed!");
            }
        }
        else
        {
            // Incorrect button pressed, handle the incorrect input (e.g., reset the puzzle)
            lightController.ResetSequence();
        }
    }

    public override void OnFocus()
    {

    }

    public override void OnLoseFocus()
    {
    }
}
