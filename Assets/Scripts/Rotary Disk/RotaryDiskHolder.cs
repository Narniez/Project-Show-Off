using System.Collections.Generic;
using UnityEngine;

public class RotaryDiskHolder : PuzzleAbstract
{
    //Put all disks in the holder into a list
    private List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();

    IPlayer playerA;

    Material normalMat;
    [SerializeField] Material glowMat;

    //Amount by which the piece to be rotated 
    [SerializeField]
    private int rotAmount = 45;

    //Camera controller variable to store the camera controller of the player interacting  
    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    int pieceNum = 0;

    public bool isdone;

    //Set the cameracontroller and player interface 
    public void SetCameraController(CameraController camController, IPlayer player)
    {
        playerA = player;
        if (camController == null) return;
        cameraController = camController;
        //Debug.Log(cameraController);
    }

    private void Start()
    {
        //inputAsset = playerA.
        RotaryDisk[] puzzlePieceObjects = GetComponentsInChildren<RotaryDisk>(true);
        puzzlePieces.AddRange(puzzlePieceObjects);
        normalMat = puzzlePieces[0].GetComponent<Renderer>().materials[0];
    }

    private void Update()
    {


        isdone = false;
        ChangeActivePiece();
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (!puzzlePieces[i].IsCorrect(puzzlePieces[i].transform.localRotation, puzzlePieces[i].correctAngle, Axis.Z))
            {
                return;
            }
        }
        isdone = true;
    }

    void ChangeActivePiece()
    {
        if (cameraController == null || !cameraController.IsLockedOnDisk && !cameraController.IsLockedOnDiskLeft) return;

        //Select a disk that you want to interact with
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

        //Make the selected disk to rotate left or right
        if (playerA.PlayerAction.FindAction("RotateLeft").triggered)
        {
            rotAmount = -45;
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
        if (playerA.PlayerAction.FindAction("RotateRight").triggered)
        {
            rotAmount = 45;
            puzzlePieces[pieceNum].RotatePiece(rotAmount);
        }
        if (cameraController != null)
        {
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                Material pieceMaterial = i == pieceNum ? glowMat : normalMat;
                puzzlePieces[i].GetComponent<Renderer>().material = pieceMaterial;
            }
        }
    }

    public bool OnCorrectPuzzle()
    {
        //Debug.Log("Puzzle Done");
        return isdone;
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
