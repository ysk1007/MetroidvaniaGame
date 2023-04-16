using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Club : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "¸ùµÕÀÌ";
        data.itemExplanation = "ÈÇ¸¢ÇÑ ´ëÈ­¼ö´Ü";
        data.itemStat = "°ø°Ý·Â +2 \nÃÖ´ëÃ¼·Â +5";
        data.itemNumber = 2;
        data.AtkPower = 2;
        data.MaxHp = 5;
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText);
    }
}
