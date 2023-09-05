using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoisonMushroom : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "환각 버섯";
        data.itemNameEng = "PoisonMushroom";
        data.itemPrice = 2350;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemExplanation = "주의 : 섭취시 몬스터가 될 수 있다.";
        data.itemStat = "공격력 +10, 공격속도 +35%\n이동속도 +5%, 점프력 +5%\n최대 체력 -15, 방어력 -5";
        data.itemNumber = 15;
        data.AtkPower = 10;
        data.AtkSpeed = 0.35f;
        data.Speed = 0.25f;
        data.JumpPower = 0.75f;
        data.MaxHp = -15;
        data.Def = -5;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
