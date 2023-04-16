using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JadeEmblem : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "���̵� ����";
        data.itemExplanation = "������ ���� �� ����..\n���ϰ� ������ Ȱ���� ��������";
        data.itemStat = "���� 10 \n�ִ�ü�� +33";
        data.itemNumber = 3;
        data.Def = 10;
        data.MaxHp = 33;
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText);
    }
}
