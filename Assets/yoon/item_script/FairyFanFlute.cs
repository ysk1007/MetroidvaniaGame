using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FairyFanFlute : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ���÷�";
        data.itemNameEng = "FairyFanFlute";
        data.itemPrice = 800;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "ġ��Ÿ Ȯ�� +6%\nġ��Ÿ ���ط� +6%\n�̵� �ӵ� +5%";
        data.itemExplanation = "���� ��� ������ ���� �Ҹ��� ����´�.";
        data.itemNumber = 48;
        data.CriticalChance = 0.06f;
        data.CriDmgIncrease = 0.06f;
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
