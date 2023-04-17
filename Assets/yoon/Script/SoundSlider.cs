using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //오디오 사용 관련 참조
using UnityEngine.UI; //UI 사용 관련 참조

public class SoundSlider : MonoBehaviour
{
    public AudioMixer mixer; //믹서를 담을 변수

    //각 사운드 슬라이더를 담을 변수
    public Slider bgm_slider;
    public Slider sfx_slider;
    public Slider master_slider;
    void Start()
    {
        //시작할때 저장했던 음략 설정 세팅
        SetMasterVolume(PlayerPrefs.GetFloat("Master"));
        SetBgmVolume(PlayerPrefs.GetFloat("BGM"));
        SetSfxVolume(PlayerPrefs.GetFloat("SFX"));
        master_slider.value = PlayerPrefs.GetFloat("Master");
        bgm_slider.value = PlayerPrefs.GetFloat("BGM");
        sfx_slider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void FixedUpdate()
    {
        //슬라이더의 밸류값이 변할때 그 값을 매개변수로 함수를 호출
        SetMasterVolume(master_slider.value);
        SetBgmVolume(bgm_slider.value);
        SetSfxVolume(sfx_slider.value);
    }

    public void SetMasterVolume(float sliderValue) //마스터 음량
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);  //음량은 Mathf.Log10 로 값을 변환해줘야 함
        PlayerPrefs.SetFloat("Master", sliderValue); //PlayerPrefs 는 유니티에서 제공해주는 데이터 관리 클래스
    }

    public void SetBgmVolume(float sliderValue)  //배경음 음량
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGM", sliderValue);
    }

    public void SetSfxVolume(float sliderValue) //효과음 음량
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX", sliderValue);
    }
}

