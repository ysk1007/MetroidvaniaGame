using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownBoots : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "광기어린 광대의 신발";
        data.itemNameEng = "ClownBoots";
        data.itemPrice = 2400;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "";
        data.itemStat = "치명타 확률 +10%\n쿨타임 감소 +10%\n이동속도 +5%";
        data.itemNumber = 8;
        data.CriticalChance = 0.1f;
        data.DecreaseCool = 0.1f;
        data.Speed = 0.25f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
