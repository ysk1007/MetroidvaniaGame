using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlassSword : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ��";
        data.itemNameEng = "GlassSword";
        data.itemPrice = 3600;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "��ġ�⸸ �ص� ġ��Ÿ!";
        data.itemStat = "������ +100%\n ���� -50";
        data.itemNumber = 18;
        data.DmgIncrease = 1f;
        data.Def = -50;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
