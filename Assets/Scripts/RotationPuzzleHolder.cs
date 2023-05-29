using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleHolder : MonoBehaviour
{
    [SerializeField]
    private List<RotationPuzzle> puzzles = new List<RotationPuzzle>();

    void Start()
    {
        RotationPuzzle[] puzzlePieceObjects = GetComponentsInChildren<RotationPuzzle>(true);
        puzzles.AddRange(puzzlePieceObjects);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            //if (puzzlePieces[i].IsCorrect()) Debug.Log(puzzlePieces[i].name + " is in correct position");
            if (!puzzles[i].OnPuzzleCompleted())
            {
                return;
            }
        }
        LevelCompete();
    }

    void LevelCompete()
    {
        Debug.Log("All puzzles done");
    }
}
