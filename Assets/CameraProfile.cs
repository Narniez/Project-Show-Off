using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraProfile : MonoBehaviour
{

    [SerializeField]bool useSecondProfile;



    [SerializeField] Volume[] volumes;

    private void Start()
    {
       volumes = GetComponentsInChildren<Volume>(true);
        
    }

    private void Update()
    {
        if (useSecondProfile)
        {
            volumes[0].enabled = false;
            volumes[1].enabled = true;
        }
        else
        {
            volumes[0].enabled = true;
            volumes[1].enabled = false;
        }
    }
}
