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
        data.itemName = "�ҸӴ��� �����";
        data.itemNameEng = "GrandmaNecklace";
        data.itemPrice = 0;
        data.color = Color.magenta;
        data.Rating = "����";
        data.itemStat = "���ݷ� +3\n���� �ӵ� +20%\n�̵� �ӵ� +5%\n����ġ ȹ�� +10%";
        data.AtkPower = 3;
        data.AtkSpeed = 0.2f;
        data.Speed = 0.25f;
        data.EXPGet = 0.1f;
        data.itemExplanation = "\"�� ����̰� �� �����ٰԴ�\"";
    }

    public override void SpecialPower()
    {

    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
