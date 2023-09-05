using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElunsHat: itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ����";
        data.itemNameEng = "ElunsHat";
        data.itemPrice = 1500;
        Color32 color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemStat = "���ݷ� +5\n������ +5%\n��Ÿ�� ���� +10%";
        data.AtkPower = 5;
        data.DmgIncrease = 0.05f;
        data.DecreaseCool = 0.10f;
        data.itemExplanation = "";
        data.itemNumber = 45;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
