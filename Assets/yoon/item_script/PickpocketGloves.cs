using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickpocketGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�Ҹ�ġ�� �尩";
        data.itemNameEng = "PickpocketGloves";
        data.itemPrice = 2100;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemStat = "���� ������ �� ���� 1��� ȹ��\n���� �ӵ� +20%\n�̵��ӵ� +20%";
        data.itemExplanation = "���� ������ ������";
        data.itemNumber = 43;
        data.AtkSpeed = 0.2f;
        data.Speed = 0.2f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UsePickGloves = false;
        }
        if (data.SpecialPower)
        {
            p.UsePickGloves = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
