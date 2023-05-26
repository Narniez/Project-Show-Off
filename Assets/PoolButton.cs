using UnityEngine;

public class PoolButton : Interactable, IButton
{
    public Transform spawnPosition;

    public bool IsPressed { get; set ; }

    public override void OnFocus()
    {
        return;
    }

    public override void OnInteract()
    {
        GameObject go = PoolingObjects.Instance.GetObjectFromPool();
        if (go != null)
        {
            go.transform.position = spawnPosition.position;
            go.SetActive(true);
        }
        PoolingObjects.Instance.DeactivateOldest();
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
