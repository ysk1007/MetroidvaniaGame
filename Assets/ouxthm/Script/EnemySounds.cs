using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beeAtk;

    public AudioClip boarHit;
    public AudioClip boarDie;

    public AudioClip lgHit;
    public AudioClip lgDie;
    public AudioClip lgAtk;

    public AudioClip mrHit;
    public AudioClip mrDie;
    public AudioClip mrAtk;

    public AudioClip necjump;
    public AudioClip necTeleport;
    public AudioClip necBoom;
    public AudioClip necHit;
    public AudioClip necDie;

    public AudioClip slimeHit;
    public AudioClip slimeDie;

    public AudioClip torchHit;
    public AudioClip torchDie;
    public AudioClip torchThrow;

    public AudioClip wolfAtk;
    public AudioClip wolfHit;
    public AudioClip wolfDie;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponentInParent<AudioSource>();
    }
    public void BeeAtk()
    {
        Sounds("beeAtk");
    }
    public void BoarHit()
    {
        Sounds("boarHit");
    }
    public void BoarDie()
    {
        Sounds("boarDie");
    }
    public void LgHit()
    {
        Sounds("lgHit");
    }
    public void LgDie()
    {
        Sounds("lgDie");
    }
    public void LgAtk()
    {
        Sounds("lgAtk");
    }
    public void MrHit()
    {
        Sounds("mrHit");
    }
    public void MrDie()
    {
        Sounds("mrDie");
    }
    public void MrAtk()
    {
        Sounds("mrAtk");
    }
    public void Necjump()
    {
        Sounds("necjump");
    }
    public void NecTeleport()
    {
        Sounds("necTeleport");
    }
    public void NecBoom()
    {
        Sounds("necBoom");
    }
    public void NecHit()
    {
        Sounds("necHit");
    }
    public void NecDie()
    {
        Sounds("necDie");
    }
    public void SlimeHit()
    {
        Sounds("slimeHit");
    }
    public void SlimeDie()
    {
        Sounds("slimeDie");
    }
    public void TorchHit()
    {
        Sounds("torchHit");
    }
    public void TorchDie()
    {
        Sounds("torchDie");
    }
    public void TorchThrow()
    {
        Sounds("torchThrow");
    }
    public void WolfAtk()
    {
        Sounds("wolfAtk");
    }
    public void WolfHit()
    {
        Sounds("wolfHit");
    }
    public void WolfDie()
    {
        Sounds("wolfDie");
    }


    public void Sounds(string sounds)
    {
        switch (sounds)
        {
            case "beeAtk":
                audioSource.clip = beeAtk;
                break;
            case "boarHit":
                audioSource.clip = boarHit;
                break;
            case "boarDie":
                audioSource.clip = boarDie;
                break;
            case "lgHit":
                audioSource.clip = lgHit;
                break;
            case "lgDie":
                audioSource.clip = lgDie;
                break;
            case "lgAtk":
                audioSource.clip = lgAtk;
                break;
            case "mrHit":
                audioSource.clip = mrHit;
                break;
            case "mrDie":
                audioSource.clip = mrDie;
                break;
            case "mrAtk":
                audioSource.clip = mrAtk;
                break;
            case "necjump":
                audioSource.clip = necjump;
                break;
            case "necTeleport":
                audioSource.clip = necTeleport;
                break;
            case "necBoom":
                audioSource.clip = necBoom;
                break;
            case "necHit":
                audioSource.clip = necHit;
                break;
            case "necDie":
                audioSource.clip = necDie;
                break;
            case "slimeHit":
                audioSource.clip = slimeHit;
                break;
            case "slimeDie":
                audioSource.clip = slimeDie;
                break;
            case "torchHit":
                audioSource.clip = torchHit;
                break;
            case "torchDie":
                audioSource.clip = torchDie;
                break;
            case "torchThrow":
                audioSource.clip = torchThrow;
                break;
            case "wolfAtk":
                audioSource.clip = wolfAtk;
                break;
            case "wolfHit":
                audioSource.clip = wolfHit;
                break;
            case "wolfDie":
                audioSource.clip = wolfDie;
                break;
        }
        audioSource.Play();
    }
}
