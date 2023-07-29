using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Proficiency_ui : MonoBehaviour
{
    public static Proficiency_ui instance;

    public GameObject ProWeaponUi;
    public Image[] Weaponimages;
    public int proWeaponIndex = 0;
    public int proLevel = 0;

    public Image Profill;
    public TextMeshProUGUI ProText;

    public GameObject Pro1_Skill;
    public Image[] Pro1Skillimages;
    public TextMeshProUGUI Pro1SkillText;
    public TextMeshProUGUI Pro1SkillName;
    public Image Pro1UnlockImage;

    public GameObject Pro2_Skill;
    public Image[] Pro2Skillimages;
    public TextMeshProUGUI Pro2SkillText;
    public TextMeshProUGUI Pro2SkillName;
    public Image Pro2UnlockImage;

    public GameObject Pro3_Skill;
    public Image[] Pro3Skillimages;
    public TextMeshProUGUI Pro3SkillText;
    public TextMeshProUGUI Pro3SkillName;
    public Image Pro3UnlockImage;

    private void Awake()
    {
        instance = this;
        Weaponimages = ProWeaponUi.GetComponentsInChildren<Image>();
        Pro1Skillimages = Pro1_Skill.GetComponentsInChildren<Image>();
        Pro2Skillimages = Pro2_Skill.GetComponentsInChildren<Image>();
        Pro3Skillimages = Pro3_Skill.GetComponentsInChildren<Image>();
        pro_text_setting(proWeaponIndex);
        if (true) //<- 차후 어떤 무기를 선택했는지 매개변수에 적절한 인덱스 전달 필요
        {
            pro_image_setting(Weaponimages, Pro1Skillimages, Pro2Skillimages, Pro3Skillimages, proWeaponIndex);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProText.text = (Profill.fillAmount * 100).ToString("F0") + "%";
        if (Profill.fillAmount == 1f)
        {
            proLevel = 3;
        }
        else if (Profill.fillAmount >= 0.66f)
        {
            proLevel = 2;
        }
        else if(Profill.fillAmount >= 0.33f)
        {
            proLevel = 1;
        }
        pro_fill_setting(proLevel);
        pro_text_setting(proWeaponIndex);
        unlock_Setting();
    }

    public void pro_image_setting(Image[] weapon, Image[] pro1, Image[] pro2, Image[] pro3, int index)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == index)
            {
                weapon[i].gameObject.GetComponent<Image>().enabled = true;
                pro1[i].gameObject.GetComponent<Image>().enabled = true;
                pro2[i].gameObject.GetComponent<Image>().enabled = true;
                pro3[i].gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                weapon[i].gameObject.GetComponent<Image>().enabled = false;
                pro1[i].gameObject.GetComponent<Image>().enabled = false;
                pro2[i].gameObject.GetComponent<Image>().enabled = false;
                pro3[i].gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

    public void unlock_Setting()
    {
        if (proLevel >= 1)
        {
            Pro1UnlockImage.gameObject.SetActive(false);
        }
        if (proLevel >= 2)
        {
            Pro2UnlockImage.gameObject.SetActive(false);
        }
        if (proLevel >= 3)
        {
            Pro3UnlockImage.gameObject.SetActive(false);
        }
    }

    public void pro_fill_setting(int level)
    {
        switch (level)
        {
            case 1:
                Color color1;
                ColorUtility.TryParseHtmlString("#63AB3F", out color1);
                Profill.color = color1;
                Weaponimages[proWeaponIndex].gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                Color color2;
                ColorUtility.TryParseHtmlString("#4FA4B8", out color2);
                Profill.color = color2;
                Weaponimages[proWeaponIndex].gameObject.GetComponent<Image>().color = new Color(0, 0.5f, 1, 1);
                break;
            case 3:
                Color color3;
                ColorUtility.TryParseHtmlString("#E64539", out color3);
                Profill.color = color3;
                Weaponimages[proWeaponIndex].gameObject.GetComponent<Image>().color = Color.red;
                break;
        }
    }

    public void pro_text_setting(int index)
    {
        switch (index)
        {
            case 0:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "붉은 낙인";
                    Pro1SkillText.text = "적 공격시 일정확률로 출혈을 부여 합니다. 최대 8번 중첩 됩니다";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "핏빛 선고";
                    Pro2SkillText.text = "체력 20% 이하의 적을 즉시 처형 합니다.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "오의 : 심장파열";
                    Pro3SkillText.text = "출혈 디버프를 모두 없애고, 중첩된 스택만큼 비례해 데미지를 줍니다";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
            case 1:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "굳은 결의";
                    Pro1SkillText.text = "차징 공격이 가능해집니다. 차징중에는 받는 피해가 감소하고, 차징시간에 비례하여 데미지를 줍니다.";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "철벽 부수기";
                    Pro2SkillText.text = "마지막 공격에 힘을 실어 강력한 추가 피해를 입힙니다.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "오의 : 아마조네스의 가호";
                    Pro3SkillText.text = "여전사 아마조네스의 힘을 부여 받습니다. 5초동안 차징 속도가 대폭 증가하고, 철벽 부수기를 계속 사용합니다.";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
            case 2:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "일타이피";
                    Pro1SkillText.text = "기본공격의 투사체가 1개 추가됩니다.";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "사냥꾼의 감각";
                    Pro2SkillText.text = "기본공격이 적을 향해 추적해 날아갑니다. 지형관통 불가";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "오의 : 이그드라실의 씨앗";
                    Pro3SkillText.text = "적에게 이그드라실의 씨앗을 심습니다. 몸에 나무가 자라 적을 휘감고, 뿌리가 몸을 관통합니다.";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
        }

        void TextAligmentTopLeft(TextMeshProUGUI text)
        {
            text.alignment = TextAlignmentOptions.TopLeft;
        }
    }
}
