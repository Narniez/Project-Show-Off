using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{



    public int[] lightOrder;
    public float lightDuration = 1f;
    public float delayBetweenLights = 0.5f;
    public float delayBetweenSequences = 2f;

    private Light[] lights;
    private int currentIndex = 0;
    private bool isSequenceActive = false;

    public bool startSequence = false;

    private void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
    public void StartLightSequence()
    {
        if (!isSequenceActive)
        {
            StartCoroutine(ActivateLightsSequence());
        }
    }

    private IEnumerator ActivateLightsSequence()
    {
        isSequenceActive = true;

        for (int i = 0; i < lightOrder.Length; i++)
        {
            int index = lightOrder[i];
            if (index >= 0 && index < lights.Length)
            {
                Light light = lights[index];
                TurnOnLight(light);
                yield return new WaitForSeconds(lightDuration);
                TurnOffLight(light);
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }

        yield return new WaitForSeconds(delayBetweenSequences);
        isSequenceActive = false;
        ResetSequence();
    }

    private void TurnOnLight(Light light)
    {
        light.enabled = true;
    }

    private void TurnOffLight(Light light)
    {
        light.enabled = false;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public int GetLightSequenceLength()
    {
        return lightOrder.Length;
    }

    public void IncrementIndex()
    {
        currentIndex++;
    }

    public void ResetSequence()
    {
        currentIndex = 0;
        // Reset any necessary puzzle-related state here
    }
}
