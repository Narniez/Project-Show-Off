using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotaryDiskHolder : Interactable
{
    public List<RotaryDisk> puzzlePieces = new List<RotaryDisk>();
    public InputActionAsset inputAsset;
    private InputActionMap player;

    IPlayer playerA;

    float transitionSpeed = 5f;
    private Quaternion targetRotation;
    private float rotationProgress = 0f;


    [SerializeField]
    float rotationSpeed = 100f;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    int pieceNum = 0;

    public void SetCameraController(CameraController camController, IPlayer player)
    {
        playerA = player;
        if (camController == null) return;
        cameraController = camController;
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

        if (playerA.PlayerAction.FindAction("Interaction").triggered)
        {
            RotatePiece();
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
        puzzlePieces[pieceNum].transform.rotation = newRotation;
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
