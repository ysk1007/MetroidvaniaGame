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
        data.itemPrice = 2250;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "빵! 바를까? 놀랐지?";
        data.itemStat = "시야가 좁아집니다.\n데미지 +30%";
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
