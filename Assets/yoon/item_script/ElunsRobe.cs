using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElunsRobe : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ �κ�";
        data.itemNameEng = "ElunsRobe";
        data.itemPrice = 1400;
        Color32 color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemStat = "�ִ� ü�� -25\n���� +10\n��Ÿ�� ���� +15%";
        data.DecreaseCool = 0.15f;
        data.MaxHp = -25;
        data.Def = 10;
        data.itemExplanation = "���� ������ ����";
        data.itemNumber = 46;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
