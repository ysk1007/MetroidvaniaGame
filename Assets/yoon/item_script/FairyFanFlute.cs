using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FairyFanFlute : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "요정의 팬플룻";
        data.itemNameEng = "FairyFanFlute";
        data.itemPrice = 1000;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemStat = "치명타 확률 +10%\n치명타 피해량 +20%";
        data.itemExplanation = "숲속 어딘가 요정의 연주 소리가 들려온다.";
        data.itemNumber = 48;
        data.CriticalChance = 0.10f;
        data.CriDmgIncrease = 0.20f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
