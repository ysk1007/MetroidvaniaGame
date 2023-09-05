using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThreePeas : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�ϵ���";
        data.itemNameEng = "ThreePeas";
        data.itemPrice = 200;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "������ ����.";
        data.itemStat = "ü�� ��� +0.3";
        data.itemNumber = 37;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen -= 0.3f;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen += 0.3f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
