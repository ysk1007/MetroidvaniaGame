using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElfBow : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� �ü��� Ȱ";
        data.itemNameEng = "ElfBow";
        data.itemPrice = 2700;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "ȭ�� ��Ÿ� +75%\nġ��Ÿ Ȯ�� +30%\nġ��Ÿ ���ط� +20%";
        data.itemNumber = 29;
        data.ArrowDis = 0.5625f;
        data.CriticalChance = 0.3f;
        data.CriDmgIncrease = 0.2f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
