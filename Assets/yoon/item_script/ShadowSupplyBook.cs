using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSupplyBook : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "그림자 비급서";
        data.itemNameEng = "ShadowSupplyBook";
        data.itemPrice = 3500;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "하지만 간지 디@졌잖아";
        data.itemStat = "공격 속도 +150%\n데미지 -75%";
        data.itemNumber = 51;
        data.AtkSpeed = 1.5f;
        data.DmgIncrease = -0.75f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.GetComponent<afterImageGenerator>().Active = false;
        }
        if (data.SpecialPower)
        {
            p.GetComponent<afterImageGenerator>().Active = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
