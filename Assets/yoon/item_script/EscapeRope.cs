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
        data.itemName = "탈출 로프";
        data.itemNameEng = "EscapeRope";
        data.itemPrice = 1190;
        Color color = new Color32(93, 141, 255, 255);
        data.color = color;
        data.Rating = "희귀";
        data.itemExplanation = "동굴에서 탈출할 때 요긴하게 쓰인다";
        data.itemStat = "대쉬 쿨타임 감소 25%\n이동속도 +50%\n점프력 +20%";
        data.itemNumber = 31;
        data.Speed = 2.0f;
        data.JumpPower = 3f;
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
