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
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    void Start()
    {
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
        if (active.Count <= 1) return;

        GameObject go = active[0];
        if (go.activeInHierarchy)
        {
            go.SetActive(false);
            active.Remove(go);
        }
    }


    public GameObject GetObjectFromPool()
    {
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
