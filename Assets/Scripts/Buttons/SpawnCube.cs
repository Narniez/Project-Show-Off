using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : PuzzleAbstract
{
    [SerializeField] private Transform instantiationPosition;

    public GameObject puzzle;

    public GameObject companionCube;

    bool canSpawn;
    bool hasSpawned;

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

    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (canSpawn && !hasSpawned)
        {
            companionCube.SetActive(true);
            //InstantiateReward(objectToInstantiate, instantiationPosition);
            hasSpawned = true;
        }
        if (hasSpawned)
        {
            ResetPosition();
        }
    }

    public override void OnLoseFocus()
    {
       
    }

    void ResetPosition()
    {
        companionCube.transform.position = instantiationPosition.position;
    }
}
