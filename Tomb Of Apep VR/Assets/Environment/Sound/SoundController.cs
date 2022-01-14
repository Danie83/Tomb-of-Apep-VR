using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundController : MonoBehaviour
{
    public float volume = 1f;
    private float baseVolume = 1f;
    private float musicVolume = 1f;
    private float previousVolume = 1f;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (volume != previousVolume)
        {
            previousVolume = volume;
            audioSource.volume = volume;
        }
    }

    public void UpdateBaseVolume(float volume)
    {
        this.baseVolume = volume;
        this.volume = this.baseVolume * this.musicVolume;
    }

    public void UpdateMusicVolume(float volume)
    {
        this.musicVolume = volume;
        this.volume = this.baseVolume * this.musicVolume;
    }
}
