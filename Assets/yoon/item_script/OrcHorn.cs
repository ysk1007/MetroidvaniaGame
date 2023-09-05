using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrcHorn : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "��ũ�� ���Ǹ�";
        data.itemNameEng = "OrcHorn";
        data.itemPrice = 800;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "���ݷ� +5\n���� �ӵ� +5%\nġ��Ÿ ���ط� +5%";
        data.itemExplanation = "\'�ѿ�-\'";
        data.itemNumber = 49;
        data.AtkPower = 5;
        data.AtkSpeed = 0.05f;
        data.CriDmgIncrease = 0.05f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
