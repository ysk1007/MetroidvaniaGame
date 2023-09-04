using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BundleOfGifts : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���� ������";
        data.itemNameEng = "BundleOfGifts";
        data.itemPrice = 1225;
        data.color = Color.green;
        data.Rating = "���";
        data.itemExplanation = "\"������ ������ó�� ��ٰ� ���ߵſ�.\"";
        data.itemStat = "��� ȹ�淮 +20%\n����ġ ȹ�淮 +20%";
        data.itemNumber = 31;
        data.GoldGet = 0.20f;
        data.EXPGet = 0.20f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.SlidingCool += 0.5f;
        }
        if (data.SpecialPower)
        {
            p.SlidingCool -= 0.5f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
