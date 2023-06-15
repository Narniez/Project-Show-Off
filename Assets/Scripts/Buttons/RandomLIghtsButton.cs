using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLIghtsButton : Interactable
{
    //bool activateSequence = false;

    [SerializeField]LightController lightController;
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
