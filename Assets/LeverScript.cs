using UnityEngine;

public class LeverScript : Interactable
{
    new ILight light;

    public bool turnedOn;

    Animator anim;

    private void Start()
    {
        light = GetComponentInParent<ILight>();
        anim = GetComponent<Animator>();
    }

    public override void OnFocus()
    { }

    public override void OnInteract()
    {
        anim.SetTrigger("activated");
        if (light.thisIsCorrect)
        {
            Debug.Log("azis " + (light as MonoBehaviour).name);
            turnedOn = !turnedOn;
        }
    }

    public override void OnLoseFocus()
    { }


    private void Update()
    {
    }
}
