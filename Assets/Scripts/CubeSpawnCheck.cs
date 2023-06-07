using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnCheck : MonoBehaviour
{

    [SerializeField]GameObject[] cubes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isActiveAndEnabled) return;
        foreach(GameObject cube in cubes)
        {
            if (!cube.activeInHierarchy)
            {
                return;
            }
        }
        this.gameObject.SetActive(false);
    }
}
