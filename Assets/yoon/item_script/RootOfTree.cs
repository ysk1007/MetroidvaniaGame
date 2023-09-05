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
        data.itemPrice = 3200;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "��� ������ ���� ���شٴ� ������ ����";
        data.itemStat = "�ִ� ü�� +50\n���� +20\n�̵� �ӵ� -5%";
        data.itemNumber = 16;
        data.MaxHp = 50;
        data.Def = 20;
        data.Speed = -0.25f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
