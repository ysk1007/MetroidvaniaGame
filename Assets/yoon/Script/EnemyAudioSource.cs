using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioSource : MonoBehaviour
{
    public AudioSource[] EnemyAudio;
    public static EnemyAudioSource instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyAudio = gameObject.GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundOff()
    {
        for (int i = 0; i < EnemyAudio.Length; i++)
        {
            if (EnemyAudio[i] != null)
            {
                EnemyAudio[i].mute = true;
            }
        }
    }

    public void SoundOn()
    {
        for (int i = 0; i < EnemyAudio.Length; i++)
        {
            if (EnemyAudio[i] != null)
            {
                EnemyAudio[i].mute = false;
            }
        }
    }
}
