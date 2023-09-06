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

    private void Start()
    {
        List<float> Volumes = new List<float>();
        Volumes = DataManager.instance.getVolume();
        setting(Volumes[0], Volumes[1], Volumes[2]);
        SetMixer();
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
        DataManager.instance.JsonSliderSave(master_slider.value, bgm_slider.value, sfx_slider.value);
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

    public void setting(float Master, float Bgm, float Sfx)
    {
        master_slider.value = Master;
        bgm_slider.value = Bgm;
        sfx_slider.value = Sfx;
    }

    public void SetMixer()
    {
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
    }
}