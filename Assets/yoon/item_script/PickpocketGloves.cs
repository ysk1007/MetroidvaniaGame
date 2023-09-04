using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickpocketGloves : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "소매치기 장갑";
        data.itemNameEng = "PickpocketGloves";
        data.itemPrice = 2100;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemStat = "적을 공격할 때 마다 1골드 획득\n공격 속도 +20%\n이동속도 +20%";
        data.itemExplanation = "손은 눈보다 빠르다";
        data.itemNumber = 43;
        data.AtkSpeed = 0.2f;
        data.Speed = 0.2f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UsePickGloves = false;
        }
        if (data.SpecialPower)
        {
            p.UsePickGloves = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
