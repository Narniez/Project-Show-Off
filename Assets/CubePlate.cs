using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlate : MonoBehaviour
{
    bool canFreeze;
    bool hasCube;

    [SerializeField] float cubeHeightAbovePlatform;

    [SerializeField] GameObject correctCube;
    public bool HasCube()
    {
        return hasCube;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCube) return;
        if (correctCube!= null && other.gameObject == correctCube)
        {
            hasCube = true;
            other.tag = "CubePlaced";
            other.gameObject.transform.rotation = this.transform.rotation;
            other.gameObject.transform.position = this.transform.position + new Vector3(0, cubeHeightAbovePlatform, 0);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
