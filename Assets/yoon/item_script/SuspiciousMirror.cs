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
        data.itemName = "수상한 거울";
        data.itemNameEng = "SuspiciousMirror";
        data.itemPrice = 2100;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "빵! 바를까? 놀랐지?";
        data.itemStat = "시야가 좁아집니다\n공격 속도 +25%\n치명타 확률 +25%";
        data.itemNumber = 39;
        data.AtkSpeed = 0.25f;
        data.CriticalChance = 0.25f;
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
