using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NecPotion : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "네크의 비약";
        data.itemNameEng = "NecPotion";
        data.itemPrice = 0;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemStat = "두 번째 재료";
        data.itemExplanation = "정신을 맑게 해주는 비약이다.";
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
