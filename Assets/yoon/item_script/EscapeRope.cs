using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EscapeRope : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "Ż�� ����";
        data.itemNameEng = "EscapeRope";
        data.itemPrice = 1190;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "���";
        data.itemExplanation = "�������� Ż���� �� ����ϰ� ���δ�";
        data.itemStat = "�뽬 ��Ÿ�� ���� 20%\n�̵��ӵ� +10%\n������ +10%";
        data.itemNumber = 31;
        data.Speed = 0.5f;
        data.JumpPower = 1.5f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.SlidingCool += 0.4f;
        }
        if (data.SpecialPower)
        {
            p.SlidingCool -= 0.4f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
