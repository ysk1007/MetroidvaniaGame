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
        data.itemName = "������";
        data.itemExplanation = "�Ǹ��� ��ȭ����";
        data.itemStat = "���ݷ� +2 \n�ִ�ü�� +5";
        data.itemNumber = 2;
        data.AtkPower = 2;
        data.MaxHp = 5;
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText);
    }
}
