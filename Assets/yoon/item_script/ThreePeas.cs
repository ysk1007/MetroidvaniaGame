using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreePeas : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "완두콩";
        data.itemNameEng = "ThreePeas";
        data.itemPrice = 200;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemExplanation = "가성비가 좋다.";
        data.itemStat = "체력 재생 +0.3";
        data.itemNumber = 37;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen -= 0.3f;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen += 0.3f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
