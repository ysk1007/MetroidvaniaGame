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
        data.itemPrice = 2150;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "\"FBI open up!!!\"";
        data.itemStat = "���ظ� �԰� �з����� �ʽ��ϴ�.\n�ִ� ü�� +10\n���� +15";
        data.itemNumber = 36;
        data.MaxHp = 15;
        data.Def = 15;
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
