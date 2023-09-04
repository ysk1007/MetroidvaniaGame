using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BundleOfGifts : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "¼±¹° º¸µû¸®";
        data.itemNameEng = "BundleOfGifts";
        data.itemPrice = 1225;
        data.color = Color.green;
        data.Rating = "°í±Þ";
        data.itemExplanation = "\"¼±¹°À» ·¹ÀÌÀúÃ³·³ ½ð´Ù°í ºÁ¾ßµÅ¿ä.\"";
        data.itemStat = "°ñµå È¹µæ·® +20%\n°æÇèÄ¡ È¹µæ·® +20%";
        data.itemNumber = 31;
        data.GoldGet = 0.20f;
        data.EXPGet = 0.20f;
    }

    public override void SpecialPower()
    {
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.SlidingCool += 0.5f;
        }
        if (data.SpecialPower)
        {
            p.SlidingCool -= 0.5f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
