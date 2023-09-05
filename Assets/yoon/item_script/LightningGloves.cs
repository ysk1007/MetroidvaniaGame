using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightningGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "¹ø°³ Àå°©";
        data.itemNameEng = "LightningGloves";
        data.itemPrice = 1500;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "Èñ±Í";
        data.itemStat = "°ø°Ý¼Óµµ +45% \nµ¥¹ÌÁö -15%";
        data.itemNumber = 10;
        data.DmgIncrease = -0.15f;
        data.AtkSpeed = 0.45f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
