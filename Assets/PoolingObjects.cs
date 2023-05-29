using System.Collections.Generic;
using UnityEngine;

public class PoolingObjects : MonoBehaviour
{
    public static PoolingObjects Instance { get; private set; }

    [SerializeField] private GameObject poolObject;
    [SerializeField] private int poolCapacity;

    private List<GameObject> poolingObjects = new List<GameObject>();
    private List<GameObject> active = new List<GameObject>();

    private void Awake()
    {
        //Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    void Start()
    {
        //Fill the list with objects
        GameObject temp;
        for (int i = 0; i < poolCapacity; i++)
        {
            temp = Instantiate(poolObject);
            temp.SetActive(false);
            poolingObjects.Add(temp);
        }
    }

    public void DeactivateOldest()
    {
        if (active.Count <= 1) return;  // if the list is empty, return

        // get the first object in the list and if is active, deactivate it and remove it from the list
        GameObject go = active[0];
        if (go.activeInHierarchy)
        {
            go.SetActive(false);
            active.Remove(go);
        }
    }


    public GameObject GetObjectFromPool()
    {
        //Loop through the list and check if the element is disabled,
        //add it to the list and return it
        for (int i = 0; i < poolCapacity; i++)
        {
            if (!poolingObjects[i].activeInHierarchy)
            {
                active.Add(poolingObjects[i]);
                return poolingObjects[i];
            }
        }
        return null;
    }
}
