using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzlePiece : PuzzleAbstract
{
    public UnityAction PieceRotated;
    public Quaternion correctAngle;
    public bool isCorrect;
    public string uiText;
    public TextMeshProUGUI textUIelement;
    [HideInInspector] public Quaternion targetAngle;

    public AudioClip soundEffect;

    [SerializeField] private int rotAmount = 90;
    [SerializeField] private float rotationDuration = 1;
    private IPlayer playerControls;

    public void AssignControls(IPlayer controls)
    {
        playerControls = controls;
    }

    public override void OnFocus() {


        //UIManager.Instance.worldUItext.text = "lelq pena blyskala";
    }

    //Override the OnInteract method to call RotatePiece
    public override void OnInteract()
    {
        SoundEffects.instance.PlaySoundEffect(soundEffect);
        if(playerControls.IsLockedOnTower)
        RotatePiece();
    }

    public override void OnLoseFocus()
    {
        //UIManager.Instance.worldUItext.text = " ";
    }

    private void FixedUpdate()
    {
        isCorrect = IsCorrect(transform.localRotation, correctAngle, Axis.Y);
        if (IsCorrect(transform.localRotation, correctAngle, Axis.Y))
        {
            Debug.Log(this.name + " Puzzle piece is in the correct orientation.");
        }
    }

    //If we are not rotating, set the target angle and rotation duration, start rotation and Invoke PieceRotated event 
    public void RotatePiece()
    {
        if (!IsRotating())
        {
            targetAngle = transform.rotation * Quaternion.Euler(0, rotAmount, 0);
            StartCoroutine(RotateTowardsTarget(targetAngle, rotationDuration));
        }
        PieceRotated?.Invoke();
    }
}
