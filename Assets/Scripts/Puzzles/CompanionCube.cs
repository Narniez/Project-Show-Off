using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCube : Interactable
{
    public bool isPlaced { get ; set; }

    [SerializeField] public bool isPicked;

    public string text = "Drop Cube";

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        
    }

    public override void OnLoseFocus()
    {
       
    }
}
