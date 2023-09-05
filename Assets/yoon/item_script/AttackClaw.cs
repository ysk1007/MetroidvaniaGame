using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackClaw : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�������� ����";
        data.itemNameEng = "AttackClaw";
        data.itemPrice = 500;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "���� ������!";
        data.itemStat = "���� �ӵ� +15%";
        data.itemNumber = 25;
        data.AtkSpeed = 0.15f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
