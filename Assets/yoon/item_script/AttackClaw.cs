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
        data.itemName = "선제공격 발톱";
        data.itemNameEng = "AttackClaw";
        data.itemPrice = 500;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemExplanation = "먼저 때리기!";
        data.itemStat = "공격 속도 +15%";
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
