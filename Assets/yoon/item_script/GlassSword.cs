using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlassSword : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "유리 검";
        data.itemNameEng = "GlassSword";
        data.itemPrice = 3600;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "스치기만 해도 치명타!";
        data.itemStat = "데미지 +100%\n 방어력 -50";
        data.itemNumber = 18;
        data.DmgIncrease = 1f;
        data.Def = -50;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
