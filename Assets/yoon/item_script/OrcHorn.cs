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
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemStat = "공격력 +3\n최대 체력 +10\n방어력 +2";
        data.itemExplanation = "\'뿌우-\'";
        data.itemNumber = 49;
        data.AtkPower = 3;
        data.MaxHp = 10;
        data.Def = 2;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
