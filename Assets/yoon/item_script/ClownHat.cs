using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownHat : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ ����";
        data.itemNameEng = "ClownHat";
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemPrice = 2400;
        data.itemExplanation = "\"���� ������ ���� ��� ��ġ�ֱ⸦.\"";
        data.itemStat = "���ݷ� +5 \nġ��Ÿ Ȯ�� +10%";
        data.itemNumber = 5;
        data.AtkPower = 5;
        data.CriticalChance = 0.1f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText , TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
