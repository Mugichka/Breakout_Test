using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class VolumeController : MonoBehaviour
{
    public static VolumeController Instance;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private void Start()
    {
        if (Instance == null) Instance = this;

        sfxSlider.onValueChanged.AddListener(delegate { ApplyVolume(); });
        bgmSlider.onValueChanged.AddListener(delegate { ApplyVolume(); });

        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.4f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);

        //ApplyVolume();
    }

    public void ApplyVolume()
    {
        float sfxVol = sfxSlider.value;
        float bgmVol = bgmSlider.value;

        PlayerPrefs.SetFloat("SFXVolume", sfxVol);
        PlayerPrefs.SetFloat("BGMVolume", bgmVol);
        PlayerPrefs.Save();

        AudioManager.Instance.SetVolume(sfxVol, bgmVol);
    }
}