using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    /// <summary>
    /// NOT IN USE YET
    /// </summary>
    private List<PlayerInput> players = new List<PlayerInput>();

    private int playerCount = 0;

    [SerializeField]
    private List<Transform> startingPoints;

    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponentInParent<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerLeft -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        playerCount++;
        Debug.Log("Player Joined");
        players.Add(player);
        player.transform.position = startingPoints[players.Count - 1].position;
        if (playerCount == 2)
        {
            player.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
            player.GetComponent<InteractionHandler>().uiPosition = new Vector3(0.5f, 0, 0);
        }
    }
}
