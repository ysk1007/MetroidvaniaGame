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
        data.itemName = "��ī������ ����";
        data.itemNameEng = "VulcanArmor";
        data.itemPrice = 3400;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "�ּ��� ���� �����̴�";
        data.itemStat = "�ִ� ü�� +20\n���� +15\n������ ���� 10��, ���ݷ��� 5 �����մϴ�.";
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
