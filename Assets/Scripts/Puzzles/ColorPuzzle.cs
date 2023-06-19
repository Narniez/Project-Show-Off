using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzle : PuzzleAbstract
{
    //Action called upon puzzle completion
    public UnityAction PuzzleComplete;

    //List holding all the pieces of the puzzle
    [SerializeField] private List<ColorPuzzlePiece> puzzlePieces = new List<ColorPuzzlePiece>();
   
    private bool isCompleted = false;
    public bool isSolved { get => isCompleted; private set => isCompleted = value;}

    void Start()
    {
        //Subscirbe the OnPuzzleComleted to the puzzle complete event action
        PuzzleComplete += OnPuzzleCompleted;

        // Find and add puzzle pieces to the list
        ColorPuzzlePiece[] puzzlePieceObjects = GetComponentsInChildren<ColorPuzzlePiece>(true);
        puzzlePieces.AddRange(puzzlePieceObjects);

        // Subscribe each piece to the PieceRitated event action
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            puzzlePiece.PieceRotated += OnPieceRotated;
        }
    }

    private void OnDestroy()
    {
        //Unsubscribe
        PuzzleComplete -= OnPuzzleCompleted;

        // Unsubscribe from the puzzle piece events
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            puzzlePiece.PieceRotated -= OnPieceRotated;
        }
    }

    //Check if all puzzle pieces are in the correct orientation after a delay
    private IEnumerator DelayedRotationCheck(float time)
    {
        yield return new WaitForSeconds(time);

        bool allPiecesCorrect = true;
        foreach (ColorPuzzlePiece piece in puzzlePieces)
        {
            if (!piece.IsCorrect(piece.transform.localRotation, piece.correctAngle, Axis.Y))
            {
                allPiecesCorrect = false;
                break;
            }
        }

        //If all pieces are correct invoke the puzzle complete event 
        if (allPiecesCorrect)
        {
            PuzzleComplete?.Invoke();
        }

    }

    private void OnPieceRotated()
    {
        StartCoroutine(DelayedRotationCheck(1));
    }

    public bool IsSolved()
    {
        return isCompleted;
    }

    void OnPuzzleCompleted()
    {
        // If already completed, return early
        if (isCompleted) return;

        isCompleted = true;
        Debug.Log("Puzzle Done");
    }

    public override void OnInteract() { }

    public override void OnFocus() { }

    public override void OnLoseFocus() { }
}
