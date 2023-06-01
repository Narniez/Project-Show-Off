using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScapeRandom : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audiosource1;
    public AudioClip[] clips1;

    private AudioClip GetRandomClip1()
    {
        return clips1[Random.Range(0, clips1.Length)];
    }


    void Start()
    {
        audiosource1.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audiosource1.isPlaying)
        {
            audiosource1.clip = GetRandomClip1();
            audiosource1.Play();
        }

    }
}