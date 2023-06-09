using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlatesHolder : MonoBehaviour
{

    [SerializeField] private List<CubePlate> plates = new List<CubePlate>();

    [SerializeField] GameObject[] doors;

    public bool levelSolved = false;
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

        levelSolved = true;
        foreach (GameObject door in doors)
        {
            door.TryGetComponent<Animator>(out Animator anim);
            if (anim != null)
            {
                anim.SetTrigger("Open");
            }

            if (anim == null)
            {

                door.SetActive(false);
            }
        }
    }

}
