using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotaryDiskHolder : PuzzleAbstract
{
    public List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();
   // public InputActionAsset inputAsset;
    IPlayer playerA;

    Quaternion newRotation;
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
            if (!puzzlePieces[i].IsCorrect(puzzlePieces[i].transform.rotation,puzzlePieces[i].correctAngle,Axis.Z))
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
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
        if (playerA.PlayerAction.FindAction("RotateRight").triggered)
        {
            rotAmount = 45;
            //RotatePiece(puzzlePieces[pieceNum]);
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
    }

    void CorrectPuzzle()
    {
        Debug.Log(" All pieces are in correct position");
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
