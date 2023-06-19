using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] GameObject door;
    [SerializeField] GameObject[] cubes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        foreach(GameObject cube in cubes)
        {
            if (!cube.activeInHierarchy) return;
        }

        door.SetActive(false);
    }
}
