using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�밡�� �尩";
        data.itemNameEng = "WorkGloves";
        data.itemPrice = 400;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemStat = "���ݷ� +2\n���� +3";
        data.itemNumber = 41;
        data.AtkPower = 2;
        data.Def = 3;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
