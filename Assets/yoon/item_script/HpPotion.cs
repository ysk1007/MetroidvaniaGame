using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpPotion : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "ü�� ����";
        data.itemNameEng = "HpPotion";
        data.itemPrice = 300;
        data.color = Color.white;
        data.Rating = "�Ҹ�ǰ";
        data.itemExplanation = "�÷��̾��� ü���� ȸ���մϴ�";
        data.itemStat = "ȸ���� +50%";
    }
    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
