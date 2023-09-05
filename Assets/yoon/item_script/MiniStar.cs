using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniStar : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ��";
        data.itemNameEng = "MiniStar";
        data.itemPrice = 2000;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemExplanation = "���� �ٶ󺸴� �ڿ��� ���� �ش�.";
        data.itemStat = "��� �ɷ�ġ +3";
        data.itemNumber = 34;
        data.DmgIncrease = 0.03f;
        data.CriticalChance = 0.03f;
        data.CriDmgIncrease = 0.03f;
        data.AtkPower = 3;
        data.Def = 3;
        data.AtkSpeed = 0.03f;
        data.Speed = 0.15f;
        data.MaxHp = 3;
        data.lifeStill = 0.003f;
        data.GoldGet = 0.03f;
        data.EXPGet = 0.03f;
        data.DecreaseCool = 0.03f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
