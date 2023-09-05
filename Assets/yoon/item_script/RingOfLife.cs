using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingOfLife : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ����";
        data.itemNameEng = "RingOfLife";
        data.itemPrice = 1000;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "�ִ� ü�� +10\nü�� ��� +0.5";
        data.itemExplanation = "������ ��� ����";
        data.itemNumber = 44;
        data.MaxHp = 10;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen -= 0.5f;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen += 0.5f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
