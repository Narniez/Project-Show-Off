using UnityEngine;

public interface ILight {
    bool thisIsCorrect { get; set; }
}

public class LightPuzzlePiece : MonoBehaviour, ILight
{
    public bool isCorrect;
    public bool thisIsCorrect { get  => isCorrect; set => isCorrect = value; }
}
