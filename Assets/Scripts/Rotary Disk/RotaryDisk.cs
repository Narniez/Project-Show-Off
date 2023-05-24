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
    public Quaternion targetAngle;

    /// <summary>
    /// Checks if the disks are in the correct rotation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>

    public bool CorrectPuzzlePosition()
    {
        bool isCorrect = targetAngle.eulerAngles.z == transform.rotation.eulerAngles.z;
        return isCorrect;
    }
}
