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
        data.itemPrice = 2222;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemExplanation = "���� �ٶ󺸴� �ڿ��� ���� �ش�.";
        data.itemStat = "��� �ɷ�ġ +5";
        data.itemNumber = 33;
        data.DmgIncrease = 0.05f;
        data.CriticalChance = 0.05f;
        data.CriDmgIncrease = 0.05f;
        data.AtkPower = 5;
        data.Def = 5;
        data.AtkSpeed = 0.05f;
        data.Speed = 0.05f;
        data.MaxHp = 5;
        data.JumpPower = 0.75f;
        data.lifeStill = 0.05f;
        data.GoldGet = 0.05f;
        data.EXPGet = 0.05f;
        data.DecreaseCool = 0.05f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
