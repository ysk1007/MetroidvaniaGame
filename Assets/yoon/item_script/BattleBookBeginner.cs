using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleBookBeginner : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "전투의 정석 [기본편]";
        data.itemNameEng = "BattleBook";
        data.itemPrice = 600;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemStat = "공격력 +5 \n공격속도 +12%\n이동속도 +13%";
        data.itemNumber = 11;
        data.AtkPower = 5;
        data.AtkSpeed = 0.12f;
        data.Speed = 0.13f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
