using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //����� ���� ����
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer; //�ͼ��� ���� ����
    public AudioSource bgSound; //��������� ���� ����
    public static SoundManager instance; //���� �Ŵ��� �ν��Ͻ�(Ŭ������ ���Ǹ� ���� ������� ��ü)
    public AudioClip[] bglist; //������ǵ��� ���� �迭
    SoundSlider soundSlider;
    private void Awake()
    {
        //�̱��� ����
        if(instance == null) //���� �Ŵ����� ������
        {
            instance = this; //���� �Ŵ����� �ڽ�
            DontDestroyOnLoad(instance); //�� ��ȯ�� �ı����� ����
            SceneManager.sceneLoaded += OnSceneLoaded; //�� ��ȯ�Ҷ� OnSceneLoaded �Լ�
        }
        else //���� �Ŵ����� �̹� ������
        {
            Destroy(gameObject); //�ڽ��� �ı�
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1) //�� ����� ���� �̸��� Ŭ���� �̸��� ���� ������� ����
    {
        for (int i = 0; i < bglist.Length; i++) //for������ ��渮��Ʈ�� Ž���� ���� �̸��� ������� ã��
        {
            if (arg0.name == bglist[i].name)
            BgmPlay(bglist[i]); //ã���� ����
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip) //ȿ���� ���� �Լ� �Ű������� �̸�(�ƹ��ų� �������), ȿ���� Ŭ��
    {
        GameObject go =new GameObject(sfxName + "Sound"); //ȿ������ �߻���ų ������Ʈ�� ���� ������Ʈ�� �̸��� �Ű������� ����
        AudioSource audioSource = go.AddComponent<AudioSource>(); 
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0]; //�ƿ�ǲ �ͽ��� SFX �������� ����
        audioSource.clip = clip; //Ŭ�� ����
        audioSource.Play(); //����

        Destroy(go,clip.length); //Ŭ�� ���̸�ŭ �����ϰ� ������Ʈ �ı�
    }

    public void BgmPlay(AudioClip clip) //����� ���� �Լ� �Ű������� ����� Ŭ��
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0]; //�ƿ�ǲ �ͽ��� BGM �������� ����
        bgSound.clip = clip; //Ŭ�� ����
        bgSound.loop = true; //�����ݺ� ����
        bgSound.volume = 0.1f; //�⺻ ����
        bgSound.Play(); //����
    }

}
