using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RootOfTree : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "세계수의 뿌리";
        data.itemNameEng = "RootOfTree";
        data.itemPrice = 3200;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "모든 지병을 낫게 해준다는 전설의 약초";
        data.itemStat = "최대 체력 +50\n방어력 +20\n이동 속도 -5%";
        data.itemNumber = 16;
        data.MaxHp = 50;
        data.Def = 20;
        data.Speed = -0.25f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
