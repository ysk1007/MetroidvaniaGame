using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public struct Data
{
    public Image itemimg;
    public string itemName;
    public string itemExplanation;
    public int itemNumber;
    public int AtkPower;
    public float AtkSpeed;
    public int Def;
    public int MaxHp;
    public float Speed;
    public float CriticalChance;
}

public abstract class itemStatus : MonoBehaviour
{
    public Data data;

    public abstract void InitSetting();

    public virtual void Using(Image img,TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText)
    {
        TextImageSettings(img, NameText, ExplanationText);
    }

    public virtual void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText)
    {
        img.sprite = this.data.itemimg.sprite;
        img.color = new Color32(255, 255, 255, 255);
        NameText.text = this.data.itemName;
        ExplanationText.text = this.data.itemExplanation;
    }
}
