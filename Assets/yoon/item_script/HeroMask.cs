using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroMask : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "히어로 가면";
        data.itemNameEng = "HeroMask";
        data.itemPrice = 1500;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemExplanation = "성장형 주인공";
        data.itemStat = "최대 체력 +20\n방어력 +10\n경험치 획득량 +15%";
        data.itemNumber = 35;
        data.Def = 10;
        data.MaxHp = 20;
        data.EXPGet = 0.15f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
