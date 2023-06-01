using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkable;
    public GameObject a;
    public int width;
    public int height;
    public Node[,] nodes;
    Transform floor;

    AStar aStar;
    private void Awake()
    {
        aStar = GetComponent<AStar>();
        Gridd();
    }

    /// <summary>
    /// Creates a grid of nodes
    /// It checks if is walkable. It detects it with Physics.CheckBox if collides with unwalkable layer
    /// </summary>
    void Gridd()
    {
        floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Transform>();
        nodes = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int nodePos = new Vector3Int(x - Mathf.FloorToInt(floor.localScale.x), 1,
                    y - Mathf.FloorToInt(floor.localScale.z));

                bool walkable = !Physics.CheckBox(nodePos, new Vector3(.49f, .49f, .49f), Quaternion.identity, unwalkable);

                nodes[x, y] = new Node(nodePos, walkable, 1.0f);

                if (walkable)
                {
                    aStar.openDictionary.Add(nodePos, nodes[x, y]);
                    
                    GameObject cube = Instantiate(a, nodes[x, y].worldPos, Quaternion.identity);
                    cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                }
            }
        }

        // Find the start and end positions
        GameObject startObj = GameObject.FindGameObjectWithTag("Start");
        GameObject endObj = GameObject.FindGameObjectWithTag("End");

        startObj.transform.position = FindClosestNodePosition(startObj.transform.position);
        endObj.transform.position = FindClosestNodePosition(endObj.transform.position);

        if (startObj != null)
        {
            aStar.startPos = startObj.transform.position;
        }
        else
        {
            Debug.LogError("Start object not found in the scene!");
        }

        if (endObj != null)
        {
            aStar.endPos = endObj.transform.position;
        }
        else
        {
            Debug.LogError("End object not found in the scene!");
        }
    }
    private Vector3 FindClosestNodePosition(Vector3 position)
    {
        float closestDistance = float.MaxValue;
        Vector3 closestPosition = Vector3Int.zero;

        foreach (var node in aStar.openDictionary.Values)
        {
            float distance = Vector3.Distance(position, node.worldPos);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = node.worldPos;
            }
        }

        return closestPosition;
    }

}