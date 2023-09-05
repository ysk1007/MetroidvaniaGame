using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReproductionOfMassacre : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�л��� ����";
        data.itemNameEng = "ReproductionOfMassacre";
        data.itemPrice = 4444;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemExplanation = "\"���� �����ڿ��� ���°� ��Ⱑ �����ϰ�,\n�ΰ����� �����Ű�� ����̾���.\"";
        data.itemStat = "�÷��̾ ���������� ���ظ� �Խ��ϴ�.\n����� ��� +4%\n���ݷ� +44\n���� �ӵ� +44%";
        data.itemNumber = 21;
        data.AtkSpeed = 0.44f;
        data.AtkPower = 44;
        data.lifeStill = 0.04f;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen += 4;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen -= 4;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
