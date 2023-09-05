using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuspiciousMirror : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "������ �ſ�";
        data.itemNameEng = "SuspiciousMirror";
        data.itemPrice = 2250;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemExplanation = "��! �ٸ���? �����?";
        data.itemStat = "�þ߰� �������ϴ�.\n������ +30%";
        data.itemNumber = 39;
        data.DmgIncrease = 0.3f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UseMirror = false;
        }
        if (data.SpecialPower)
        {
            p.UseMirror = true;
        }
        p.Mirror();
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
