using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public struct Data //������
{
    public Image itemimg; //�̹���
    public AudioClip equipSfx; //���� ȿ����
    public string itemName; //�̸�
    public string itemExplanation; //����
    public string itemStat; //����
    public int itemNumber; //������ ���� ��ȣ

    public int AtkPower; //���ݷ�
    public float AtkSpeed; //����
    public int Def; //����
    public int MaxHp; //�ִ�ü��
    public float Speed; //�̵��ӵ�
    public float CriticalChance; //ġ��Ÿ Ȯ��
}

public abstract class itemStatus : MonoBehaviour
{
    public Data data;

    public abstract void InitSetting(); //abstract -> ��ӹ��� ��ũ��Ʈ���� ������ ����

    public virtual void Using(Image img,TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)  //virtual �� �������� ��� ���� ��
    {
        TextImageSettings(img, NameText, ExplanationText, StatText);
    }

    public virtual void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText) //�ؽ�Ʈ �� �̹��� ����
    {
        img.sprite = this.data.itemimg.sprite; //�ڽ��� �̹��� ��������Ʈ�� ������
        img.SetNativeSize(); //SetNativeSize ���� png ������� ��ȯ (�̹������� ������ �޶� �� �� �ʱ�ȭ ��)

        RectTransform rect = (RectTransform)img.transform; //�ʱ�ȭ �� ������ ������
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * 5, rect.sizeDelta.y * 5); // 5 ���� (������ ũ��)
        img.rectTransform.sizeDelta = rect.sizeDelta; // ������ ����

        img.color = new Color32(255, 255, 255, 255); // ���� 100%

        NameText.text = this.data.itemNumber.ToString()+". " + this.data.itemName;  //������ �̸� �ؽ�Ʈ �κ� ������ȣ �ٿ��� ����
        ExplanationText.text = this.data.itemExplanation; //���� �ؽ�Ʈ �κ� ����
        StatText.text = this.data.itemStat; //���� �ؽ�Ʈ �κ� ����
    }
}
