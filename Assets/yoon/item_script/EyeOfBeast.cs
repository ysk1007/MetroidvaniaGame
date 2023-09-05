using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EyeOfBeast : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�ø��� ��";
        data.itemNameEng = "EyeOfBeast";
        data.itemPrice = 3500;
        data.color = Color.red;
        data.Rating = "����";
        data.itemExplanation = "����� �ͼ� �ø��� ���̴�. ���� ����ϴ�.";
        data.itemStat = "ȭ�� ��Ÿ� +50%\n���ݼӵ� +50%\n���ݷ� +20\n�̵��ӵ� -10%\n���� -5";
        data.itemNumber = 24;
        data.AtkPower = 20;
        data.AtkSpeed = 0.5f;
        data.ArrowDis = 0.375f;
        data.Speed = -0.5f;
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
