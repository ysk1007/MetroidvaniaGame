using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElunsWand : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "엘룬의 완드";
        data.itemNameEng = "ElunsWand";
        data.itemPrice = 2200;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemStat = "공격력 +8\n데미지 +5%\n쿨타임 감소 +20%";
        data.DecreaseCool = 0.20f;
        data.AtkPower = 8;
        data.DmgIncrease = 0.05f;
        data.itemExplanation = "\"마력의 힘이 느껴지나?\"";
        data.itemNumber = 47;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
