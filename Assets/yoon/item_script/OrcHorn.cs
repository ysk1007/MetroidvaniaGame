using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrcHorn : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "오크의 뿔피리";
        data.itemNameEng = "OrcHorn";
        data.itemPrice = 800;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemStat = "공격력 +5\n공격 속도 +5%\n치명타 피해량 +5%";
        data.itemExplanation = "\'뿌우-\'";
        data.itemNumber = 49;
        data.AtkPower = 5;
        data.AtkSpeed = 0.05f;
        data.CriDmgIncrease = 0.05f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
