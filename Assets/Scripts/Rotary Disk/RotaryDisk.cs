using System;
using System.Collections;
using UnityEngine;

public class RotaryDisk : PuzzleAbstract 
{
    public Quaternion correctAngle;

    private bool isRotating;
    [SerializeField] private float rotationDuration = 1;

    [HideInInspector] public Quaternion targetAngle;
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
        if (!isRotating)
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, 0, rotAmount);
            StartCoroutine(RotateTowardsTarget(isRotating,targetAngle,rotationDuration));
        }
        //Debug.Log(" Current Z rotation is " + transform.rotation.eulerAngles.z);
        //Debug.Log(" Target Z rotation is: " + targetAngle.eulerAngles.z);
    }
}
