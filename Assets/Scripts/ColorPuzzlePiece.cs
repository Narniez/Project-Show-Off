using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColorPuzzlePiece : PuzzleAbstract
{
    public UnityAction<ColorPuzzlePiece> PieceRotated;

    [SerializeField] private int rotAmount = 90;

    [SerializeField] private float rotationDuration = 1;
    public Quaternion correctAngle;

    [HideInInspector]public Quaternion targetAngle;

    public override void OnFocus()
    {
        //Debug.Log("Looking at puzzlePiece");
    }

    public override void OnInteract()
    {
        RotatePiece();
    }

    public override void OnLoseFocus()
    {

    }

    private void FixedUpdate()
    {
        if (IsCorrect(transform.rotation, correctAngle, Axis.Y))
        {
            //Debug.Log(this.name + " Puzzle piece is in the correct orientation.");
        }
    }

    public void RotatePiece()
    {
        if (!IsRotating())
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, rotAmount, 0);
            StartCoroutine(RotateTowardsTarget(targetAngle,rotationDuration));
        }
        PieceRotated?.Invoke(this);
    }
}
