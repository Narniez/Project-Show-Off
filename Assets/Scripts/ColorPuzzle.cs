using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzle : Interactable
{
    public UnityAction PuzzleComplete; 

   // public ColorPuzzlePiece[] puzzlePieces;
    private List<ColorPuzzlePiece> puzzlePieces = new List<ColorPuzzlePiece>();

    public override void OnFocus()
    {
       
    }

    public override void OnInteract()
    {
         
    }

    public override void OnLoseFocus()
    {
       
    }

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

        //If a piece is not in a correct position update bool to false
        foreach (ColorPuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrect())
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
            Debug.Log("All puzzle pieces are correct!");
    }
}
