using System.Collections.Generic;
using UnityEngine;

public class RotaryDisk : MonoBehaviour
{
    [HideInInspector] public RotaryDisk corespondingDisk;
    [HideInInspector] public List<GameObject> disks = new List<GameObject>();

    [SerializeField] private int[] correctDegreesObject;

    private void Start()
    {
        //Get the coresponding disk
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            GameObject childTransform = transform.parent.GetChild(i).gameObject;

            if (childTransform == this.gameObject)
                continue;

            corespondingDisk = childTransform.GetComponent<RotaryDisk>();

            if (corespondingDisk != null)
                break;
        }

        //Get the disk pieces
        for (int i = 0; i < transform.childCount; i++)
        {
            disks.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CorrectPuzzlePosition();
    }

    private bool CorrectPuzzlePosition()
    {
        if (corespondingDisk == null)
            throw new System.NullReferenceException("Coresponding Disk is null!!!!! ADD THE OTHER DISK MADARFAKAR");

        if ((int)disks[0].transform.rotation.eulerAngles.z == (int)correctDegreesObject[0] &&
            (int)disks[1].transform.rotation.eulerAngles.z == (int)correctDegreesObject[1] &&
            (int)disks[2].transform.rotation.eulerAngles.z == (int)correctDegreesObject[2] &&

            (int)corespondingDisk.disks[0].transform.rotation.eulerAngles.z == (int)corespondingDisk.correctDegreesObject[0] &&
            (int)corespondingDisk.disks[1].transform.rotation.eulerAngles.z == (int)corespondingDisk.correctDegreesObject[1] &&
            (int)corespondingDisk.disks[2].transform.rotation.eulerAngles.z == (int)corespondingDisk.correctDegreesObject[2])
        {
            Debug.Log("Correct position");
            return true;
        }
        return false;
    }
}
