using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    [SerializeField] private List<AudioClip> puzzleSovledLines;
    [SerializeField] private List<AudioClip> levelSolvedLines;
    [SerializeField] private List<AudioClip> noPuzzleSovedLines;
    public AudioSource audioSource;

    [SerializeField] private ColorPuzzle colorPuzzle;
    [SerializeField] private RotaryDiskHolder rotDiskPuzzle;
    [SerializeField] private LightPuzzle lightPuzzle;

    [SerializeField] private int notSolvedTimer;
    private float pTimer;
    [SerializeField] private int onPuzzleLockedTimer;

    void Start()
    {
        pTimer = notSolvedTimer;
    }

    // Update is called once per frame
    void Update()
    {
        pTimer -= Time.deltaTime;
        int seconds = Mathf.FloorToInt(pTimer);
        Debug.Log(seconds);
        if (colorPuzzle == null || rotDiskPuzzle == null || lightPuzzle == null)
        {
            Debug.Log("One or more puzzles not attached in the VoiceLines");
            return;
        }

        if (colorPuzzle.isSolved && rotDiskPuzzle.isdone && lightPuzzle.isCompleted)
        {
            audioSource.clip = levelSolvedLines[Random.Range(0, levelSolvedLines.Count)];
            audioSource.Play();
        }

        if (colorPuzzle.isSolved || rotDiskPuzzle.isdone || lightPuzzle.isCompleted)
        {
            pTimer = notSolvedTimer;
            audioSource.clip = puzzleSovledLines[Random.Range(0, levelSolvedLines.Count)];
            audioSource.Play();
        }

        if(seconds <= 0)
        {
            Debug.Log("PlayLine");
            audioSource.clip = noPuzzleSovedLines[Random.Range(0, levelSolvedLines.Count)];
            audioSource.Play();
        }
    }
}
