using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        PlayBGM("BGM");
    }

    public void PlaySFX(string clipName)
    {
        AudioClip clip = audioLibrary.GetClipByName(clipName);
        if (clip) sfxSource.PlayOneShot(clip);
    }

    public void PlayBGM(string clipName)
    {
        AudioClip clip = audioLibrary.GetClipByName(clipName);
        if (clip && bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void SetVolume(float sfxVolume, float bgmVolume)
    {
        sfxSource.volume = sfxVolume;
        bgmSource.volume = bgmVolume;
    }
}