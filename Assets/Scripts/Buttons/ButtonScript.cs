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
        if (Input.GetMouseButton(0))
        {
            Debug.Log("You pressed a button");
            IsPressed = true;
        }
        else { IsPressed = false; }
    }

    public override void OnInteract()
    {

    }

    public override void OnLoseFocus()
    {
        IsPressed = false;
    }
}
