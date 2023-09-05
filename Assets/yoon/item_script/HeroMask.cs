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
        data.itemPrice = 1500;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "������ ���ΰ�";
        data.itemStat = "�ִ� ü�� +20\n���� +10\n����ġ ȹ�淮 +15%";
        data.itemNumber = 35;
        data.Def = 10;
        data.MaxHp = 20;
        data.EXPGet = 0.15f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
