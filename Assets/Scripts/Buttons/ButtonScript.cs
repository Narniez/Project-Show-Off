using UnityEngine;

public interface IButton
{
    bool IsPressed { get; set; }
}

public class ButtonScript : Interactable, IButton
{
    public bool IsPressed { get; set; }
    public Light[] lights;

    public override void OnFocus()
    {
        //Debug.Log("Lookking at button");
    }
    void Update()
    {
        TurnLights();
    }

    public override void OnInteract()
    { }

    public override void OnLoseFocus()
    {
        IsPressed = false;
        TurnLights();
    }

    void TurnLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = IsPressed;
        }
    }
}
