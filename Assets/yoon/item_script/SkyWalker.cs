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
        data.itemName = "��ī�� ��Ŀ";
        data.itemExplanation = "�������� ��ü�� ����������";
        data.itemStat = "�̵��ӵ� +50% \n���ݼӵ� +50%";
        data.itemNumber = 1;
        data.Speed = 0.5f;
        data.AtkSpeed = 0.5f;
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText);
    }
}
