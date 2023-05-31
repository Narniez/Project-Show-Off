using UnityEngine;

public interface IButton
{
    bool IsPressed { get; set; }
}

public class ButtonScript : Interactable, IButton
{
    public bool IsPressed { get; set; }

    public LightPuzzle puzzle;

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
    }

    /// <summary>
    /// Loop through all the lights and if this button is pressed lights are on or off
    /// </summary>
    void TurnLights()
    {
        foreach (ILight light in puzzle.lights)
        {
            if (light.thisIsCorrect)
            {
                Light lightComponent = (light as MonoBehaviour).GetComponent<Light>();
                lightComponent.enabled = IsPressed;
            }
        }
    }
}
