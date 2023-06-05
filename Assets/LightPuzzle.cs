using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public List<ILight> lights = new List<ILight>();

    private List<LeverScript> levers = new List<LeverScript>();
    private HashSet<LeverScript> leversDone = new HashSet<LeverScript>();

    public bool isCompleted => leversDone.Count >= 10;
   //public bool isSolved { get => isCompleted; set => isCompleted = value; }


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
        Debug.Log(leversDone.Count);
        leversDone.Clear();

        foreach (LeverScript lever in levers)
        {
            if (lever.matchCorrect)
            {
                leversDone.Add(lever);
            }
        }

        if (isCompleted)
        {
            Debug.Log("Puzzle completed");
        }
    }

}
