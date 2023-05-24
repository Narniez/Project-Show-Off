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

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
