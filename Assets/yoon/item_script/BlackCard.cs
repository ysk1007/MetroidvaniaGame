using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackCard : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� Ʈ���� ī��";
        data.itemNameEng = "BlackCard";
        data.itemPrice = 1248;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "\"�� �׷� �����ұ�? \n�� ������ ����� �� ī����̸� ���̾�..\"";
        data.itemStat = "�ִ� ü�� +27\n ���� +27";
        data.itemNumber = 26;
        data.MaxHp = 27;
        data.Def = 27;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
