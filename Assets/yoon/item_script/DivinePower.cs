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
        data.itemName = "신성 권능";
        data.itemNameEng = "DivinePower";
        data.itemPrice = 4999;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "빛의 힘이여, 나를 이끌어라!";
        data.itemStat = "대쉬 쿨타임이 감소합니다.\n대쉬 사용시 신성 화살을 발사합니다.";
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
