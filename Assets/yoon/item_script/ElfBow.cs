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
        data.itemPrice = 2500;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "";
        data.itemStat = "ȭ�� ��Ÿ� +50%\nġ��Ÿ Ȯ�� +10%\nġ��Ÿ ���ط� +20%";
        data.itemNumber = 30;
        data.ArrowDis = 0.375f;
        data.CriticalChance = 0.1f;
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
