using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoubleEdgedAxe : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "È²±Ý ¾ç³¯ µµ³¢";
        data.itemNameEng = "DoubleEdgedAxe";
        data.itemPrice = 2500;
        data.color = Color.magenta;
        data.Rating = "¿µ¿õ";
        data.itemExplanation = "";
        data.itemStat = "µµ³¢ Â÷Â¡¼Óµµ +50%\n°ø°Ý·Â +10\n°ñµå È¹µæ·® +20%";
        data.itemNumber = 29;
        data.ChargingTime = 1f;
        data.AtkPower = 10;
        data.GoldGet = 0.2f;
        data.Speed = -0.25f;
        data.JumpPower = -0.75f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
