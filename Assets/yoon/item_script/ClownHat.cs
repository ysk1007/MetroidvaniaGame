using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownHat : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "광기어린 광대의 모자";
        data.itemNameEng = "ClownHat";
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemPrice = 2400;
        data.itemExplanation = "\"나의 죽음이 나의 삶보다 가치있기를.\"";
        data.itemStat = "공격력 +5 \n치명타 확률 +10%";
        data.itemNumber = 5;
        data.AtkPower = 5;
        data.CriticalChance = 0.1f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText , TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
