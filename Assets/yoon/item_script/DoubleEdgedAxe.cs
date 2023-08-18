using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoubleEdgedAxe : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "Ȳ�� �糯 ����";
        data.itemNameEng = "DoubleEdgedAxe";
        data.itemPrice = 2700;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "���� ��¡�ӵ� +50%\n���ݷ� +20\n������ -5%";
        data.itemNumber = 28;
        data.ChargingTime = 1f;
        data.AtkPower = 20;
        data.JumpPower = -0.75f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
