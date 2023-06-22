using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    [SerializeField] private List<AudioClip> puzzleSolvedLines;
    [SerializeField] private List<AudioClip> levelSolvedLines;
    [SerializeField] private List<AudioClip> noPuzzleSolvedLines;
    public AudioSource audioSource;

    [SerializeField] private ColorPuzzle colorPuzzle;
    [SerializeField] private RotationPuzzleHolder rotDiskPuzzle;
    [SerializeField] private LightPuzzle lightPuzzle;

    [SerializeField] private int notSolvedTimer;
    private float pTimer;

    private bool colorPuzzleSolved;
    private bool rotDiskPuzzleSolved;
    private bool lightPuzzleSolved;

    private void Start()
    {
        pTimer = notSolvedTimer;

        // Check if puzzles are attached
        if (colorPuzzle == null || rotDiskPuzzle == null || lightPuzzle == null)
        {
            Debug.LogError("One or more puzzles not attached in the VoiceLines script.");
        }

    }

    private void Update()
    {

        PlayVoiceLines();
       
    }

    private void PlayVoiceLines()
    {
        pTimer -= Time.deltaTime;
        float seconds = Mathf.CeilToInt(pTimer);
        Debug.Log(seconds);

        if (colorPuzzle.isSolved && !colorPuzzleSolved)
        {
            pTimer = notSolvedTimer;
            colorPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
           
        }
        else if (rotDiskPuzzle.isSolved && !rotDiskPuzzleSolved)
        {
            pTimer = notSolvedTimer;
            rotDiskPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
        }
        else if (lightPuzzle.isCompleted && !lightPuzzleSolved)
        {
            pTimer = notSolvedTimer;
            lightPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
        }
        else if (colorPuzzleSolved && rotDiskPuzzleSolved && lightPuzzleSolved)
        {
            audioSource.clip = levelSolvedLines[Random.Range(0, levelSolvedLines.Count)];
            audioSource.Play();
        }
        else if(seconds <= 0)
        {
            ChooseLine(noPuzzleSolvedLines);;
            pTimer = notSolvedTimer;
        }
    }

    void ChooseLine(List<AudioClip> audioClips)
    {
        int index = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[index];
        audioSource.Play();
        audioClips.RemoveAt(index);

    }

}