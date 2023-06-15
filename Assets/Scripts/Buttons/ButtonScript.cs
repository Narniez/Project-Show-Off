using UnityEngine;

public interface IButton
{
    AudioClip soundEffect { get; set; }
    bool IsPressed { get; set; }
}

public class ButtonScript : Interactable, IButton
{
    public bool IsPressed { get; set; }

    public AudioClip soundEffectClip;

    public AudioClip soundEffect { get => soundEffectClip; set => soundEffectClip = value; }

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
    {
        SoundEffects.instance.PlaySoundEffect(soundEffect);
    }

    public override void OnLoseFocus()
    {
        IsPressed = false;
    }

    /// <summary>
    /// Loop through all the lights and if this button is pressed lights are on or off
    /// </summary>
    void TurnLights()
    {
        if (puzzle == null) return;
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
