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
        data.itemName = "�����̾��� ��";
        data.itemNameEng = "VampireCup";
        data.itemPrice = 1600;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemExplanation = "\"�����ֽ� �ƴ϶��!\"";
        data.itemStat = "���ݷ� +13\n����� ��� +1%";
        data.itemNumber = 13;
        data.AtkPower = 13;
        data.lifeStill = 0.01f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
