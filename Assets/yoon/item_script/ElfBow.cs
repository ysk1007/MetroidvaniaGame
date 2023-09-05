using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElfBow : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "엘프 궁수의 활";
        data.itemNameEng = "ElfBow";
        data.itemPrice = 2500;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "";
        data.itemStat = "화살 사거리 +50%\n치명타 확률 +10%\n치명타 피해량 +20%";
        data.itemNumber = 30;
        data.ArrowDis = 0.375f;
        data.CriticalChance = 0.1f;
        data.CriDmgIncrease = 0.2f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
