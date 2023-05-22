using System.Collections;
using UnityEngine;

public class DelayButton : Interactable, IButton
{
    public bool IsPressed { get; set; }

    public override void OnFocus()
    {
        return;
    }

    public override void OnInteract()
    {
        IsPressed = true;
        StartCoroutine(TimeButton());
    }

    public override void OnLoseFocus()
    {
        return;
    }

    IEnumerator TimeButton()
    {
        yield return new WaitForSeconds(2);
        IsPressed = false;
    }

    void Update() {
        Debug.Log(IsPressed);
    }
}
