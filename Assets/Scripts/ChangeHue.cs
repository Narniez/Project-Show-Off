using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeHue : MonoBehaviour
{
    public Volume volume;
    public float timeScale = 1f;

    public ColorAdjustments colorAdjustments;
    private VolumeParameter<float> hueShift = new VolumeParameter<float>();

    void Start()
    {
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);

        if (colorAdjustments == null)
            Debug.LogError("No ColorAdjustments found on profile");

    }

    void Update()
    {


        if (colorAdjustments != null)
        {

            hueShift.value += timeScale * Time.deltaTime;

            colorAdjustments.hueShift.SetValue(hueShift);
        }
    }

}
