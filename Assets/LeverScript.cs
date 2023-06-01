using UnityEngine;

public class LeverScript : Interactable
{
    public new ILight light;

    public bool turnedOn;

    public bool matchCorrect;

    Animator anim;

    private void Start()
    {
        light = GetComponentInParent<ILight>();
        anim = GetComponent<Animator>();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        anim.SetTrigger("activated");
        turnedOn = !turnedOn;
        if (light.thisIsCorrect)
        {
            Debug.Log("azis " + (light as MonoBehaviour).name);
        }
    }

    public override void OnLoseFocus()
    {
    }

    private void Update()
    {
        if (turnedOn && light.thisIsCorrect)
        {
            matchCorrect = true;
        }
        else if (!turnedOn && !light.thisIsCorrect)
        {
            matchCorrect = true;
        }
        else
        {
            matchCorrect = false;
        }
    }
}
