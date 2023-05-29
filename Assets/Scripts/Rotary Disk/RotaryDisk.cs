using System;
using System.Collections;
using UnityEngine;

public class RotaryDisk : PuzzleAbstract 
{
    public Quaternion correctAngle;
    [SerializeField] private float rotationDuration = 1;

    [HideInInspector] public Quaternion targetAngle;
    public override void OnFocus() { }

    public override void OnInteract() { }

    public override void OnLoseFocus() { }

    /// <summary>
    /// Checks if the disks are in the correct rotation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    /// 
    public void RotatePiece(int rotAmount)
    {
        if (!IsRotating())
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, 0, rotAmount);
            StartCoroutine(RotateTowardsTarget(targetAngle,rotationDuration));
        }
    }
}
