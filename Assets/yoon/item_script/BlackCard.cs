using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackCard : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "검정 트럼프 카드";
        data.itemNameEng = "BlackCard";
        data.itemPrice = 1248;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemExplanation = "\"자 그럼 시작할까? \n이 세계의 운명을 건 카드놀이를 말이야..\"";
        data.itemStat = "최대 체력 +27\n 방어력 +27";
        data.itemNumber = 26;
        data.MaxHp = 27;
        data.Def = 27;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
