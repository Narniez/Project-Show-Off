using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolButton : Interactable, IButton
{
    public bool IsPressed { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void OnFocus()
    {
        return;
    }

    public override void OnInteract()
    {

        GameObject go = PoolingObjects.Instance.GetObjectFromPool();
        if (go != null)
        {
            go.SetActive(true);
        }
        PoolingObjects.Instance.DeactivateOldest();
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
