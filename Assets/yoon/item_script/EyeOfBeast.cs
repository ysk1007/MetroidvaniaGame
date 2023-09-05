using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EyeOfBeast : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "시모의 눈";
        data.itemNameEng = "EyeOfBeast";
        data.itemPrice = 3800;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "사악한 맹수 시모의 눈이다. 아주 사악하다.";
        data.itemStat = "화살 사거리 +50%\n공격속도 +60%\n공격력 +15\n이동속도 -20%";
        data.itemNumber = 24;
        data.AtkPower = 15;
        data.AtkSpeed = 0.6f;
        data.ArrowDis = 0.375f;
        data.Speed = -0.8f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
