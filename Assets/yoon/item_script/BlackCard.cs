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
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "\"�� �׷� �����ұ�? \n�� ������ ����� �� ī����̸� ���̾�..\"";
        data.itemStat = "ġ��Ÿ Ȯ�� +5%\n��Ÿ�� ���� +10%\n�̵� �ӵ� +5%";
        data.itemNumber = 26;
        data.CriticalChance = 0.05f;
        data.DecreaseCool = 0.1f;
        data.Speed = 0.25f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
