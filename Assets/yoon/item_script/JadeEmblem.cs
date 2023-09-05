using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JadeEmblem : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "제이드 엠블럼";
        data.itemNameEng = "JadeEmblem";
        data.itemPrice = 2400;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "도무지 읽을 수 없다..\n지니고 있으면 활력이 느껴진다";
        data.itemStat = "방어력 15\n최대체력 +30\n공격력 -7";
        data.itemNumber = 3;
        data.Def = 10;
        data.MaxHp = 30;
        data.AtkPower = -7;
    }

    public override void SpecialPower()
    {
        
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText , RatingText);
    }
}
