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
        data.itemPrice = 150;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemExplanation = "������ ����";
        data.itemStat = "ü�� ��� +1";
        data.itemNumber = 37;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen -= 1;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen += 1;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
