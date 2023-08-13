using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DivinePower : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�ż� �Ǵ�";
        data.itemNameEng = "DivinePower";
        data.itemPrice = 4999;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemExplanation = "���� ���̿�, ���� �̲����!";
        data.itemStat = "�뽬 ��Ÿ���� �����մϴ�.\n�뽬 ���� �ż� ȭ���� �߻��մϴ�.";
        data.itemNumber = 22;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.DivinePower = false;
            p.SlidingCool += 1.5f;
        }
        if (data.SpecialPower)
        {
            p.DivinePower = true;
            p.SlidingCool -= 1.5f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
