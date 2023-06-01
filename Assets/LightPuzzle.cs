using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public List<ILight> lights = new List<ILight>();

    private List<LeverScript> levers = new List<LeverScript>();
    private HashSet<LeverScript> leversDone = new HashSet<LeverScript>();

    public bool isSolved => leversDone.Count >= 4;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            ILight light = child.GetComponent<ILight>();
            if (light != null)
            {
                lights.Add(light);
            }
            foreach (Transform item in child.transform)
            {
                if (item.TryGetComponent(out LeverScript lever))
                {
                    levers.Add(lever);
                }
            }
        }
    }

    private void Update()
    {
        leversDone.Clear();

        foreach (LeverScript lever in levers)
        {
            if (lever.matchCorrect)
            {
                leversDone.Add(lever);
            }
        }

        if (isSolved)
        {
            Debug.Log("Puzzle completed");
        }
    }

}
