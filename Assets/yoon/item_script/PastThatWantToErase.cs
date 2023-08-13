using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PastThatWantToErase : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "지우고 싶은 과거";
        data.itemNameEng = "PastThatWantToErase";
        data.itemPrice = 4999;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "과거로 돌아갈 수 있다면 자신에게 어떤 조언을 하겠습니까.";
        data.itemStat = "1회 부활";
        data.itemNumber = 20;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UsePastErase = false;
            p.PastErase = null;
        }
        if (data.SpecialPower)
        {
            p.UsePastErase = true;
            p.PastErase = gameObject;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
