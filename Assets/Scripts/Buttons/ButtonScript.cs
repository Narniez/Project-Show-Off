using UnityEngine;

public interface IButton
{
    bool IsPressed { get; set; }
}

public class ButtonScript : Interactable, IButton
{
    public bool IsPressed { get; set; }

    public override void OnFocus()
    {
        //Debug.Log("Lookking at button");
    }

    public override void OnInteract()
    {
        IsPressed = true;
        Debug.Log("You pressed a button");
    }

    public override void OnLoseFocus()
    {
        IsPressed = false;
    }
}
