using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgSound;
    public static SoundManager instance;
    public AudioClip[] bglist;
    public AudioClip[] BossBglist;
    public AudioClip[] StageBglist;
    public AudioClip marketBGM;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SliderSetting();
    }

    public void SliderSetting()
    {
        List<float> Volumes = new List<float>();
        Volumes = DataManager.instance.getVolume();
        mixer.SetFloat("Master", Mathf.Log10(Volumes[0]) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(Volumes[1]) * 20);
        mixer.SetFloat("BGM", Mathf.Log10(Volumes[2]) * 20);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
            BgmPlay(bglist[i]);
        }

        
    }

    public void BGM_Volume(float val)
    {
        mixer.SetFloat("BGM", Mathf.Log10(val) * 20);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go =new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go,clip.length);
    }

    public void BgmPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.05f;
        bgSound.Play();
    }

    public void BossStage(int stage)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = BossBglist[stage - 1];
        bgSound.loop = true;
        bgSound.volume = 0.05f;
    }

    public void MarketStage()
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = marketBGM;
        bgSound.loop = true;
        bgSound.volume = 0.05f;
    }

    public void Loading()
    {
        bgSound.Pause();
    }

    public void SoundUp()
    {
        bgSound.Play();
    }

    public void Stage(int stage)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgSound.clip = StageBglist[stage - 1];
        bgSound.loop = true;
        bgSound.volume = 0.05f;
    }
}
