using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkyWalker : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "��ī�� ��Ŀ";
        data.itemNameEng = "SkyWalker";
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "�������� ��ü�� ����������";
        data.itemStat = "�̵��ӵ� +50% \n���ݼӵ� +50%";
        data.itemNumber = 1;
        data.Speed = 0.5f;
        data.AtkSpeed = 0.5f;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.JumpCount--;
            Player.instance.JumpCnt--;
        }
        if(data.SpecialPower)
        {
            Player.instance.JumpCount++;
            Player.instance.JumpCnt++;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
