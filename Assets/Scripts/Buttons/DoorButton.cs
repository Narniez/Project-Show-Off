using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : Interactable
{
    [SerializeField] GameObject door;

    Animator doorAnim;

    Animator leverAnim;

    bool isOpen = false;
    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        Debug.Log("Interaction");
        if (!isOpen)
        {
            leverAnim.SetTrigger("activated");
            door.GetComponent<Animator>().SetTrigger("Open");
            isOpen = true;
        }
        else
        {
            leverAnim.SetTrigger("activated");
            door.GetComponent<Animator>().SetTrigger("Close");
            isOpen = false;
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    void Start()
    {
        leverAnim = this.GetComponent<Animator>();
        door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
