using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpPotion : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "체력 물약";
        data.itemNameEng = "HpPotion";
        data.itemPrice = 300;
        data.color = Color.white;
        data.Rating = "소모품";
        data.itemExplanation = "플레이어의 체력을 회복합니다";
        data.itemStat = "회복량 +50%";
    }
    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
