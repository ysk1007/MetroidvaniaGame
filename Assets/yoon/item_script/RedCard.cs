using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RedCard : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� Ʈ���� ī��";
        data.itemNameEng = "RedCard";
        data.itemPrice = 3333;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "\"�λ��̶� ���� �ڱ� ������� ���� �ʴ� ���̶���\"";
        data.itemStat = "���� �� ���� ������ 1~333%�� ���ظ� �����ϴ�.\n ���ݼӵ� +33%";
        data.itemNumber = 26;
        data.AtkSpeed = 0.33f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UseRedCard = false;
        }
        if (data.SpecialPower)
        {
            p.UseRedCard = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
