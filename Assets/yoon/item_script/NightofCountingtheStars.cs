using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightofCountingtheStars : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "별 헤는 밤";
        data.itemNameEng = "NightofCountingtheStars";
        data.itemPrice = 4040;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "\"겨울이 지나고 나의 별에도 봄이 왔다\"";
        data.itemStat = "모든 능력치 +17";
        data.itemNumber = 17;
        data.DmgIncrease = 0.17f;
        data.CriticalChance = 0.17f;
        data.CriDmgIncrease = 0.17f;
        data.AtkPower = 17;
        data.Def = 17;
        data.AtkSpeed = 0.17f;
        data.Speed = 0.17f;
        data.MaxHp = 17;
        data.lifeStill = 0.017f;
        data.GoldGet = 0.17f;
        data.EXPGet = 0.17f;
        data.DecreaseCool = 0.17f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
