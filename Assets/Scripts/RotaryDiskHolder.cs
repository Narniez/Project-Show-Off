using System.Collections.Generic;
using UnityEngine;

public class RotaryDiskHolder : MonoBehaviour
{
    public List<IRotaryDiskPuzzle> puzzlePieces = new List<IRotaryDiskPuzzle>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            for (int j = 0; j < childTransform.childCount; j++)
            {
                IRotaryDiskPuzzle puzzlePiece = childTransform.GetChild(j).GetComponent<IRotaryDiskPuzzle>();

                if (puzzlePiece != null && !puzzlePieces.Contains(puzzlePiece))
                {
                    puzzlePieces.Add(puzzlePiece);
                }
            }
        }


    }

    private void Update()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            if (!puzzlePieces[i].CorrectPuzzlePosition())
            {
                return;
            }
        }

        CorrectPuzzle();
    }

    void CorrectPuzzle()
    {
        Debug.Log("Correct puzzle");
        // Implement an event or perform any necessary actions when the puzzle is correct
    }
}
