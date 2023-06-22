using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLIghtsButton : Interactable, IButton
{
    //bool activateSequence = false;

    [SerializeField]LightController lightController;
    public bool IsPressed { get; set; }
    public AudioClip soundEffect { get; set; }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
            lightController.StartLightSequence();
    }

    public override void OnLoseFocus()
    {
        
    }
}
