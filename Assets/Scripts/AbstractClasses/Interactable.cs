using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isHeld;

    public virtual void Awake()
    {
        gameObject.layer = 9;
    }
    public abstract void OnInteract();

    public abstract void OnFocus();

    public abstract void OnLoseFocus();
}
