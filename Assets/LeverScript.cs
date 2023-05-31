using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interactable
{
    ILight light;
    private void Start()
    {
        light = GetComponentInParent<ILight>();
    }

    public override void OnFocus()
    { }

    public override void OnInteract()
    {
        if (light.thisIsCorrect)
        {
            Debug.Log("azis " + (light as MonoBehaviour).name);
        }
    }

    public override void OnLoseFocus()
    { }


    private void Update()
    {
    }
}
