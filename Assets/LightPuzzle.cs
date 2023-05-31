using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public List<ILight> lights = new List<ILight>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            ILight light = child.GetComponent<ILight>();
            if (light != null)
            {
                lights.Add(light);
            }
        }
    }
}
