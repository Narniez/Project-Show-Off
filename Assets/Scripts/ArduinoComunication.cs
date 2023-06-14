using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO.Ports;
using UnityEngine;

public class ArduinoComunication : MonoBehaviour
{
    
    string usedPort=null;

    [SerializeField] int comNum = 3;
    
    SerialPort dataStream; 
    string recievedData;

    bool isPlayer1Connected, isPlayer2Connected;

    // Start is called before the first frame update
    void Start()
    {
        PortScan();


        string comPort = usedPort; //"COM" + comNum.ToString();
        dataStream = new SerialPort(comPort, 9600);

        dataStream.Open();
        
    }

    void PortScan() {
        string[] ports = SerialPort.GetPortNames();
        Debug.Log("Got "+ports.Length+" ports");
        foreach( string portName in ports) {
            Debug.Log("Trying "+portName);

            try {
                using (SerialPort pt = new SerialPort(portName, 9600)) {
                    pt.Open();

                    string input = pt.ReadLine();

                    Debug.Log("Input: "+input);
                    pt.Close();

                    Debug.Log("success??");
                    usedPort = portName;
                }
            } catch (System.Exception error) {
                Debug.Log("Error on open: "+error.Message);
            }

        }
    }


    // Update is called once per frame
    void Update()
    {   
        dataStream.DiscardInBuffer();
        recievedData = dataStream.ReadLine();
        
        Debug.Log(recievedData);
        
        
        string[] pressurePlatePlayer = recievedData.Split(',');

        if(pressurePlatePlayer[0] == "1"){
            isPlayer1Connected = true;
        } else {
            isPlayer1Connected = false;
        }

        if(pressurePlatePlayer[1] == "1"){
            isPlayer2Connected = true;
        } else {
            isPlayer2Connected = false;
        }

        // if (!(isPlayer1Connected && isPlayer2Connected)){
        //     //timer
        // }else if (isPlayer1Connected || isPlayer2Connected){
        //     //timer
        // }else{
        //     //Both players are connected
        // }

    }
}
