using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolRich : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ��¡";
        data.itemNameEng = "SymbolRich";
        data.itemPrice = 1400;
        Color color = new Color32(93,141,255,255);
        data.color = color;
        data.Rating = "���";
        data.itemExplanation = "���ڰ� �Ǵ� ������";
        data.itemStat = "��� ȹ�� ������ +50%";
        data.itemNumber = 9;
        data.GoldGet = 0.5f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
