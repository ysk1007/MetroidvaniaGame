using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElunsRobe : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "엘룬의 로브";
        data.itemNameEng = "ElunsRobe";
        data.itemPrice = 1400;
        Color32 color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "희귀";
        data.itemStat = "최대 체력 -25\n방어력 +10\n쿨타임 감소 +15%";
        data.DecreaseCool = 0.15f;
        data.MaxHp = -25;
        data.Def = 10;
        data.itemExplanation = "내가 없어져 볼게";
        data.itemNumber = 46;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
