using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GolemPower : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�������� ���¿�";
        data.itemNameEng = "GolemPower";
        data.itemPrice = 0;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemStat = "�� ��° ���";
        data.itemExplanation = "��û�� ���°� ������� ���� �ִ�.";
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
