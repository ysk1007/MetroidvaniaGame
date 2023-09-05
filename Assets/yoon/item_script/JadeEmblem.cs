using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JadeEmblem : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���̵� ����";
        data.itemNameEng = "JadeEmblem";
        data.itemPrice = 2400;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "������ ���� �� ����..\n���ϰ� ������ Ȱ���� ��������";
        data.itemStat = "���� 15\n�ִ�ü�� +30\n���ݷ� -7";
        data.itemNumber = 3;
        data.Def = 10;
        data.MaxHp = 30;
        data.AtkPower = -7;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText , RatingText);
    }
}
