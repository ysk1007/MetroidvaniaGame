using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownPants : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "광기어린 광대의 바지";
        data.itemNameEng = "ClownPants";
        data.itemPrice = 1600;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "";
        data.itemStat = "공격속도 +30% \n치명타 확률 +20%";
        data.itemNumber = 7;
        data.AtkSpeed = 0.3f;
        data.CriticalChance = 0.2f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
