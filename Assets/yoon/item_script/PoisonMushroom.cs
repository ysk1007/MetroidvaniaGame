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
        data.itemPrice = 2100;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "���� : ����� ���Ͱ� �� �� �ִ�.";
        data.itemStat = "���ݷ� +20, ���ݼӵ� +35%\n�̵��ӵ� +50%, ������ +20%\n�ִ� ü�� -50, ���� -30";
        data.itemNumber = 15;
        data.AtkPower = 20;
        data.AtkSpeed = 0.35f;
        data.Speed = 0.5f;
        data.JumpPower = 3f;
        data.MaxHp = -50;
        data.Def = -30;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
