using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepressionShield : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ����";
        data.itemNameEng = "RepressionShield";
        data.itemPrice = 2500;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "\"FBI open up!!!\"";
        data.itemStat = "���ظ� �԰� �з����� �ʽ��ϴ�.\n�ִ� ü�� +50\n���� +20";
        data.itemNumber = 35;
        data.MaxHp = 50;
        data.Def = 20;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.NoNockback = false;
        }
        if (data.SpecialPower)
        {
            Player.instance.NoNockback = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
