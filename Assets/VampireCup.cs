using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VampireCup : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "뱀파이어의 잔";
        data.itemNameEng = "VampireCup";
        data.itemPrice = 1800;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "희귀";
        data.itemExplanation = "\"포도주스 아니라고!\"";
        data.itemStat = "공격력 +5\n생명력 흡수 +1%\n최대 체력 -10";
        data.itemNumber = 13;
        data.AtkPower = 5;
        data.lifeStill = 0.01f;
        data.MaxHp = -10;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
