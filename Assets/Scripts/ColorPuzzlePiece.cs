using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColorPuzzlePiece : PuzzleAbstract
{
    public UnityAction<ColorPuzzlePiece> PieceRotated;

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
            targetAngle = transform.rotation * Quaternion.Euler(0, 90, 0);
            StartCoroutine(RotateTowardsTarget());
        }

        PieceRotated?.Invoke(this);
    }

    //AI!
    IEnumerator RotateTowardsTarget()
    {
        isRotating = true;
        Quaternion startingRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startingRotation, targetAngle, elapsedTime / rotationDuration);
            yield return null;
        }
        Vector3 e = transform.rotation.eulerAngles;
        e.y = Mathf.Round(e.y);
        transform.rotation = Quaternion.Euler(e);
        isRotating = false;

       
    }

    // Start the rotation coroutine

    //public bool IsCorrect(Quaternion correctAngle, Quaternion targetAngle)
    //{
    //    //Improved with chatGPT
    //    // Calculate the dot product between the current rotation and the target angle
    //    //float dotProduct = Quaternion.Dot(targetAngle, correctAngle);
    //    //// Determine if the dot product exceeds the precision threshold
    //    //bool isCorrect = Mathf.Abs(dotProduct) > 0.9999f;
    //    //return isCorrect;

    //    bool isCorrect = correctAngle.eulerAngles.y == targetAngle.eulerAngles.y;
    //    return isCorrect;
    //}
}
