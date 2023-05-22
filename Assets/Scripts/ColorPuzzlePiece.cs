using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzlePiece : Interactable
{
    public UnityAction<ColorPuzzlePiece> PieceRotated;

    float rotY = 90;
    Vector3 rotation = Vector3.zero;
    public Quaternion targetAngle;

    public override void OnFocus()
    {
        //Debug.Log("Looking at puzzlePiece");
    }

    public override void OnInteract()
    {
        Debug.Log("Interacting");
        RotatePiece();
    }

    public override void OnLoseFocus()
    {

    }

    public void RotatePiece()
    {
        //if (!CameraController.isLockedOnTower) return;
        rotation.y += rotY;
        transform.Rotate(rotation);
        PieceRotated?.Invoke(this);
    }

    void Start()
    {
        //targetAngle = Quaternion.Euler(correctOrientation);

    }

    public bool IsCorrect()
    {
        //Improved with chatGPT
        // Calculate the dot product between the current rotation and the target angle
        float dotProduct = Quaternion.Dot(transform.rotation, targetAngle);
        // Determine if the dot product exceeds the precision threshold
        bool isCorrect = Mathf.Abs(dotProduct) > 0.9999f;
        return isCorrect;
    }
    // Update is called once per frame
    void Update()
    {
        //if (IsCorrect())
        //{
        //    Debug.Log(this.name + " Piece is in the correct position");
        //}
    }
}
