using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMusicRandomPlaylist : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audiosource1;
    public AudioSource audiosource2;
    public AudioSource audiosource3;
    public AudioSource audiosource4;
    public AudioSource audiosource5;
    public AudioClip [] clips1;
    public AudioClip[] clips2;
    public AudioClip[] clips3;
    public AudioClip[] clips4;
    public AudioClip[] clips5;

    private AudioClip GetRandomClip1()
    {
        return clips1[Random.Range(0, clips1.Length)];
    }

    private AudioClip GetRandomClip2()
    {
        return clips2[Random.Range(0, clips2.Length)];
    }

    private AudioClip GetRandomClip3()
    {
        return clips3[Random.Range(0, clips3.Length)];
    }

      private AudioClip GetRandomClip4()
    {
        return clips4[Random.Range(0, clips4.Length)];
    }

    private AudioClip GetRandomClip5()
    {
        return clips5[Random.Range(0, clips4.Length)];
    }

    void Start()
    {
        audiosource1.loop = false;
        audiosource2.loop = false;
        audiosource3.loop = false;
        audiosource4.loop = false;
        audiosource5.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audiosource1.isPlaying)
        {
            audiosource1.clip = GetRandomClip1();
            audiosource1.Play();
        }

        if (!audiosource2.isPlaying)
        {
            audiosource2.clip = GetRandomClip2();
            audiosource2.Play();
        }

          if (!audiosource3.isPlaying)
        {
            audiosource3.clip = GetRandomClip3();
            audiosource3.Play();
        }

           if (!audiosource4.isPlaying)
        {
            audiosource4.clip = GetRandomClip4();
            audiosource4.Play();
        }
        if (!audiosource5.isPlaying)
        {
            audiosource5.clip = GetRandomClip5();
            audiosource5.Play();
        }
    }
}
