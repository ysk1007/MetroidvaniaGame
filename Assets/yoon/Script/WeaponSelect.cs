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
                WeaponName.text = "단검";
                ExplainText.text = "적을 출혈 상태로 만들어 지속적인 피해를 입힙니다.\n체력이 낮은 적은 처형 됩니다.";
                RecommandStat.text = "추천 스탯 : 치명타 확률, 치명타 피해량";
                break;
            case 1:
                WeaponName.text = "도끼";
                ExplainText.text = "기를 모아 강력한 한방의 데미지를 입힙니다.";
                RecommandStat.text = "추천 스탯 : 공격력, 방어력, 체력";
                break;
            case 2:
                WeaponName.text = "활";
                ExplainText.text = "기본 공격의 투사체가 증가하고, 적을 추적합니다.";
                RecommandStat.text = "추천 스탯 : 공격력, 공격속도";
                break;
        }
    }
}
