using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Club : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������";
        data.itemNameEng = "Club";
        data.itemPrice = 300;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "�Ǹ��� ��ȭ����";
        data.itemStat = "���ݷ� +3 \n�ִ�ü�� +10";
        data.itemNumber = 2;
        data.AtkPower = 3;
        data.MaxHp = 10;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
