using System.Collections.Generic;
using UnityEngine;

public class RotaryDiskHolder : MonoBehaviour
{
    public List<RotaryDisk> disks = new List<RotaryDisk>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            RotaryDisk disk = transform.GetChild(i).GetComponent<RotaryDisk>();
            if (disk != null && !disks.Contains(disk))
            {
                disks.Add(disk);
            }
        }
    }
    private void Update()
    {
        for (int i = 0; i < disks.Count; i++)
        {
            if (!disks[i].CorrectPuzzlePosition())
            {
                return;
            }
        }

        CorrectPuzzle();
        Debug.Log("Correct puzzle");
    }

    void CorrectPuzzle() { 
        //Implement an event
    }
}
