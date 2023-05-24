using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotaryDiskHolder : Interactable
{
    public List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();
   // public InputActionAsset inputAsset;
    private InputActionMap player;

    IPlayer playerA;

    [SerializeField]
    private int rotAmount = 45;
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    int pieceNum = 0;

    public void SetCameraController(CameraController camController, IPlayer player)
    {
        playerA = player;
        if (camController == null) return;
        cameraController = camController;
        //Debug.Log(cameraController);
    }


    public override void Awake()
    {

    }
    private void Start()
    {
        //inputAsset = playerA.
        RotaryDisk[] puzzlePieceObjects = GetComponentsInChildren<RotaryDisk>(true);
        puzzlePieces.AddRange(puzzlePieceObjects);
    }

    private void Update()
    {
        ChangeActivePiece();
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (!puzzlePieces[i].CorrectPuzzlePosition())
            {
                return;
            }
        }
        CorrectPuzzle();
    }

    void ChangeActivePiece()
    {
        if (cameraController == null) return;
        if (!cameraController.IsLockedOnDisk) return;
        if (playerA.PlayerAction.FindAction("CameraDown").triggered)
        {
            Debug.Log("Go to nesxt Piece");
            pieceNum++;
        }
        if (playerA.PlayerAction.FindAction("CameraUp").triggered)
        {
            pieceNum--;
        }

        if (pieceNum <= 0)
        {
            pieceNum = 0;
        }
        if (pieceNum >= puzzlePieces.Count - 1)
        {
            pieceNum = puzzlePieces.Count - 1;
        }

        if (playerA.PlayerAction.FindAction("RotateLeft").triggered)
        {
            rotAmount = -45;
            RotatePiece();
        }
        if (playerA.PlayerAction.FindAction("RotateRight").triggered)
        {
            rotAmount = 45;
            RotatePiece();
        }
    }

    void CorrectPuzzle()
    {
        Debug.Log(puzzlePieces[pieceNum].name + " is in correct position");
    }

    void RotatePiece()
    {
        if (cameraController == null) return;
        if (!cameraController.IsLockedOnDisk) return;
        
        Quaternion currentRotation = puzzlePieces[pieceNum].transform.rotation;
        Quaternion rotationIncrement = Quaternion.Euler(0, 0, (int)rotAmount);
        Quaternion newRotation = currentRotation * rotationIncrement;

        // Round the rotation to the nearest integer value
        Vector3 eulerAngles = newRotation.eulerAngles;
        eulerAngles.z = Mathf.Round(eulerAngles.z);
        puzzlePieces[pieceNum].transform.rotation = Quaternion.Euler(eulerAngles);
        Debug.Log(" Current Z rotation is " + puzzlePieces[pieceNum].transform.rotation.eulerAngles.z);
        Debug.Log(" Target Z rotation is: " + puzzlePieces[pieceNum].targetAngle.eulerAngles.z);
    }
    public override void OnInteract()
    {
        //RotatePiece();
    }

    public override void OnFocus()
    {
        return;
    }

    public override void OnLoseFocus()
    {
        cameraController = null;
        Debug.Log("Lost focus");
    }
}
