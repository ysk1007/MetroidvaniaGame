using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrokenWatch : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� �ʽð�";
        data.itemNameEng = "BrokenWatch";
        data.itemPrice = 433;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "�̹� ���� ����� ���ϴ�.";    
        data.itemStat = "��Ÿ�� ���� +10%\n�̵��ӵ� +10%";
        data.itemNumber = 23;
        data.DecreaseCool = 0.1f;
        data.Speed = 0.1f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}