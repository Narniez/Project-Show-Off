using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzle : PuzzleAbstract
{
    public UnityAction PuzzleComplete;
    [SerializeField] private GameObject doorObject;

    [SerializeField]
    private List<ColorPuzzlePiece> puzzlePieces = new List<ColorPuzzlePiece>();

    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private Transform instantiationPosition;

    private bool isCompleted = false;

    void Start()
    {
        PuzzleComplete += OnPuzzleCompleted;

        // Find and add puzzle pieces to the list
        ColorPuzzlePiece[] puzzlePieceObjects = GetComponentsInChildren<ColorPuzzlePiece>(true);
        puzzlePieces.AddRange(puzzlePieceObjects);

        // Subscribe to the puzzle piece events
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            puzzlePiece.PieceRotated += OnPieceRotated;
        }
    }

    private void OnDestroy()
    {
        PuzzleComplete -= OnPuzzleCompleted;

        // Unsubscribe from the puzzle piece events
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            puzzlePiece.PieceRotated -= OnPieceRotated;
        }
    }

    private void OnPieceRotated(ColorPuzzlePiece puzzlePiece)
    {
        bool allPiecesCorrect = true;

        // Check if all puzzle pieces are in the correct position
        foreach (ColorPuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrect(piece.transform.rotation, piece.targetAngle, Axis.Y))
            {
                allPiecesCorrect = false;
                break;
            }
            else { 
                PuzzleComplete?.Invoke();
            }
        }

        if (allPiecesCorrect)
        {
        }
    }

    void OnPuzzleCompleted()
    {
        if (isCompleted) return; // If already completed, return early

        isCompleted = true; // Set the completion flag to true

        if (doorObject != null)
        {
            doorObject.SetActive(false);
        }

        // Instantiate the object at the specified position
        if (objectToInstantiate != null && instantiationPosition != null)
        {
            Instantiate(objectToInstantiate, instantiationPosition.position, instantiationPosition.rotation);
        }

        Debug.Log("Puzzle Done");
    }

    public override void OnInteract() { }

    public override void OnFocus() { }

    public override void OnLoseFocus() { }
}
