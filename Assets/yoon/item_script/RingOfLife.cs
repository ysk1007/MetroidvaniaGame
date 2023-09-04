using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingOfLife : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "생명의 반지";
        data.itemNameEng = "RingOfLife";
        data.itemPrice = 1000;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemStat = "최대 체력 +30\n체력 재생 +2";
        data.itemExplanation = "";
        data.itemNumber = 44;
        data.MaxHp = 30;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen -= 2;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen += 2;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
