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
        data.itemPrice = 500;
        data.color = Color.white;
        data.Rating = "�Ϲ�";
        data.itemStat = "���ݷ� +2\n���� �ӵ� +2%\n�ִ� ü�� +2\n���� +2";
        data.itemNumber = 11;
        data.AtkPower = 2;
        data.AtkSpeed = 0.02f;
        data.Def = 2;
        data.MaxHp = 2;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
