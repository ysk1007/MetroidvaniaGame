using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //오디오 관련 참조
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer; //믹서를 담을 변수
    public AudioSource bgSound; //배경음악을 담을 변수
    public static SoundManager instance; //사운드 매니저 인스턴스(클래스의 정의를 통해 만들어진 객체)
    public AudioClip[] bglist; //배경음악들을 담을 배열
    SoundSlider soundSlider;
    private void Awake()
    {
        //싱글톤 패턴
        if(instance == null) //사운드 매니저가 없으면
        {
            instance = this; //사운드 매니저는 자신
            DontDestroyOnLoad(instance); //씬 전환시 파괴되지 않음
            SceneManager.sceneLoaded += OnSceneLoaded; //씬 전환할때 OnSceneLoaded 함수
        }
        else //사운드 매니저가 이미 있으면
        {
            Destroy(gameObject); //자신은 파괴
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) //씬 변경시 씬의 이름과 클립의 이름이 같은 배경음을 실행
    {
        for (int i = 0; i < bglist.Length; i++) //for문으로 배경리스트를 탐색해 같은 이름의 배경음을 찾음
        {
            if (arg0.name == bglist[i].name)
            BgmPlay(bglist[i]); //찾으면 실행
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip) //효과음 실행 함수 매개변수는 이름(아무거나 상관없음), 효과음 클립
    {
        GameObject go =new GameObject(sfxName + "Sound"); //효과음을 발생시킬 오브젝트를 생성 오브젝트의 이름은 매개변수로 받음
        AudioSource audioSource = go.AddComponent<AudioSource>(); 
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0]; //아웃풋 믹스를 SFX 음량으로 설정
        audioSource.clip = clip; //클립 받음
        audioSource.Play(); //실행

        Destroy(go,clip.length); //클립 길이만큼 실행하고 오브젝트 파괴
    }

    public void BgmPlay(AudioClip clip) //배경음 실행 함수 매개변수는 배경음 클립
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0]; //아웃풋 믹스를 BGM 음량으로 설정
        bgSound.clip = clip; //클립 받음
        bgSound.loop = true; //루프반복 설정
        bgSound.volume = 0.1f; //기본 볼륨
        bgSound.Play(); //실행
    }

}
