using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VoiceLines : MonoBehaviour
{
    [SerializeField] private List<AudioClip> puzzleSolvedLines;
    [SerializeField] private List<AudioClip> levelSolvedLines;
    [SerializeField] private List<AudioClip> noPuzzleSolvedLines;


    [SerializeField] private List<string> puzzleSolvedSubs;
    [SerializeField] private List<string> levelSolvedSubs;
    [SerializeField] private List<string> noPuzzleSolvedSubs;

    [SerializeField] private List<string> startVoiceLineSubs;

    [SerializeField] private TextMeshProUGUI subtitles;
    [SerializeField] private GameObject subsPanel;

    public AudioSource audioSource;


    [SerializeField] private CubePlatesHolder platesHolder;
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
    private bool levelSolved;

    private bool isDisplayingSubtitles = false; // Track if subtitles are currently being displayed

    public static bool startVoiceLineTimer = false;

    bool displayStartSubs = true;

    PlayerManager playerManager;

    private void Start()
    {
        pTimer = notSolvedTimer;
        playerManager = FindObjectOfType<PlayerManager>();

        //// Check if puzzles are attached
        //if (colorPuzzle == null || rotDiskPuzzle == null || lightPuzzle == null)
        //{
        //    Debug.Log("One or more puzzles not attached in the VoiceLines script.");
        //}
    }

    private void FixedUpdate()
    {

        PlayVoiceLines();

        if (playerManager.PlayersConnected() && displayStartSubs && startVoiceLineTimer)
        {
            PlayStartVoiceLines();
        }
    }

    private void PlayVoiceLines()
    {
        if (startVoiceLineTimer)
        {
            pTimer -= Time.deltaTime;
        }
        float seconds = Mathf.FloorToInt(pTimer);

        if (colorPuzzle.isSolved && !colorPuzzleSolved)
        {

            colorPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines, puzzleSolvedSubs);

        }
        else if (rotDiskPuzzle.isSolved && !rotDiskPuzzleSolved)
        {
            rotDiskPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines, puzzleSolvedSubs);
        }
        else if (lightPuzzle != null && lightPuzzle.isCompleted && !lightPuzzleSolved)
        {
            lightPuzzleSolved = true;
            ChooseLine(puzzleSolvedLines, puzzleSolvedSubs);
        }
        else if (randomLightPuzzle != null && randomLightPuzzle.IsSolved() && !randomLightsSolved)
        {
            randomLightsSolved = true;
            ChooseLine(puzzleSolvedLines, puzzleSolvedSubs);
        }
        else if (platesHolder.levelSolved && !levelSolved)
        {
            ChooseLine(levelSolvedLines, levelSolvedSubs);
            levelSolved = true;
        }
        else if (seconds <= 0)
        {
            ChooseLine(noPuzzleSolvedLines, noPuzzleSolvedSubs);
        }
    }

    void ChooseLine(List<AudioClip> audioClips, List<string> subsList)
    {
        pTimer = notSolvedTimer;
        if (audioClips.Count <= 0) return;
        int index = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[index];
        audioSource.Play();
        audioClips.RemoveAt(index);
        DisplaySubtitles(subsList[index]);
        subsList.RemoveAt(index);
    }

    void DisplaySubtitles(string subtitle)
    {
        StartCoroutine(DisplaySubtitlesCoroutine(subtitle));
    }

    IEnumerator DisplaySubtitlesCoroutine(string subtitle)
    {
        isDisplayingSubtitles = true; // Set the flag to indicate that subtitles are being displayed

        subsPanel.SetActive(true);
        subtitles.text = string.Empty;
        for (int i = 0; i < subtitle.Length; i++)
        {
            subtitles.text += subtitle[i];
            yield return new WaitForSeconds(0.05f); // Adjust the delay between characters if needed
        }

        yield return new WaitForSeconds(3f); // Keep the subtitles on the screen for 3 seconds

        subtitles.text = string.Empty; // Clear the subtitles after 3 seconds
        subsPanel.SetActive(false);

        isDisplayingSubtitles = false; // Reset the flag to indicate that subtitles are no longer being displayed
    }

    private void PlayStartVoiceLines()
    {
        if (isDisplayingSubtitles)
        {
            return; // If subtitles are already being displayed, ignore the start voice lines
        }

        if (startVoiceLineSubs.Count > 0)
        {
            string subtitle = startVoiceLineSubs[0];
            startVoiceLineSubs.RemoveAt(0);

            StartCoroutine(DisplayStartVoiceLineSubtitle(subtitle));
        }
        else
        {
            displayStartSubs = false;
        }
    }

    IEnumerator DisplayStartVoiceLineSubtitle(string subtitle)
    {
        isDisplayingSubtitles = true; // Set the flag to indicate that subtitles are being displayed

        subsPanel.SetActive(true);
        subtitles.text = string.Empty;
        for (int i = 0; i < subtitle.Length; i++)
        {
            subtitles.text += subtitle[i];
            yield return new WaitForSeconds(0.05f); // Adjust the delay between characters if needed
        }

        yield return new WaitForSeconds(1.5f); // Keep the subtitles on the screen for 3 seconds

        subtitles.text = string.Empty; // Clear the subtitles after 3 seconds
        subsPanel.SetActive(false);

        isDisplayingSubtitles = false; // Reset the flag to indicate that subtitles are no longer being displayed

        // Proceed to the next start voice line after a short delay
        yield return new WaitForSeconds(0.7f);
        PlayStartVoiceLines();
    }


}