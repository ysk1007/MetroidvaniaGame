using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public struct Data
{
    public Image itemimg;
    public AudioClip equipSfx;
    public string itemName;
    public string itemNameEng;
    public float itemPrice;
    public Color color;
    public string Rating;
    public string itemExplanation;
    public string itemStat;
    public int itemNumber;
    public int AtkPower;
    public float DmgIncrease;
    public float AtkSpeed;
    public int Def;
    public int MaxHp;
    public float Speed;
    public float JumpPower;
    public float CriticalChance;
    public float CriDmgIncrease;
    public float GoldGet;
    public float EXPGet;
    public float lifeStill;
    public float DecreaseCool;
    public float ArrowDis;
    public float BleedDmg;
    public float ChargingTime;
    public bool SpecialPower;
}

public abstract class itemStatus : MonoBehaviour
{
    public Data data;

    public abstract void InitSetting();
    public abstract void SpecialPower();

    public virtual void Using(Image img,TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }

    public virtual void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        img.sprite = this.data.itemimg.sprite;
        img.SetNativeSize();
        RectTransform rect = (RectTransform)img.transform;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * 5, rect.sizeDelta.y * 5);
        img.rectTransform.sizeDelta = rect.sizeDelta;
        img.color = new Color32(255, 255, 255, 255);
        NameText.text = /*this.data.itemNumber.ToString()+". " + */this.data.itemName;
        NameText.color = this.data.color;
        RatingText.color = this.data.color;
        ExplanationText.text = this.data.itemExplanation;
        StatText.text = this.data.itemStat;
        PriceText.text += this.data.itemPrice.ToString();
        RatingText.text = this.data.Rating;
    }

    public virtual void StatusGet(Player player)
    {
        player.AtkPower += data.AtkPower;
        player.ATS += data.AtkSpeed;
        player.delayTime = -0.4f * player.ATS + 1.4f;
        player.Def += data.Def;
        player.MaxHp += data.MaxHp;
        player.jumpPower += data.JumpPower;
        player.SpeedChange += data.Speed;
        player.CriticalChance += data.CriticalChance;
        player.anim.SetFloat("AttackSpeed", player.ATS);
        player.GoldGet += data.GoldGet;
        player.EXPGet += data.EXPGet;
        player.CriDmgIncrease += data.CriDmgIncrease;
        player.DmgIncrease += data.DmgIncrease;
        player.DecreaseCool += data.DecreaseCool;
        player.lifeStill += data.lifeStill;
        player.ArrowDistance += data.ArrowDis;
        player.bleedDamage += data.BleedDmg;
        player.chargingTime -= data.ChargingTime;
    }

    public virtual void StatusReturn(Player player)
    {
        player.AtkPower -= data.AtkPower;
        player.ATS -= data.AtkSpeed;
        player.delayTime = -0.4f * player.ATS + 1.4f;
        player.Def -= data.Def;
        player.MaxHp -= data.MaxHp;
        player.jumpPower -= data.JumpPower;
        player.SpeedChange -= data.Speed;
        player.CriticalChance -= data.CriticalChance;
        player.anim.SetFloat("AttackSpeed", player.ATS);
        player.GoldGet -= data.GoldGet;
        player.EXPGet -= data.EXPGet;
        player.CriDmgIncrease -= data.CriDmgIncrease;
        player.DmgIncrease -= data.DmgIncrease;
        player.DecreaseCool -= data.DecreaseCool;
        player.lifeStill -= data.lifeStill;
        player.ArrowDistance -= data.ArrowDis;
        player.bleedDamage -= data.BleedDmg;
        player.chargingTime += data.ChargingTime;
    }
}
