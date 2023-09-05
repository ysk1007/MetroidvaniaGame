using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrokenWatch : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "깨진 초시계";
        data.itemNameEng = "BrokenWatch";
        data.itemPrice = 823;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemExplanation = "이미 누가 사용한 듯하다.";    
        data.itemStat = "쿨타임 감소 +10%\n이동속도 +5%";
        data.itemNumber = 23;
        data.DecreaseCool = 0.1f;
        data.Speed = 0.25f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
