using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GolemPower : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "피조물의 동력원";
        data.itemNameEng = "GolemPower";
        data.itemPrice = 0;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemStat = "세 번째 재료";
        data.itemExplanation = "엄청난 마력과 생명력이 깃들어 있다.";
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
