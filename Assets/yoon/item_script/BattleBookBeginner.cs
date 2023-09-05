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
        data.itemPrice = 500;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemStat = "공격력 +2\n공격 속도 +2%\n최대 체력 +2\n방어력 +2";
        data.itemNumber = 11;
        data.AtkPower = 2;
        data.AtkSpeed = 0.02f;
        data.Def = 2;
        data.MaxHp = 2;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
