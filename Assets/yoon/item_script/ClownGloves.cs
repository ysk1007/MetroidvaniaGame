using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ �尩";
        data.itemNameEng = "ClownGloves";
        data.itemPrice = 2200;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "\"�ȶ�\"";
        data.itemStat = "ġ��Ÿ Ȯ�� +10%\n���� �ӵ� +25%";
        data.itemNumber = 6;
        data.AtkSpeed = 0.25f;
        data.CriticalChance = 0.1f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText , TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
