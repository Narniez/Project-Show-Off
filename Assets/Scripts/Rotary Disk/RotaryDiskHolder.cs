using System.Collections.Generic;
using UnityEngine;

public class RotaryDiskHolder : PuzzleAbstract
{
    //Put all disks in the holder into a list
    public List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();

    //Get a reference to the player interface
    IPlayer playerA;

    //Amount by which the piece to be rotated 
    [SerializeField]
    private int rotAmount = 45;

    //Camera controller variable to store the camera controller of the player interacting  
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    int pieceNum = 0;


    //Set the cameracontroller and player interface 
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
        OnCorrectPuzzle();
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

        if (playerA.PlayerAction.FindAction("RotateRight").triggered)
        {
            rotAmount = -45;
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
        if (playerA.PlayerAction.FindAction("RotateLeft").triggered)
        {
            rotAmount = 45;
            //RotatePiece(puzzlePieces[pieceNum]);
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
    }

    void OnCorrectPuzzle()
    {
     

        Debug.Log("Puzzle Done");
    }
    public override void OnInteract()
    {
        
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
