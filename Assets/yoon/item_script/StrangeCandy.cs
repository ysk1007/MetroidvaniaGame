using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrangeCandy : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�̻��� ����";
        data.itemNameEng = "StrangeCandy";
        data.itemPrice = 3000;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "... ����!?";
        data.itemStat = "����ġ ȹ�淮 +100%";
        data.itemNumber = 12;
        data.EXPGet = 1.0f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
