using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public List<ILight> lights = new List<ILight>();
    public List<LeverScript> levers = new List<LeverScript>();
    public LeverScript lever;

    private bool isCompleted;

    public bool isSolved { get => isCompleted; private set => isCompleted = value; }

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
                item.TryGetComponent<LeverScript>(out lever);
                levers.Add(lever);
            }
        }
    }

    private void Update()
    {
        foreach (ILight light in lights)
        {
            foreach (LeverScript lever in levers)
            {
                if (light.thisIsCorrect && lever.turnedOn)
                {
                    isCompleted = true;
                    Debug.Log("puzzle correct lights");
                }
                else isCompleted = false;
            }
        }
    }
}
