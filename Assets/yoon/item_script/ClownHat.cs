using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClownHat : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������ ����";
        data.itemNameEng = "ClownHat";
        data.color = Color.magenta;
        data.itemPrice = 1900;
        data.itemExplanation = "\"���� ������ ���� ��� ��ġ�ֱ⸦.\"";
        data.itemStat = "���ݷ� +2 \nġ��Ÿ Ȯ�� +25%";
        data.itemNumber = 5;
        data.AtkPower = 2;
        data.CriticalChance = 25f;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText);
    }
}
