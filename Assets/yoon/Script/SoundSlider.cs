using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public static SoundSlider instance;
    public AudioMixer mixer;
    public Slider bgm_slider;
    public Slider sfx_slider;
    public Slider master_slider;

    void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
        DataManager.instance.JsonSliderSave();
    }

    public void SetMasterVolume(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }

    public void SetBgmVolume(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSfxVolume(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void setting(float Master,float Bgm, float Sfx)
    {
        master_slider.value = Master;
        bgm_slider.value = Bgm;
        sfx_slider.value = Sfx;
        gameObject.SetActive(false);
    }

    public void SetMixer()
    {
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
    }
}

