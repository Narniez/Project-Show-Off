using UnityEngine;

public class LeverScript : Interactable
{
    new ILight light;

    public bool turnedOn;

    private void Start()
    {
        light = GetComponentInParent<ILight>();
    }

    public override void OnFocus()
    { }

    public override void OnInteract()
    {
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
