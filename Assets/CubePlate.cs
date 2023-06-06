using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlate : MonoBehaviour
{
    bool canFreeze;
    bool hasCube;
    public bool HasCube()
    {
        return hasCube;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCube) return;
        if (other.CompareTag("CompanionCube"))
        {
            hasCube = true;
            other.tag = "CubePlaced";
            other.gameObject.transform.rotation = this.transform.rotation;
            other.gameObject.transform.position = this.transform.position + new Vector3(0, 1, 0);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
