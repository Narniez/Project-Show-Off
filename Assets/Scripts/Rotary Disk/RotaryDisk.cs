using System;
using System.Collections.Generic;
using UnityEngine;

public class RotaryDisk : MonoBehaviour
{
    [HideInInspector] public RotaryDisk corespondingDisk;
    [SerializeField] private int[] correctDegreesObject;

    private List<Transform> disks = new List<Transform>();

    /// <summary>
    /// finds the coresponding disk (it needs 2 disks) and add the children to a list
    /// </summary>
    private void Start()
    {
        // Get the corresponding disk
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject childTransform = transform.parent.GetChild(i).gameObject;

            if (childTransform == this.gameObject)
                continue;

            corespondingDisk = childTransform.GetComponent<RotaryDisk>();

            if (corespondingDisk != null)
                break;
        }

        // Get the disk pieces
        for (int i = 0; i < transform.childCount; i++)
        {
            disks.Add(transform.GetChild(i));
        }
    }

    /// <summary>
    /// Checks if the disks are in the correct rotation
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public bool CorrectPuzzlePosition()
    {
        if (corespondingDisk == null)
        {
            throw new NullReferenceException("Coresponding Disk is null! Add the other disk.");
        }

        for (int i = 0; i < disks.Count; i++)
        {
            if ((int)disks[i].rotation.eulerAngles.z != correctDegreesObject[i])
            {
                return false;
            }

            if ((int)corespondingDisk.disks[i].rotation.eulerAngles.z != corespondingDisk.correctDegreesObject[i])
            {
                return false;
            }
        }

        return true;
    }
}
