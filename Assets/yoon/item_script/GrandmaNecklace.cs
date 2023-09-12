using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaNecklace : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "할머니의 목걸이";
        data.itemNameEng = "GrandmaNecklace";
        data.itemPrice = 0;
        data.color = Color.magenta;
        data.Rating = "영웅";
        data.itemStat = "공격력 +3\n공격 속도 +20%\n이동 속도 +5%\n경험치 획득 +10%";
        data.AtkPower = 3;
        data.AtkSpeed = 0.2f;
        data.Speed = 0.25f;
        data.EXPGet = 0.1f;
        data.itemExplanation = "\"이 목걸이가 널 지켜줄게다\"";
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
