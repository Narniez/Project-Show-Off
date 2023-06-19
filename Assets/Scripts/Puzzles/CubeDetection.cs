using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDetection : MonoBehaviour
{
    int cubeCount;
    [SerializeField] GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cubeCount == 3)
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CompanionCube"))
        {
            cubeCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CompanionCube"))
        {
            cubeCount--;
        }
    }
}
