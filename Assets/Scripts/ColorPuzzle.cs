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
           
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            if (Mathf.Abs(Quaternion.Dot(puzzlePiece.transform.rotation,puzzlePiece.targetAngle)) > precision)
            {
                Debug.Log(puzzlePiece.name + " Piece is in correct orientation");
            }
        }
    }
}
