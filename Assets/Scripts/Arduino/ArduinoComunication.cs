using System.Threading;
using System.IO.Ports;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Can be attached to every object
/// </summary>
public class ArduinoComunication : MonoBehaviour
{
    public static ArduinoComunication instance;
    public AudioClip playersLeftClip;
    public AudioClip playerLeftClip;

    public bool arduinoTurned;

    [SerializeField] string comPort = "COM5";
    private SerialPort dataStream;
    private Queue<string> receivedDataQueue;

    private float timer = 60f; // Timer variable to track the elapsed time
    private float _resetLevelTimer = 10f; // Timer variable to track the elapsed time

    private int player1, player2;
    private bool bothPlayersHere;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        dataStream = new SerialPort(comPort, 9600);
        dataStream.ReadTimeout = 200; // Set a higher timeout value in milliseconds

        try
        {
            dataStream.Open();
            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to open the serial port: " + ex.Message);
        }

        receivedDataQueue = new Queue<string>();

        // Start a separate thread for reading data from the serial port
        Thread readThread = new Thread(ReadData);
        readThread.Start();
    }

    private void ReadData()
    {
        while (dataStream.IsOpen)
        {
            try
            {
                string data = dataStream.ReadLine();
                if (!string.IsNullOrEmpty(data))
                {
                    receivedDataQueue.Enqueue(data);
                }
            }
            catch (System.TimeoutException)
            {
                // Handle timeout exception if needed
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to read data from the serial port: " + ex.Message);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma)) arduinoTurned = !arduinoTurned;
        if (!arduinoTurned || Time.timeScale == 0 || SceneManager.GetActiveScene().buildIndex == 0) return;

        Debug.Log("Player1 " + player1 + " " + "Player2 " + player2);
        

        if (receivedDataQueue.Count > 0)
        {
            while (receivedDataQueue.TryDequeue(out string data))
            {
                // Process the received data
                string[] players = data.Split(',');
                player1 = int.Parse(players[0]);
                player2 = int.Parse(players[1]);
                Debug.Log("Received data: player1 " + player1);
                Debug.Log("Received data: player2 " + player2);
            }
        }

        ResetLevel();
        WaitingForThePlayerToComeBack();

        if (player1 == 1 && player2 == 1) {
            Debug.Log("tuka sa");
            bothPlayersHere = true;
            // timerUI.gameObject.SetActive(false);
        }
        // Continue with the rest of your update logic
    }

    void ResetLevel()
    {
        if (player1 == 0 && player2 == 0)
        {
            //SoundEffects.instance.PlaySoundEffect(playersLeftClip);
            // timerUI.gameObject.SetActive(true);
            _resetLevelTimer -= Time.deltaTime; // Update the timer each frame
            int seconds = Mathf.FloorToInt(_resetLevelTimer);
            // timerUI.text = "Reset in " + seconds.ToString("00:00");
            Debug.Log("Timer: " + seconds + " seconds");
            if (seconds <= 0)
            {
                //Reset the level
                SoundEffects.instance.StopSoundEffect();
                SceneManager.LoadScene(0);
                Debug.Log("reset the level");
                _resetLevelTimer = 10f;
            }
        }
        else
        {
            _resetLevelTimer = 10f;
            // timerUI.gameObject.SetActive(false);
        }
    }

    void WaitingForThePlayerToComeBack()
    {
        //if (player1 != 0 || player2 != 0)
        //{
        //    //timerUI.gameObject.SetActive(false);
        //    //return;
        //}

        if (player1 == 0 || player2 == 0)
        {
            //SoundEffects.instance.PlaySoundEffect(playerLeftClip);
            Debug.Log("trqbva da pleiva");

            timer -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timer);

            Debug.Log("Waiting for the player" + "Timer: " + seconds + " seconds");

            if (seconds <= 0)
            {
                //Reset the level
                SoundEffects.instance.StopSoundEffect();

                SceneManager.LoadScene(0);

                Debug.Log("stop waiting maybe!?");
            }
        }
        else timer = 60;
    }
}
