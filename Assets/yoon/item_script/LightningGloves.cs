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
        data.itemName = "���� �尩";
        data.itemNameEng = "LightningGloves";
        data.itemPrice = 900;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "���ݼӵ� +75% \n���ط� ���� -15%";
        data.itemNumber = 1;
        data.DmgIncrease = -0.15f;
        data.AtkSpeed = 0.75f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
