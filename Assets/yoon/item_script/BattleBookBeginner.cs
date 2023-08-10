using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleBookBeginner : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ ���� [�⺻��]";
        data.itemNameEng = "BattleBook";
        data.itemPrice = 600;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemStat = "���ݷ� +5 \n���ݼӵ� +12%\n�̵��ӵ� +13%";
        data.itemNumber = 11;
        data.AtkPower = 5;
        data.AtkSpeed = 0.12f;
        data.Speed = 0.13f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
