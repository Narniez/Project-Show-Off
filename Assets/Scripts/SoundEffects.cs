using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopSoundEffect() { 
        audioSource.Stop();
    }
}
