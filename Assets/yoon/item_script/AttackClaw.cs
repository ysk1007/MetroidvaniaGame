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
        data.itemPrice = 600;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "���� ������!";
        data.itemStat = "���ݼӵ� +50%";
        data.itemNumber = 24;
        data.AtkSpeed = 0.5f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
