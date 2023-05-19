using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform target; // Target for camera rotation
    public float rotationSpeed;
    private Quaternion initialRotation; // Initial rotation of the camera
    private Vector3 initialPosition;
    bool isLocked = false;

    private Quaternion targetRotation;
    private Vector3 targetPosition;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isLocked)
        {
            initialRotation = transform.rotation;
            initialPosition = transform.position;
        }
        if (target != null)
        {
            // Rotate the camera towards the target
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void LockCamera(Transform newTarget)
    {
        isLocked = true;
        target = newTarget;
        PlayerControls.canLook = false;
        PlayerControls.canMove = false;
    }

    public void AssignTarget(Transform _target)
    {
        target = _target;
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }    
    public void UnlockCamera()
    {
        isLocked = false;
        PlayerControls.canLook = true;
        PlayerControls.canMove = true;
        target = null;
        transform.rotation = initialRotation; // Reset the camera rotation
    }
}
