using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume;
    private float defaultVolume = 0.5f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, defaultVolume);
        audioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        Debug.Log(volume);
        volume += 0.1f;
        if (volume > 1f)
            volume = 0f;

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
        Debug.Log(volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
