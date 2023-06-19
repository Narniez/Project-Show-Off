using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlatesHolder : MonoBehaviour
{

    [SerializeField] private List<CubePlate> plates = new List<CubePlate>();

    [SerializeField] GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        CubePlate[] cubeObjects = GetComponentsInChildren<CubePlate>(true);
        plates.AddRange(cubeObjects);
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < plates.Count; i++)
        {
            if (!plates[i].HasCube())  // if one of the puzzles is wrong, return
            {
                return;
            }
        }

        foreach(GameObject door in doors)
        {
            door.SetActive(false);
        }
    }

}
