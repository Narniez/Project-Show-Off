using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleHolder : PuzzleAbstract
{
    [SerializeField] private List<RotaryDiskHolder> puzzles = new List<RotaryDiskHolder>();

    private bool isCompleted;

    public bool isSolved { get => isCompleted; private set => isCompleted = value;}

    void Start()
    {
        RotaryDiskHolder[] puzzlePieceObjects = GetComponentsInChildren<RotaryDiskHolder>(true);
        puzzles.AddRange(puzzlePieceObjects);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            if (!puzzles[i].OnCorrectPuzzle())  // if one of the puzzles is wrong, return
            {
                return;
            }
        }
        LevelCompete();
    }

    void LevelCompete()
    {
        // If already completed, return early
        if (isCompleted) return;

        isCompleted = true;
        Debug.Log("All puzzles done");
    }

    public override void OnInteract()
    {
        
    }

    public override void OnFocus()
    {
        
    }

    public override void OnLoseFocus()
    {
        
    }
}
