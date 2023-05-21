using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzlePiece : Interactable
{
    Vector3 orientation;
    float rotY = 90;
    Vector3 rotation = Vector3.zero;
    public Vector3 correctOrientation;
    public Vector3 currentOrientation;
    public Quaternion targetAngle;
    public bool isCorrect = false;

    public override void OnFocus()
    {
        //Debug.Log("Looking at puzzlePiece");
    }

    public override void OnInteract()
    {


        Debug.Log("Interacting");
        rotation.y += rotY;
        transform.Rotate(rotation);

    }

    public override void OnLoseFocus()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //currentOrientation = transform.rotation.eulerAngles;
    }
}
