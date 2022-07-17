using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistAudio : MonoBehaviour
{
    void Awake()
    {
        // Prevents music player from being destroyed upon loading a new scene
        DontDestroyOnLoad(transform.gameObject);
    }
}
