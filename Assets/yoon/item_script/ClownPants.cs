using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownPants : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ ����";
        data.itemNameEng = "ClownPants";
        data.itemPrice = 1600;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "���ݼӵ� +30% \nġ��Ÿ Ȯ�� +20%";
        data.itemNumber = 7;
        data.AtkSpeed = 0.3f;
        data.CriticalChance = 0.2f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
