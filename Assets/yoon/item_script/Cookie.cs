using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cookie : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ���� ��Ű";
        data.itemNameEng = "Cookie";
        data.itemPrice = 150;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "���� ���� ��Ű~";
        data.itemStat = "�ִ� ü�� +5\n ���� +3";
        data.itemNumber = 14;
        data.MaxHp = 5;
        data.Def = 3;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
