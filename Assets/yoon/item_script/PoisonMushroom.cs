using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoisonMushroom : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "ȯ�� ����";
        data.itemNameEng = "PoisonMushroom";
        data.itemPrice = 2350;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "���� : ����� ���Ͱ� �� �� �ִ�.";
        data.itemStat = "���ݷ� +10, ���ݼӵ� +35%\n�̵��ӵ� +5%, ������ +5%\n�ִ� ü�� -15, ���� -5";
        data.itemNumber = 15;
        data.AtkPower = 10;
        data.AtkSpeed = 0.35f;
        data.Speed = 0.25f;
        data.JumpPower = 0.75f;
        data.MaxHp = -15;
        data.Def = -5;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
