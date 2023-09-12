using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSupplyBook : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�׸��� ��޼�";
        data.itemNameEng = "ShadowSupplyBook";
        data.itemPrice = 3500;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "������ ���� ��@���ݾ�";
        data.itemStat = "���� �ӵ� +150%\n������ -75%";
        data.itemNumber = 51;
        data.AtkSpeed = 1.5f;
        data.DmgIncrease = -0.75f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.GetComponent<afterImageGenerator>().Active = false;
        }
        if (data.SpecialPower)
        {
            p.GetComponent<afterImageGenerator>().Active = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
