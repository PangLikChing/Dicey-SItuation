using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0f, 1f)] public float backgroundVolume = 1.0f;
    [Range(0f, 1f)] public float soundFXVolume = 1.0f;

    [System.Serializable]
    public class VolumeChangedEvent : UnityEvent<float> { }

    [HideInInspector] public VolumeChangedEvent OnSoundFXVolumeChanged;
    [HideInInspector] public VolumeChangedEvent OnBackgroundVolumeChanged;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        OnSoundFXVolumeChanged = new VolumeChangedEvent();
        OnBackgroundVolumeChanged = new VolumeChangedEvent();
    }

    public void SetBackgroundVolume(float volume)
    {
        if (volume > 1.0f)
        {
            volume = 1.0f;
        }
        else if (volume < 0.0f)
        {
            volume = 0.0f;
        }
        backgroundVolume = volume;
        OnBackgroundVolumeChanged.Invoke(backgroundVolume);
    }

    public void SetSoundFXVolume(float volume)
    {
        if (volume > 1.0f)
        {
            volume = 1.0f;
        }
        else if (volume < 0.0f)
        {
            volume = 0.0f;
        }
        soundFXVolume = volume;
        OnSoundFXVolumeChanged.Invoke(soundFXVolume);
    }
}
