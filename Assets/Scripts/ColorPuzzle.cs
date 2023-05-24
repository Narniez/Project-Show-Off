using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class ColorPuzzle : PuzzleAbstract
{
    public UnityAction PuzzleComplete;
    [SerializeField] private GameObject doorObject;

    private List<ColorPuzzlePiece> puzzlePieces = new List<ColorPuzzlePiece>();

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
            if (!piece.IsCorrect(piece.correctAngle, piece.targetAngle, Axis.Y))
            {
                allPiecesCorrect = false;
                break;
            }
        }

        if (allPiecesCorrect)
        {
            PuzzleComplete?.Invoke();
        }
    }

    void OnPuzzleCompleted()
    {

        if (doorObject != null)
        {
            doorObject.SetActive(false);
        }

    }

    public override void OnInteract() { }

    public override void OnFocus() { }

    public override void OnLoseFocus() { }
}
