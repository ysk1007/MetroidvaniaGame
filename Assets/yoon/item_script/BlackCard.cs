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
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "고급";
        data.itemExplanation = "\"자 그럼 시작할까? \n이 세계의 운명을 건 카드놀이를 말이야..\"";
        data.itemStat = "치명타 확률 +5%\n쿨타임 감소 +10%\n이동 속도 +5%";
        data.itemNumber = 26;
        data.CriticalChance = 0.05f;
        data.DecreaseCool = 0.1f;
        data.Speed = 0.25f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
