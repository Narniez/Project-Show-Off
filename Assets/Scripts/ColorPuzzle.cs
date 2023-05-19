using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : Interactable
{
    public ColorPuzzlePiece[] puzzlePieces;
    float precision = 0.9999f;

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

    }

    // Update is called once per frame
    void Update()
    {
        bool allPiecesCorrect = true;

        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            float dotProduct = Quaternion.Dot(puzzlePiece.transform.rotation, puzzlePiece.targetAngle);
            bool isCorrect = Mathf.Abs(dotProduct) > precision;

            if (isCorrect)
            {
               // Debug.Log(puzzlePiece.name + " Piece is in correct orientation");
            }

            puzzlePiece.isCorrect = isCorrect;
            allPiecesCorrect &= isCorrect;
        }

        if (allPiecesCorrect)
        {
            Debug.Log("All puzzle pieces are correct!");
            // Do something when all puzzle pieces are correct
        }
    }
}
