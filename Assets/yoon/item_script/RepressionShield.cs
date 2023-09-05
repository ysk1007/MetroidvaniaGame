using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepressionShield : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "진압 방패";
        data.itemNameEng = "RepressionShield";
        data.itemPrice = 2150;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "\"FBI open up!!!\"";
        data.itemStat = "피해를 입고 밀려나지 않습니다.\n최대 체력 +10\n방어력 +15";
        data.itemNumber = 36;
        data.MaxHp = 15;
        data.Def = 15;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.NoNockback = false;
        }
        if (data.SpecialPower)
        {
            Player.instance.NoNockback = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
