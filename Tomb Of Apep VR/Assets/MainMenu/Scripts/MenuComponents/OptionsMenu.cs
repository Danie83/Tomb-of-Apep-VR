using UnityEngine;

public class OptionsMenu : MenuBaseComponent
{
    [Header("Audio Settings")]
    [SerializeField]
    private AudioController masterAudioController;
    [SerializeField]
    private AudioController effectsAudioController;
    [SerializeField]
    private AudioController musicAudioController;

    public float GetAudioVolume()
    {
        return masterAudioController.Volume;
    }

    public float GetEffectsVolume()
    {
        return effectsAudioController.Volume;
    }

    public float GetMusicVolume()
    {
        return musicAudioController.Volume;
    }


    
}
