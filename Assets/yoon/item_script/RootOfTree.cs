using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RootOfTree : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������� �Ѹ�";
        data.itemNameEng = "RootOfTree";
        data.itemPrice = 3100;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "��� ������ ���� ���شٴ� ������ ����";
        data.itemStat = "�ִ� ü�� +111\n ���� +33\n �̵��ӵ� -30%";
        data.itemNumber = 16;
        data.MaxHp = 111;
        data.Def = 33;
        data.Speed = -5;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
