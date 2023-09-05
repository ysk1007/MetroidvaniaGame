using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroMask : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "����� ����";
        data.itemNameEng = "HeroMask";
        data.itemPrice = 1000;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "������ ���ΰ�";
        data.itemStat = "�ִ� ü�� +7\n���� +5\n����ġ ȹ�淮 +20%";
        data.itemNumber = 35;
        data.Def = 5;
        data.MaxHp = 7;
        data.EXPGet = 0.2f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
