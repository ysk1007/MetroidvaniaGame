using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodenShield : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "나무 방패";
        data.itemNameEng = "WoodenShield";
        data.itemPrice = 300;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemStat = "방어력 +5";
        data.itemExplanation = "";
        data.itemNumber = 50;
        data.Def = 5;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
