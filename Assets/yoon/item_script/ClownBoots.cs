using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownBoots : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ �Ź�";
        data.itemNameEng = "ClownBoots";
        data.itemPrice = 2400;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "ġ��Ÿ Ȯ�� +10%\n��Ÿ�� ���� +10%\n�̵��ӵ� +5%";
        data.itemNumber = 8;
        data.CriticalChance = 0.1f;
        data.DecreaseCool = 0.1f;
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
