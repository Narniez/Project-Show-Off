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
    public TextMeshProUGUI playerReadyText;

    private void Awake()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();

        // Disable the canvas and pre-game camera initially
        readyPannel.SetActive(true);
        uiCamera.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
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

            timerText.text = seconds.ToString("00:00");

            if (seconds <= 0)
            {
                readyPannel.SetActive(false);
                uiCamera.gameObject.SetActive(false);
                foreach(PlayerInput player in players)
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

        if(playerCount == 1)
        {
            playerReadyText.text = "Player Ready: 1/2";
        }
        Debug.Log("Player Joined");
        player.transform.position = startingPoints[players.Count - 1].position;
        if (playerCount == 2)
        {
            playerReadyText.text = "Player Ready: 2/2";
            player.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
            player.GetComponent<InteractionHandler>().uiPosition = new Vector3(0.5f, 0, 0);
            
        }
    }
}
