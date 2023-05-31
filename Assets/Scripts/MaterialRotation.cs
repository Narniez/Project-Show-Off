using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRotation : MonoBehaviour
{
    [SerializeField]
    Material material;

   public float rotationSpeed = 1.0f;

    private float rotation;
    void Start()
    {
        if (material == null) return;
        // Get the material attached to the renderer
        //Renderer renderer = GetComponent<Renderer>();
       // material = renderer.sharedMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        if (material == null) return;
        // Update the rotation based on the time and speed
        rotation += Time.deltaTime * rotationSpeed;

        // Set the rotation value in the material
        material.SetFloat("_Rotation", rotation);
    }
}
