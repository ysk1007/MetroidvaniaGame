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
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "���";
        data.itemStat = "���ݷ� +3\n�ִ� ü�� +10\n���� +2";
        data.itemExplanation = "\'�ѿ�-\'";
        data.itemNumber = 49;
        data.AtkPower = 3;
        data.MaxHp = 10;
        data.Def = 2;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
