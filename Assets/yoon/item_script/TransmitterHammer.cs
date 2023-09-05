using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransmitterHammer : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "전송병의 망치";
        data.itemNameEng = "TransmitterHammer";
        data.itemPrice = 526;
        data.color = Color.white;
        data.Rating = "일반";
        data.itemExplanation = "\"말뚝 언제 다 박냐\"";
        data.itemStat = "공격력 +3\n도끼 차징속도 +15% ";
        data.itemNumber = 38;
        data.AtkPower = 3;
        data.ChargingTime = 0.3f;
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
