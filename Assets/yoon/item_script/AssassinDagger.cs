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
        data.itemPrice = 2500;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "���� ������ +50%\n���� �ӵ� +30%\n��� ȹ�淮 +20%";
        data.itemNumber = 28;
        data.BleedDmg = +5f;
        data.AtkSpeed = 0.3f;
        data.GoldGet = 0.2f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
