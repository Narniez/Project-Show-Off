using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    public ColorPuzzlePiece[] puzzlePieces;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ColorPuzzlePiece puzzlePiece in puzzlePieces)
        {
            if(Mathf.Abs(puzzlePiece.transform.rotation.eulerAngles.y) == Mathf.Abs(puzzlePiece.correctOrientation.y))
            {
                Debug.Log(puzzlePiece.name + " Piece is in correct orientation");
            }
        }
    }
}
