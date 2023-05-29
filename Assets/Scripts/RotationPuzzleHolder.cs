using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleHolder : MonoBehaviour
{
    [SerializeField] private List<RotaryDiskHolder> puzzles = new List<RotaryDiskHolder>();

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
        Debug.Log("All puzzles done");
    }
}
