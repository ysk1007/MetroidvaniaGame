using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownBoots : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ Ʃ��";
        data.itemNameEng = "ClownBoots";
        data.itemPrice = 1700;
        data.color = Color.magenta;
        data.itemExplanation = "";
        data.itemStat = "���ݷ� +3 \nġ��Ÿ Ȯ�� +20%";
        data.itemNumber = 8;
        data.AtkPower = 3;
        data.CriticalChance = 0.2f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
