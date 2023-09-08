using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoveLetter : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "장문의 러브 레터";
        data.itemNameEng = "LoveLetter";
        data.itemPrice = 1366;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "희귀";
        data.itemExplanation = "고백으로 혼내주자!";
        data.itemStat = "공격력 +4\n데미지 +8%\n공격속도 +6%";
        data.itemNumber = 33;
        data.AtkPower = 4;
        data.DmgIncrease = 0.08f;
        data.AtkSpeed = 0.06f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
