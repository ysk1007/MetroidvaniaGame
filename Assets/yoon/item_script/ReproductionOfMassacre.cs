using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReproductionOfMassacre : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "학살의 재현";
        data.itemNameEng = "ReproductionOfMassacre";
        data.itemPrice = 4444;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "\"그의 눈동자에는 무력과 살기가 가득하고,\n인간성을 퇴색시키는 모습이었다.\"";
        data.itemStat = "플레이어가 지속적으로 피해를 입습니다.\n생명력 흡수 +4%\n공격력 +44\n공격 속도 +44%";
        data.itemNumber = 21;
        data.AtkSpeed = 0.44f;
        data.AtkPower = 44;
        data.lifeStill = 0.04f;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Player.instance.LifeRegen += 4;
        }
        if (data.SpecialPower)
        {
            Player.instance.LifeRegen -= 4;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
