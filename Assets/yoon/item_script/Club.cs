using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Club : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������";
        data.itemNameEng = "Club";
        data.itemPrice = 300;
        data.color = Color.white;
        data.itemExplanation = "�Ǹ��� ��ȭ����";
        data.itemStat = "���ݷ� +2 \n�ִ�ü�� +5";
        data.itemNumber = 2;
        data.AtkPower = 2;
        data.MaxHp = 5;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText);
    }
}
