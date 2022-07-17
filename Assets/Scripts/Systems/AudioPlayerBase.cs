using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that to store and play audio clips
/// </summary>
[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayerBase : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(-3f, 3f)]
        public float pitch = 1f;
    }

    protected AudioSource audioSource;
    protected Dictionary<List<Sound>, int> currentSoundIndex = new Dictionary<List<Sound>, int>();

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a sound, applying parameters of that sound to AudioSource
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(Sound sound)
    {
        if (sound == null) return;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.PlayOneShot(sound.audioClip);
    }

    /// <summary>
    /// Plays a random sound from the list
    /// </summary>
    /// <param name="sounds">A list of sounds to play from</param>
    public void PlayRandomFromList(List<Sound> sounds)
    {
        if (sounds == null || sounds.Count == 0) return;

        int index = Random.Range(0, sounds.Count - 1);
        PlaySound(sounds[index]);
    }

    /// <summary>
    /// Plays a sound from the list in order of that list
    /// This method knows index of audio clip played
    /// last time it was called
    /// </summary>
    /// <param name="sounds">A list of sounds to play from</param>
    public void PlayFromList(List<Sound> sounds)
    {
        if (sounds == null || sounds.Count == 0) return;

        if (!currentSoundIndex.TryGetValue(sounds, out int index))
        {
            index = 0;
            currentSoundIndex.Add(sounds, index);
        }
        else
        {
            index = index >= sounds.Count - 1 ? 0 : ++index;
            currentSoundIndex[sounds] = index;
        }

        PlaySound(sounds[index]);
    }
}