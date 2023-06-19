using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerTrigger : MonoBehaviour
{
    public VideoClip[] clips;

    VideoPlayer player;
    MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.clip = clips[0]; //  Play(clips[0]);
            player.Play(); // fails first time?
            renderer.enabled = true;
        }
        if (!player.isPlaying)
        {
            renderer.enabled = false;   
        }
    }
}
