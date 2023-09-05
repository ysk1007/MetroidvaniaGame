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
        data.itemName = "±¤±â¾î¸° ±¤´ëÀÇ ¹ÙÁö";
        data.itemNameEng = "ClownPants";
        data.itemPrice = 2300;
        data.color = Color.magenta;
        data.Rating = "¿µ¿õ";
        data.itemExplanation = "";
        data.itemStat = "Ä¡¸íÅ¸ È®·ü +10%\nµ¥¹ÌÁö +20%";
        data.itemNumber = 7;
        data.CriticalChance = 0.1f;
        data.DmgIncrease = 0.2f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
