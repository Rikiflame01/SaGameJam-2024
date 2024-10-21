using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;
    public AudioSource loopingSfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float backgroundMusicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Background Music Tracks")]
    public List<AudioClip> backgroundMusicTracks;

    [Header("Sound Effects")]
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDictionary;
    private int currentMusicIndex = 0;

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

        sfxSource = gameObject.AddComponent<AudioSource>();
        loopingSfxSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        sfxDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in sfxClips)
        {
            if (!sfxDictionary.ContainsKey(clip.name))
            {
                sfxDictionary.Add(clip.name, clip);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        backgroundMusicSource.volume = backgroundMusicVolume;
        sfxSource.volume = sfxVolume;
        loopingSfxSource.volume = sfxVolume;

        UpdateBackgroundMusic(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateBackgroundMusic(scene.name);
    }

    private void UpdateBackgroundMusic(string sceneName)
    {
        if (sceneName == "Title Screen" || sceneName == "Level 1" || sceneName == "Level 3" || sceneName == "Level 5"
            || sceneName == "Level 7" || sceneName == "Level 9")
        {
            currentMusicIndex = 0;
            PlayBackgroundMusic(backgroundMusicTracks[currentMusicIndex]);
        }
        else
        {
            currentMusicIndex = 1;
            PlayBackgroundMusic(backgroundMusicTracks[currentMusicIndex]);
        }
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (musicClip == null)
        {
            Debug.LogWarning("No music clip provided.");
            return;
        }

        if (backgroundMusicSource.clip == musicClip) return;

        backgroundMusicSource.clip = musicClip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            AudioClip clip = sfxDictionary[sfxName];
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound effect '{sfxName}' not found!");
        }
    }

    public void PlayLoopingSFX(string sfxName)
    {
        if (sfxDictionary.ContainsKey(sfxName))
        {
            AudioClip clip = sfxDictionary[sfxName];
            loopingSfxSource.clip = clip;
            loopingSfxSource.loop = true;
            loopingSfxSource.Play();
        }
        else
        {
            Debug.LogWarning($"Looping sound effect '{sfxName}' not found!");
        }
    }

    public void StopAllLoopingEffects()
    {
        if (loopingSfxSource.isPlaying)
        {
            loopingSfxSource.Stop();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
