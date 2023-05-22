using UnityEngine;

public class RotaryDiskPiece : Interactable
{
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
<<<<<<< Lennard-Particles:Assets/RotaryDiskPiece.cs
            Quaternion currentRotation = this.gameObject.transform.rotation;
            Quaternion rotationIncrement = Quaternion.Euler(0, 0, 45);
            Quaternion newRotation = currentRotation * rotationIncrement;
            this.gameObject.transform.rotation = newRotation;   
=======
>>>>>>> Decoupling, Added buttons, Optimization:Assets/Scripts/RotaryDiskPiece.cs
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
