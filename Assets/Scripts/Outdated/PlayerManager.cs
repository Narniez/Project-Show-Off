using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    /// <summary>
    /// NOT IN USE YET
    /// </summary>

    /// 
    private List<PlayerInput> players = new List<PlayerInput>();

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

    [SerializeField] private float minTimerTextSize = 300f; // Minimum size for the timer text
    [SerializeField] private float maxTimerTextSize = 900f; // Maximum size for the timer text
    [SerializeField] private float timerTextScaleSpeed = 4f; // Speed at which the timer text scales

    private void Awake()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();

        // Disable the canvas and pre-game camera initially
        readyPannel.SetActive(true);
        uiCamera.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        playerOneReadyText.SetActive(false);
        playerTwoReadyText.SetActive(false);
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
        if (playerCount == 2)
        {
            timerText.gameObject.SetActive(true);
            timeToStart -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeToStart);

            timerText.text = seconds.ToString();
            // Scale the timer text
            float timerTextSize = Mathf.Lerp(minTimerTextSize, maxTimerTextSize, (Mathf.Sin(Time.time * timerTextScaleSpeed) + 1f) / 2f);
            timerText.fontSize = (int)timerTextSize;

            if (seconds <= 0)
            {
                readyPannel.SetActive(false);
                uiCamera.gameObject.SetActive(false);
                foreach (PlayerInput player in players)
                {
                    player.GetComponent<PlayerControls>().canMoveAtStart = true;
                }
            }
        }
    }

    public void AddPlayer(PlayerInput player)
    {
        playerCount++;
        players.Add(player);

        if (playerCount == 1)
        {
            playerOneReadyText.SetActive(true);
        }
        Debug.Log("Player Joined");
        player.transform.position = startingPoints[players.Count - 1].position;
        if (playerCount == 2)
        {
            playerTwoReadyText.SetActive(true);
            player.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
            player.GetComponent<InteractionHandler>().uiPosition = new Vector3(0.5f, 0, 0);

        }
    }
}
