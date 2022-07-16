using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceAssistant : MonoBehaviour
{
    public enum AudioType
    {
        SoundFX,
        BackgroundMusic
    };
    public AudioType audioType;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioType == AudioType.SoundFX)
        {
            AudioManager.Instance.OnSoundFXVolumeChanged.AddListener(OnVolumeChangedCallback);
            audioSource.volume = AudioManager.Instance.soundFXVolume;
        }
        else
        {
            AudioManager.Instance.OnBackgroundVolumeChanged.AddListener(OnVolumeChangedCallback);
            audioSource.volume = AudioManager.Instance.backgroundVolume;
        }
    }

    private void OnDestroy()
    {
        if (audioType == AudioType.SoundFX)
        {
            AudioManager.Instance.OnSoundFXVolumeChanged.RemoveListener(OnVolumeChangedCallback);
        }
        else
        {
            AudioManager.Instance.OnBackgroundVolumeChanged.RemoveListener(OnVolumeChangedCallback);
        }
    }

    void OnVolumeChangedCallback(float volume)
    {
        audioSource.volume = volume;
    }
}
