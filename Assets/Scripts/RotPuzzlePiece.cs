using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotPuzzlePiece : MonoBehaviour
{
    public float targetCorrectRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsCorrect()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        //Mathf.Approximately it returns bool if the 2 floats are approximately equal 
        //(Research it after I saw it from AI. I used a few if checks with casting to (int))
        bool isCorrectPosition = Mathf.Approximately(currentRotation, targetCorrectRotation);
        return isCorrectPosition;
    }
}
