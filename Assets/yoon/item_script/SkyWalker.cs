using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkyWalker : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "스카이 워커";
        data.itemExplanation = "2단 점프가 가능해진다";
        data.itemNumber = 1;
        data.Speed = 0.5f;
        data.AtkSpeed = 0.5f;
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText)
    {
        base.TextImageSettings(img, NameText, ExplanationText);
    }
}
