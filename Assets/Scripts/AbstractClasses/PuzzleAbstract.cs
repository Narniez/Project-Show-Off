using System.Collections;
using UnityEngine;

public abstract class PuzzleAbstract : Interactable
{
    protected bool isRotating;
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

    public bool IsRotating()
    {
        return isRotating;
    }

    public IEnumerator RotateTowardsTarget(Quaternion targetAngle, float rotationDuration)
    {
        isRotating = true;
        Quaternion startingRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startingRotation, targetAngle, elapsedTime / rotationDuration);
            yield return null;
        }
        Vector3 e = transform.rotation.eulerAngles;
        e.y = Mathf.Round(e.y);
        transform.rotation = Quaternion.Euler(e);
        isRotating = false;
    }



    public enum Axis
    {
        X,
        Y,
        Z
    }
}
