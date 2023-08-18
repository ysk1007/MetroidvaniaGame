using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "±¤±â¾î¸° ±¤´ëÀÇ Àå°©";
        data.itemNameEng = "ClownGloves";
        data.itemPrice = 1200;
        data.color = Color.magenta;
        data.Rating = "¿µ¿õ";
        data.itemExplanation = "\"¶È¶È\"";
        data.itemStat = "°ø°Ý·Â +3 \nÄ¡¸íÅ¸ È®·ü +20%";
        data.itemNumber = 6;
        data.AtkPower = 3;
        data.CriticalChance = 0.2f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText , TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
