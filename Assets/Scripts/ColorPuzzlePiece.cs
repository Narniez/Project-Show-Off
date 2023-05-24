using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColorPuzzlePiece : PuzzleAbstract
{
    public UnityAction<ColorPuzzlePiece> PieceRotated;

    [SerializeField]
    private int rotAmount = 90;

    [SerializeField] private float rotationDuration = 1;
    public Quaternion correctAngle;

    [HideInInspector]public Quaternion targetAngle;
    private bool isRotating;

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
        if (IsCorrect(correctAngle, targetAngle, Axis.Y))
        {
            Debug.Log(this.name + " Puzzle piece is in the correct orientation.");
        }
    }

    public void RotatePiece()
    {
        //if(playerInput.PlayerAction.FindAction(""))
        //if (!CameraController.isLockedOnTower) return;
        if (!isRotating)
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, rotAmount, 0);
            StartCoroutine(RotateTowardsTarget(isRotating,targetAngle,rotationDuration));
        }

        PieceRotated?.Invoke(this);
    }
   
}
