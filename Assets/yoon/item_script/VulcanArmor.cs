using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VulcanArmor : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "불카누스의 갑옷";
        data.itemNameEng = "VulcanArmor";
        data.itemPrice = 3400;
        data.color = Color.red;
        data.Rating = "전설";
        data.itemExplanation = "최선의 방어는 공격이다";
        data.itemStat = "최대 체력 +20\n방어력 +15\n보유한 방어력 10당, 공격력이 5 증가합니다.";
        data.itemNumber = 40;
        data.MaxHp = 20;
        data.Def = 15;
    }

    public override void SpecialPower()
    {
        Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>();
        Player p = Player.instance;
        if (!data.SpecialPower)
        {
            p.UseVulcanArmor = false;
            p.VulcanPower = 0f;
            ui.UiUpdate();
        }
        if (data.SpecialPower)
        {
            p.VulcanArmor();
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
