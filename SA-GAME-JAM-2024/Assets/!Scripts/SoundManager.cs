using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float backgroundMusicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Sound Effects")]
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in sfxClips)
        {
            if (!sfxDictionary.ContainsKey(clip.name))
            {
                sfxDictionary.Add(clip.name, clip);
            }
        }
    }

    private void Start()
    {
        backgroundMusicSource.volume = backgroundMusicVolume;
        sfxSource.volume = sfxVolume;
    }


    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (backgroundMusicSource.clip == musicClip) return;
        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void PlaySFX(string sfxName, float volume = -1f)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            AudioClip clip = sfxDictionary[sfxName];

            float sfxPlayVolume = volume >= 0f ? Mathf.Clamp(volume, 0f, 1f) : sfxVolume;

            sfxSource.PlayOneShot(clip, sfxPlayVolume);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{sfxName}' not found!");
        }
    }


    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicVolume = Mathf.Clamp(volume, 0f, 1f);
        backgroundMusicSource.volume = backgroundMusicVolume;
    }


    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp(volume, 0f, 1f);
        sfxSource.volume = sfxVolume;
    }
}
