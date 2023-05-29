using UnityEngine;

public class PoolButton : Interactable, IButton
{
    public Transform spawnPosition;

    public bool IsPressed { get; set ; }

    public override void OnFocus()
    {
        return;
    }

    /// <summary>
    /// On interaction, it spawns game object at spawn position
    /// </summary>
    public override void OnInteract()
    {
        GameObject go = PoolingObjects.Instance.GetObjectFromPool(); // get an object from the pull
        if (go != null)
        {
            go.transform.position = spawnPosition.position;
            go.SetActive(true);
        }
        PoolingObjects.Instance.DeactivateOldest(); //deactivate the old object
    }

    public override void OnLoseFocus()
    {
        return;
    }
}
