using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssassinDagger : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "Ȳ�� �ܰ�";
        data.itemNameEng = "AssassinDagger";
        data.itemPrice = 2700;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "���� ������ +100%\n���ݼӵ� +40%\n���� -15";
        data.itemNumber = 28;
        data.BleedDmg = +3f;
        data.AtkSpeed = 0.4f;
        data.Def = -15;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
