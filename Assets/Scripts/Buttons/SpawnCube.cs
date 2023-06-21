using UnityEngine;

public class SpawnCube : PuzzleAbstract, IButton
{
    [SerializeField] private Transform instantiationPosition;

    public GameObject puzzle;

    public GameObject companionCube;

    public AudioClip audioClip;
    public AudioClip audioClipSpawnCube;

    bool canSpawn;
    bool hasSpawned;

    public AudioClip soundEffect { get => audioClip; set => audioClip = value; }
    
    public bool IsPressed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        companionCube.gameObject.transform.position = instantiationPosition.position;
        companionCube.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (puzzle.TryGetComponent<ColorPuzzle>(out ColorPuzzle colorP))
        {
            canSpawn = colorP.IsSolved();
        }
        else if(puzzle.TryGetComponent<RotationPuzzleHolder>(out RotationPuzzleHolder rotP))
        {
            canSpawn = rotP.isSolved;
        }
        else if(puzzle.TryGetComponent<LightPuzzle>(out LightPuzzle lightP))
        {
            canSpawn = lightP.isCompleted;
        }
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        IsPressed = true;
        SoundEffects.instance.PlaySoundEffect(soundEffect);
        if (canSpawn && !hasSpawned)
        {
            companionCube.SetActive(true);
            SoundEffects.instance.PlaySoundEffect(audioClipSpawnCube);

            //InstantiateReward(objectToInstantiate, instantiationPosition);
            hasSpawned = true;
        }
        if (hasSpawned && !companionCube.GetComponent<CompanionCube>().isPlaced)
        {
            ResetPosition();
        }
    }

    public override void OnLoseFocus()
    {
        IsPressed = false;
    }

    void ResetPosition()
    {
        companionCube.transform.position = instantiationPosition.position;
    }
}
