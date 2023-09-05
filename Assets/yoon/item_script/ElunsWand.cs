using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElunsWand : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ �ϵ�";
        data.itemNameEng = "ElunsWand";
        data.itemPrice = 2200;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemStat = "���ݷ� +8\n������ +5%\n��Ÿ�� ���� +20%";
        data.DecreaseCool = 0.20f;
        data.AtkPower = 8;
        data.DmgIncrease = 0.05f;
        data.itemExplanation = "\"������ ���� ��������?\"";
        data.itemNumber = 47;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
