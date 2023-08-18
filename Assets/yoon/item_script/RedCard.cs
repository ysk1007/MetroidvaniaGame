using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RedCard : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "빨강 트럼프 카드";
        data.itemNameEng = "RedCard";
        data.itemPrice = 3333;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "\"인생이란 원래 자기 생각대로 되지 않는 법이란다\"";
        data.itemStat = "공격 시 원래 피해의 1~333%의 피해를 입힙니다.\n 공격속도 +33%";
        data.itemNumber = 26;
        data.AtkSpeed = 0.33f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UseRedCard = false;
        }
        if (data.SpecialPower)
        {
            p.UseRedCard = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
