using System.Collections;
using UnityEngine;

public abstract class PuzzleAbstract : Interactable
{

    //Bool to store if the current interactable isRotating 
    protected bool isRotating;

    //Check if the current angle on the chosen axis is close to the target angle we set
    public virtual bool IsCorrect(Quaternion currentAngle, Quaternion targetAngle, Axis axisToCompare)
    {
        bool isCorrect = false;
        switch (axisToCompare)
        {
            case Axis.X:
                isCorrect = currentAngle.eulerAngles.x == targetAngle.eulerAngles.x;
                break;
            case Axis.Y:
                isCorrect = Mathf.Abs(Mathf.Abs(currentAngle.eulerAngles.y) - Mathf.Abs(targetAngle.eulerAngles.y)) <= 5f;
                break;
            case Axis.Z:
                isCorrect = Mathf.Abs(Mathf.Abs(currentAngle.eulerAngles.z) - Mathf.Abs(targetAngle.eulerAngles.z)) <= 5f;
                break;
            default:
                break;
        }

        return isCorrect;
    }

    //Return the value of isRotating for the current interactbale 
    public bool IsRotating()
    {
        return isRotating;
    }

    //Rotate towards the target angle for a specified time
    public IEnumerator RotateTowardsTarget(Quaternion targetAngle, float rotationDuration)
    {
        //Save the starting position and reset the elapsed time 
        isRotating = true;
        Quaternion startingRotation = transform.rotation;
        float elapsedTime = 0f;


        //If the time passed is less than the time we set for the rotation keep rotatng to the target angle
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startingRotation, targetAngle, elapsedTime / rotationDuration);
            yield return null;
        }

        //Round to the nearest integer because of unity's weird angles 
        Vector3 e = transform.rotation.eulerAngles;
        e.x = Mathf.Round(e.x);
        e.y = Mathf.Round(e.y);
        e.z = Mathf.Round(e.z);
        transform.rotation = Quaternion.Euler(e);
        isRotating = false;
    }

    public void InstantiateReward(GameObject go, Transform pos)
    {
        // Instantiate the object at the specified position
        if (go != null && pos != null)
        {
            Instantiate(go, pos.position, Quaternion.identity);
        }
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
