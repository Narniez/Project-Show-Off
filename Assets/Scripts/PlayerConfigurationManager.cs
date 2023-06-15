using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;


    [SerializeField] private int maxPlayers = 2;


    public static PlayerConfigurationManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Only one instance of singleton can be created");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        playerConfigs[index].playerMat = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if(playerConfigs.Count == maxPlayers && playerConfigs.TrueForAll(playerConfigs => playerConfigs.isReady))
        {

        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        pi.transform.SetParent(transform);
        if(!playerConfigs.TrueForAll(p => p.playerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public class PlayerConfiguration
    {
        public PlayerConfiguration(PlayerInput pi)
        {
            playerIndex = pi.playerIndex;
            playerInput = pi;
        }

        public PlayerInput playerInput { get; set; }

        public int playerIndex { get; set; }

        public bool isReady { get; set; }

        public Material playerMat { get; set; }

    }
}
