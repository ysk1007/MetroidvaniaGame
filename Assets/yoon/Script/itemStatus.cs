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
    public string itemExplanation;
    public string itemStat;
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

    public virtual void Using(Image img,TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        TextImageSettings(img, NameText, ExplanationText, StatText);
    }

    public virtual void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        img.sprite = this.data.itemimg.sprite;
        img.SetNativeSize();
        RectTransform rect = (RectTransform)img.transform;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * 5, rect.sizeDelta.y * 5);
        img.rectTransform.sizeDelta = rect.sizeDelta;
        img.color = new Color32(255, 255, 255, 255);
        NameText.text = this.data.itemNumber.ToString()+". " + this.data.itemName;
        ExplanationText.text = this.data.itemExplanation;
        StatText.text = this.data.itemStat;
    }
}
