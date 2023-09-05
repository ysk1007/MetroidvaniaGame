using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownCloth : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "±¤±â¾î¸° ±¤´ëÀÇ Æ©´Ð";
        data.itemNameEng = "ClownCloth";
        data.itemPrice = 2000;
        data.color = Color.magenta;
        data.Rating = "¿µ¿õ";
        data.itemExplanation = "\"Why so serious?\"";
        data.itemStat = "Ä¡¸íÅ¸ È®·ü +10%\nÄ¡¸íÅ¸ ÇÇÇØ·® +10%";
        data.itemNumber = 4;
        data.CriticalChance = 0.1f;
        data.CriDmgIncrease = 0.1f;

    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText , TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
