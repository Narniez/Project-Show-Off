using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    [SerializeField] GameObject door;

    Animator doorAnim;

    bool isOpen = false;
    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        Debug.Log("Interaction");
        if (!isOpen)
        {
            door.GetComponent<Animator>().SetTrigger("Open");
            isOpen = true;
        }
        else
        {
            door.GetComponent<Animator>().SetTrigger("Close");
            isOpen = false;
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    void Start()
    {
        door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
