using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ocarina : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "��ī����";
        data.itemNameEng = "Ocarina";
        data.itemPrice = 800;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "��Ÿ�� ���� +10%\n���� �ӵ� +10%\n�̵��ӵ� +5%";
        data.itemNumber = 42;
        data.DecreaseCool = 0.10f;
        data.AtkSpeed = 0.10f;
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
