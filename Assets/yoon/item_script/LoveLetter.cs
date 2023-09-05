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
        data.itemPrice = 2000;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "희귀";
        data.itemExplanation = "고백으로 혼내주자!";
        data.itemStat = "공격력 +20\n공격속도 +50%";
        data.itemNumber = 33;
        data.AtkPower = 20;
        data.AtkSpeed = 0.5f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
