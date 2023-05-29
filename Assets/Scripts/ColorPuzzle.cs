using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class ColorPuzzle : PuzzleAbstract
{
    //Action called upon puzzle completion
    public UnityAction PuzzleComplete;

    [SerializeField] private GameObject doorObject;

    //List holding all the pieces of the puzzle
    [SerializeField] private List<ColorPuzzlePiece> puzzlePieces = new List<ColorPuzzlePiece>();

    //Object to instantiate on a set position
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private Transform instantiationPosition;

   
    private bool isCompleted = false;

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
            if (!piece.IsCorrect(piece.transform.rotation, piece.correctAngle, Axis.Y))
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



    void OnPuzzleCompleted()
    {
        // If already completed, return early
        if (isCompleted) return;

        isCompleted = true;

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
