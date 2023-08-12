using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    public Image[] WeaponIcons;
    public int index = 0;
    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI ExplainText;
    public TextMeshProUGUI RecommandStat;
    public Button BTN_Left;
    public Button BTN_Right;
    public Color32 offColor;
    public Color32 onColor;
    public bool WeaponSelectUiOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        WeaponIcons[index].enabled = true;
        TextSetting(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 0)
        {
            BTN_Left.image.color = offColor;
        }
        else
        {
            BTN_Left.image.color = onColor;
        }
        if (index == 2)
        {
            BTN_Right.image.color = offColor;
        }
        else
        {
            BTN_Right.image.color = onColor;
        }

        if (Input.GetKeyUp(KeyCode.E) && WeaponSelectUiOpen)
        {
            Player.instance.proSelectWeapon = index;
            Proficiency_ui.instance.proWeaponIndex = index;
            Proficiency_ui.instance.DoStart();
            WeaponSelectUiOpen = false;
            gameObject.SetActive(false);
            DataManager.instance.JsonSave("ProData");
            DataManager.instance.JsonLoad("ProData");
        }
    }

    public void ChangeWeapon(int i)
    {
        if (index + i < 0 || index + i > WeaponIcons.Length - 1)
        {
            return;
        }
        index += i;
        for (int j = 0; j < WeaponIcons.Length; j++)
        {
            if (j == index)
            {
                WeaponIcons[j].enabled = true;
            }
            else
            {
                WeaponIcons[j].enabled = false;
            }
        }
        TextSetting(index);
    }

    public void TextSetting(int index)
    {
        switch (index)
        {
            case 0:
                WeaponName.text = "�ܰ�";
                ExplainText.text = "���� ���� ���·� ����� �������� ���ظ� �����ϴ�.\nü���� ���� ���� ó�� �˴ϴ�.";
                RecommandStat.text = "��õ ���� : ġ��Ÿ Ȯ��, ġ��Ÿ ���ط�";
                break;
            case 1:
                WeaponName.text = "����";
                ExplainText.text = "�⸦ ��� ������ �ѹ��� �������� �����ϴ�.";
                RecommandStat.text = "��õ ���� : ���ݷ�, ����, ü��";
                break;
            case 2:
                WeaponName.text = "Ȱ";
                ExplainText.text = "�⺻ ������ ����ü�� �����ϰ�, ���� �����մϴ�.";
                RecommandStat.text = "��õ ���� : ���ݷ�, ���ݼӵ�";
                break;
        }
    }
}
