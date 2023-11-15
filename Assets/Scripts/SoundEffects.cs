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
           //DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.volume = 1;
        if (clip != null && !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void StopSoundEffect() { 
        audioSource.clip = null;
        //audioSource.Stop();
    }
}
