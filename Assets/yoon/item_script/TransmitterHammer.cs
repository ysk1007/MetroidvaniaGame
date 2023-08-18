using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransmitterHammer : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���ۺ��� ��ġ";
        data.itemNameEng = "TransmitterHammer";
        data.itemPrice = 526;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "\"���� ���� �� �ڳ�\"";
        data.itemStat = "���ݷ� +5\n���� ��¡�ӵ� +15% ";
        data.itemNumber = 37;
        data.AtkPower = 5;
        data.ChargingTime = 0.3f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
