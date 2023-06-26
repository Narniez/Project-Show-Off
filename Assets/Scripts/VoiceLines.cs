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
    [SerializeField] private LightController randomLightPuzzle;

    [SerializeField] private int notSolvedTimer;
    private float pTimer;

    private bool colorPuzzleSolved;
    private bool rotDiskPuzzleSolved;
    private bool lightPuzzleSolved;
    private bool randomLightsSolved;

    private void Start()
    {
        pTimer = notSolvedTimer;

        //// Check if puzzles are attached
        //if (colorPuzzle == null || rotDiskPuzzle == null || lightPuzzle == null)
        //{
        //    Debug.Log("One or more puzzles not attached in the VoiceLines script.");
        //}
    }

    private void Update()
    {

        PlayVoiceLines();
       
    }

    private void PlayVoiceLines()
    {
        pTimer -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(pTimer);
        Debug.Log(seconds);

        if (colorPuzzle.isSolved && !colorPuzzleSolved)
        {
            
            colorPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
           
        }
        else if (rotDiskPuzzle.isSolved && !rotDiskPuzzleSolved)
        {
            rotDiskPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
        }
        else if (lightPuzzle != null &&  lightPuzzle.isCompleted && !lightPuzzleSolved)
        {
            lightPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines);
        }
        else if (randomLightPuzzle != null && randomLightPuzzle.IsSolved() && !randomLightsSolved)
        {
            randomLightsSolved = true;
            ChooseLine(puzzleSolvedLines);
        }
        else if (colorPuzzleSolved && rotDiskPuzzleSolved && lightPuzzleSolved)
        {
            audioSource.clip = levelSolvedLines[Random.Range(0, levelSolvedLines.Count)];
            audioSource.Play();
        }
        else if(seconds <= 0)
        {
            ChooseLine(noPuzzleSolvedLines);
        }
    }

    void ChooseLine(List<AudioClip> audioClips)
    {
        pTimer = notSolvedTimer;
        if (audioClips.Count <= 0) return;
        int index = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[index];
        audioSource.Play();
        audioClips.RemoveAt(index);
    }
}