using System;
using UnityEngine;

public class RotaryDisk : PuzzleAbstract 
{
    public Quaternion correctAngle; 

    public override void OnFocus() { }

    public override void OnInteract() { }

    public override void OnLoseFocus() { }


    private void FixedUpdate()
    {
        if (IsCorrect(transform.rotation, correctAngle, Axis.Z))
        {
            Debug.Log(this.name + " is in correct position with Z angle =  " + this.transform.rotation.eulerAngles.z);
        }
    }

    /// <summary>
    /// Checks if the disks are in the correct rotation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    /// 
    public void RotatePiece(int rotAmount)
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion rotationIncrement = Quaternion.Euler(0, 0, (int)rotAmount);
        Quaternion newRotation = currentRotation * rotationIncrement;

        // Round the rotation to the nearest integer value
        Vector3 eulerAngles = newRotation.eulerAngles;
        eulerAngles.z = Mathf.Round(eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
        //Debug.Log(" Current Z rotation is " + transform.rotation.eulerAngles.z);
       //Debug.Log(" Target Z rotation is: " + targetAngle.eulerAngles.z);
    }

}
