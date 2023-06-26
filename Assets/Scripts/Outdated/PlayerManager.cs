using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();

    private List<Gamepad> gamepads = new List<Gamepad>();

    private int playerCount = 0;

    [SerializeField]
    private List<Transform> startingPoints;

    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    [SerializeField] private GameObject readyPannel;
    [SerializeField] private Camera uiCamera;


    [SerializeField] float timeToStart = 3;
    public TextMeshProUGUI timerText;
    public GameObject playerOneReadyText;
    public GameObject playerTwoReadyText;

    private float currentSeconds = 0;
    [SerializeField] private float initialTimerTextSize = 100f; // Initial size for the timer text
    [SerializeField] private float targetTimerTextSize = 400f; // Target size for the timer text
    [SerializeField] private float timerTextScaleDuration = 1f; // Duration in seconds to scale the timer text

    private float scaleTimerStartTime = 0f; // Time when the timer text scaling started

    bool activateTimer = true;

    public GameObject panel;

    private void Start()
    {
        if (Gamepad.all.Count > 1)
        {
            gamepads.AddRange(Gamepad.all);
            InputSystem.EnableDevice(gamepads[1]);
        }
    }

    private void Awake()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();

        // Disable the canvas and pre-game camera initially
        readyPannel.SetActive(true);
        uiCamera.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        playerOneReadyText.SetActive(false);
        playerTwoReadyText.SetActive(false);
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerLeft -= AddPlayer;
    }

    private void Update()
    {
        if (playerCount == 2 && activateTimer)
        {
            timerText.gameObject.SetActive(true);
            timeToStart -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeToStart);

            if (currentSeconds != seconds)
            {
                currentSeconds = seconds;
                timerText.fontSize = (int)initialTimerTextSize; // Reset the font size for each new number
                scaleTimerStartTime = Time.time; // Record the start time of the timer text scaling
            }

            timerText.text = seconds.ToString();

            if (seconds <= 0)
            {
                readyPannel.SetActive(false);
                uiCamera.gameObject.SetActive(false);
                activateTimer = false;
                foreach (PlayerInput player in players)
                {
                    player.GetComponent<PlayerControls>().canMoveAtStart = true;
                }
            }

            if (!activateTimer) return;
            // Scale the timer text
            float elapsedTime = Time.time - scaleTimerStartTime;
            float timerTextSize = Mathf.Lerp(initialTimerTextSize, targetTimerTextSize, elapsedTime / timerTextScaleDuration);
            timerText.fontSize = (int)timerTextSize;
        }
    }

    public IEnumerator DeactivateUI()
    {
        yield return new WaitForSeconds(.8f);
        readyPannel.SetActive(false);
        uiCamera.gameObject.SetActive(false);
    }

    public void AddPlayer(PlayerInput player)
    {
        Debug.Log("Player Joined");
        playerCount++;
        players.Add(player);

        if (playerCount == 1)
        {
            playerOneReadyText.SetActive(true);
        }
        player.transform.position = startingPoints[players.Count - 1].position;
        if (playerCount == 2)
        {
            playerTwoReadyText.SetActive(true);
            player.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
            player.GetComponent<PlayerUI>().uiPosition = new Vector3(0.5f, 0, 0);
        }
    }
}
