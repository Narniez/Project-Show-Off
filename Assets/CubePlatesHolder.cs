using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlatesHolder : MonoBehaviour
{
   [SerializeField] private List<CubePlate> cubePlates = new List<CubePlate>();

    [SerializeField] GameObject[] doors;

    bool openDoors;

    // Start is called before the first frame update
    void Start()
    {
        CubePlate[] plates = GetComponentsInChildren<CubePlate>(true);
        cubePlates.AddRange(plates);

    }
    // Update is called once per frame
    void Update()
    {

        openDoors = false;

        for(int i = 0; i < cubePlates.Count; i++)
        {
            if (!cubePlates[i].HasCube())
            {
                return;
            }
        }
        openDoors = true;
        if (openDoors)
        {
            foreach(GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }
}
