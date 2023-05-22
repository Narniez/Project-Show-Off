using UnityEngine;

public class RotaryDiskPiece : Interactable
{
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
            Quaternion currentRotation = this.gameObject.transform.rotation;
            Quaternion rotationIncrement = Quaternion.Euler(0, 0, 45);
            Quaternion newRotation = currentRotation * rotationIncrement;
            this.gameObject.transform.rotation = newRotation;   
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
