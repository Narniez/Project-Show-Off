using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;

    public AudioClip buttonClip;
    public AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
