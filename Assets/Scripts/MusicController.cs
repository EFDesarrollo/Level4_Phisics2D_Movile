using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource music;
    private bool onMenu = true, onPlaying = false;
    private int musicIndex = 0;
    private int maxIndex = 1;

    public float volumeDownFactor = 0f;
    public float volumeDownLength = 10f;

    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.clip = clips[musicIndex];
    }
    // Update is called once per frame
    void Update()
    {
        music.volume += (1f / volumeDownLength) * Time.deltaTime;
        music.volume = Mathf.Clamp(music.volume, 0, 0.7f);

        if (onMenu)
        {
            music.volume = volumeDownFactor;
            musicIndex = 0;
            maxIndex = 1;
            music.clip = clips[musicIndex];
            music.Play();
            onMenu = false;
            onPlaying = false;
        }
        if (onPlaying)
        {
            if (musicIndex == 0)
            {
                music.volume = volumeDownFactor;
                musicIndex = 1;
                music.clip = clips[musicIndex];
                music.Play();
            }
            maxIndex = clips.Length-1;
            onPlaying = false;
            onMenu = false;
        }
        PlayList(maxIndex);
    }
    public void PlayList(int end)
    {
        if (!music.isPlaying)
        {
            musicIndex++;
            if (musicIndex >= end) musicIndex = end;
            music.clip = clips[musicIndex];
            music.Play();
        }
    }
    public void OnStartDoPerformance()
    {
        onMenu = true;
        onPlaying = false;
    }
    public void  OnPlayDoPerformance()
    {
        onMenu = false;
        onPlaying = true;
    }
}
