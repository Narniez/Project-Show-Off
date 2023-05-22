using System;
using UnityEngine;

public interface IRotaryDiskPuzzle {
    bool CorrectPuzzlePosition();
}
/// <summary>
/// USED AI TO HELP ME TO REMOVE COUPLING (CAUSED SOME PROBLEMS)
/// Gave me the idea of using interface so if I change the script it will not affect the scripts that inherits from it.
/// 
/// Stanislav Velikov
/// </summary>
public class RotaryDisk : MonoBehaviour, IRotaryDiskPuzzle 
{
    public float targetCorrectRotation;

    /// <summary>
    /// Checks if the disks are in the correct rotation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>

    public bool CorrectPuzzlePosition()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        //Mathf.Approximately it returns bool if the 2 floats are approximately equal 
        //(Research it after I saw it from AI. I used a few if checks with casting to (int))
        bool isCorrectPosition = Mathf.Approximately(currentRotation, targetCorrectRotation);
        return isCorrectPosition;
    }

    //public override void OnInteract()
    //{
    //    Quaternion currentRotation = this.gameObject.transform.rotation;
    //    Quaternion rotationIncrement = Quaternion.Euler(0, 0, 45);
    //    Quaternion newRotation = currentRotation * rotationIncrement;
    //    this.gameObject.transform.rotation = newRotation;
    //}

}
