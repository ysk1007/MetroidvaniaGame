using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "달콤한 초코 쿠키";
        data.itemNameEng = "Cookie";
        data.itemPrice = 300;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemExplanation = "내가 만든 쿠키~";
        data.itemStat = "최대 체력 +30\n 방어력 +10";
        data.itemNumber = 14;
        data.MaxHp = 30;
        data.Def = 10;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
