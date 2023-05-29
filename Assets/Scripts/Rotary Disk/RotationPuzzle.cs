using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class RotationPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<RotPuzzlePiece> puzzlePieces = new List<RotPuzzlePiece>();
    public InputActionAsset inputAsset;
    private InputActionMap playerInput;
    [SerializeField]
    private int pieceNum = 0;

    [SerializeField]
    private float rotationSpeed = 5f;
    public bool canInteract = false;
    public UnityAction PuzzleComplete;

    private void Awake()
    {
        playerInput = inputAsset.FindActionMap("Player");
    }
    private void Start()
    {
        //PuzzleComplete += OnPuzzleCompleted;
        RotPuzzlePiece[] puzzlePieceObjects = GetComponentsInChildren<RotPuzzlePiece>(true);
        puzzlePieces.AddRange(puzzlePieceObjects);
    }

    private void OnDestroy()
    {
        //PuzzleComplete -= OnPuzzleCompleted;
    }

    private void OnEnable()
    {
        playerInput.FindAction("Interaction").started += RotatePiece;
    }


    private void OnDisable()
    {
        playerInput.FindAction("Interaction").started -= RotatePiece;
    }

    private void Update()
    {
        ChangeActivePiece();
        
        bool puzzleDone = true;

        //If a piece is not in a correct position update bool to false
        foreach (RotPuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrect())
            {
                puzzleDone = false;
                break;
            }
        }

        if (puzzleDone)
        {
            OnPuzzleCompleted();
        }
    }

    void ChangeActivePiece()
    {
        if (GetComponentInParent<CameraController>() == null) return;
        if (!GetComponentInParent<CameraController>().IsLockedOnDisk) return;
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Debug.Log("Go to next Piece");
                pieceNum++;
            }
            if (Input.GetKeyDown(KeyCode.F2))
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

    public bool  OnPuzzleCompleted()
    {
        Debug.Log("Puzzle complete");
        return true;
    }
    void RotatePiece(InputAction.CallbackContext obj)
    {
        if (GetComponentInParent<CameraController>() == null) return;
        if (GetComponentInParent<CameraController>().IsLockedOnDisk || !canInteract) return;
        RotatePieceCoroutine();
    }

    IEnumerator RotatePieceCoroutine()
    {
        Quaternion currentRotation = puzzlePieces[pieceNum].transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, 0, 45);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;

            // Interpolate the rotation gradually
            Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, t);
            puzzlePieces[pieceNum].transform.rotation = newRotation;

            yield return null;
        }
    }
}
