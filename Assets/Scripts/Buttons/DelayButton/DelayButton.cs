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

    /// <summary>
    /// After 2 seconds, the button is no longer pressed
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeButton()
    {
        yield return new WaitForSeconds(2);
        IsPressed = false;
    }
}
