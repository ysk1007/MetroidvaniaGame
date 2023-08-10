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
        data.itemName = "스카이 워커";
        data.itemNameEng = "SkyWalker";
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemExplanation = "착용자의 신체가 가벼워진다";
        data.itemStat = "이동속도 +50% \n공격속도 +50%";
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
