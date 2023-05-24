using System.Collections;
using UnityEngine;

public abstract class PuzzleAbstract : Interactable
{
    public virtual bool IsCorrect(Quaternion correctAngle, Quaternion targetAngle, Axis axisToCompare)
    {
        bool isCorrect = false;


        switch (axisToCompare)
        {
            case Axis.X:
                isCorrect = correctAngle.eulerAngles.x == targetAngle.eulerAngles.x;
                break;
            case Axis.Y:
                isCorrect = correctAngle.eulerAngles.y == targetAngle.eulerAngles.y;
                break;
            case Axis.Z:
                isCorrect = correctAngle.eulerAngles.z == targetAngle.eulerAngles.z;
                break;
            default:
                break;
        }

        return isCorrect;
    }

    public IEnumerator RotateTowardsTarget(bool isRotating, Quaternion targetAngle, float rotationDuration)
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
