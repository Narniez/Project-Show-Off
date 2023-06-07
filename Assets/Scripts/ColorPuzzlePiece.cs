using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColorPuzzlePiece : PuzzleAbstract
{
    public UnityAction PieceRotated;

    [SerializeField] private int rotAmount = 90;

    [SerializeField] private float rotationDuration = 1;
    public Quaternion correctAngle;

    [HideInInspector]public Quaternion targetAngle;

    [SerializeField]
    public bool isCorrect;

    public override void OnFocus()
    {
        //Debug.Log("Looking at puzzlePiece");
    }

    //Override the OnInteract method to call RotatePiece
    public override void OnInteract()
    {
        RotatePiece();
    }

    public override void OnLoseFocus()
    {

    }

    private void FixedUpdate()
    {
        isCorrect = IsCorrect(transform.localRotation, correctAngle, Axis.Y);
        if (IsCorrect(transform.localRotation, correctAngle, Axis.Y))
        {
           //Debug.Log(this.name + " Puzzle piece is in the correct orientation.");
        }
    }

    //If we are not rotating, set the target angle and rotation duration, start rotation and Invoke PieceRotated event 
    public void RotatePiece()
    {
        if (!IsRotating())
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, rotAmount, 0);
            StartCoroutine(RotateTowardsTarget(targetAngle,rotationDuration));
        }
        PieceRotated?.Invoke();
    }
}
