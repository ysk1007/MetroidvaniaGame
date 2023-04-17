using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //����� ��� ���� ����
using UnityEngine.UI; //UI ��� ���� ����

public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer; //�ͼ��� ���� ����

    //�� ���� �����̴��� ���� ����
    public Slider bgm_slider;
    public Slider sfx_slider;
    public Slider master_slider;
    void Start()
    {
        //�����Ҷ� �����ߴ� ���� ���� ����
        SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        SetBgmVolume(PlayerPrefs.GetFloat("BGM"));
        SetSfxVolume(PlayerPrefs.GetFloat("SFX"));
        master_slider.value = PlayerPrefs.GetFloat("Master");
        bgm_slider.value = PlayerPrefs.GetFloat("BGM");
        sfx_slider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void FixedUpdate()
    {
        //�����̴��� ������� ���Ҷ� �� ���� �Ű������� �Լ��� ȣ��
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
    }

    public void SetMasterVolume(float sliderValue) //������ ����
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);  //������ Mathf.Log10 �� ���� ��ȯ����� ��
        PlayerPrefs.SetFloat("Master", sliderValue); //PlayerPrefs �� ����Ƽ���� �������ִ� ������ ���� Ŭ����
    }

    public void SetBgmVolume(float sliderValue)  //����� ����
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGM", sliderValue);
    }

    public void SetSfxVolume(float sliderValue) //ȿ���� ����
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX", sliderValue);
    }
}

