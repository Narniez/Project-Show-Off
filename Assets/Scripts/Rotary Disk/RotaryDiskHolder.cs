using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RotaryDiskHolder : Interactable
{
    public List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();
    public InputActionAsset inputAsset;
    private InputActionMap player;

    [SerializeField]
    private CameraController cameraController;


    public void SetCameraController(CameraController camController)
    {
        cameraController = camController;
        Debug.Log(cameraController);
    }

    [SerializeField]
    int pieceNum = 0;

    public override void Awake()
    {
        player = inputAsset.FindActionMap("Player");
        
    }
    private void Start()
    {
        
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
        if (player.FindAction("CameraDown").triggered)
        {
            Debug.Log("Go to nesxt Piece");
            pieceNum++;
        }
        if (player.FindAction("CameraUp").triggered)
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
    }

    void CorrectPuzzle()
    {
        Debug.Log("Correct puzzle");
    }

    void RotatePiece()
    {
        if (cameraController == null) return;
        if (!cameraController.IsLockedOnDisk) return;
        Quaternion currentRotation = puzzlePieces[pieceNum].transform.rotation;
        Quaternion rotationIncrement = Quaternion.Euler(0, 0, 45);
        Quaternion newRotation = currentRotation * rotationIncrement;
        puzzlePieces[pieceNum].transform.rotation= newRotation;
    }

    public override void OnInteract()
    {
        RotatePiece();
    }

    public override void OnFocus()
    {
        return;
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
