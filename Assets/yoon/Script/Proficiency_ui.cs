using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Proficiency_ui : MonoBehaviour
{
    public static Proficiency_ui instance;
    public Player player;

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

    public float[] ProValue = { 0.005f, 0.0075f, 0.01f }; // 1,2,3 스테이지 값
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        Weaponimages = ProWeaponUi.GetComponentsInChildren<Image>();
        Pro1Skillimages = Pro1_Skill.GetComponentsInChildren<Image>();
        Pro2Skillimages = Pro2_Skill.GetComponentsInChildren<Image>();
        Pro3Skillimages = Pro3_Skill.GetComponentsInChildren<Image>();
        pro_text_setting(proWeaponIndex);
        pro_image_setting(Weaponimages, Pro1Skillimages, Pro2Skillimages, Pro3Skillimages, proWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        ProText.text = (Profill.fillAmount * 100).ToString("F0") + "%";
        if (Profill.fillAmount == 1f)
        {
            player.proLevel = 3;
            proLevel = 3;
        }
        else if (Profill.fillAmount >= 0.66f)
        {
            player.proLevel = 2;
            proLevel = 2;
        }
        else if(Profill.fillAmount >= 0.33f)
        {
            player.proLevel = 1;
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
                if (proWeaponIndex == 4)
                {
                    break;
                }
                Color color1;
                ColorUtility.TryParseHtmlString("#63AB3F", out color1);
                Profill.color = color1;
                Weaponimages[proWeaponIndex].gameObject.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                if (proWeaponIndex == 4)
                {
                    break;
                }
                Color color2;
                ColorUtility.TryParseHtmlString("#4FA4B8", out color2);
                Profill.color = color2;
                Weaponimages[proWeaponIndex].gameObject.GetComponent<Image>().color = new Color(0, 0.5f, 1, 1);
                break;
            case 3:
                if (proWeaponIndex == 4)
                {
                    break;
                }
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
                    Pro1SkillText.text = "적 공격시 출혈을 부여 합니다. 최대 6번 중첩 됩니다";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "핏빛 선고";
                    Pro2SkillText.text = "체력 10% 이하의 적을 즉시 처형 합니다.";
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
                    Pro1SkillName.text = "철벽 부수기";
                    Pro1SkillText.text = "차징 공격이 가능해집니다. 차징시간에 비례하여 데미지를 줍니다.";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "금강불괴";
                    Pro2SkillText.text = "최대 체력과 방어력이 증가합니다.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "오의 : 신의 격노";
                    Pro3SkillText.text = "강력한 번개를 내리쳐 적에게 강력한 피해를 줍니다.";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
            case 2:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "사냥꾼의 감각";
                    Pro1SkillText.text = "기본공격이 적을 향해 추적해 날아갑니다. 지형관통 불가";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "일타이피";
                    Pro2SkillText.text = "기본공격의 투사체가 1개 추가됩니다.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "오의 : 이그드라실의 씨앗";
                    Pro3SkillText.text = "나무가 자라나 적을 휘감고, 뿌리가 몸을 관통합니다.";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
        }

        void TextAligmentTopLeft(TextMeshProUGUI text)
        {
            text.alignment = TextAlignmentOptions.TopLeft;
        }
    }

    public void GetProExp(int stage)
    {
        if (proWeaponIndex == 4 || player.WeaponChage - 1 != proWeaponIndex)
        {
            return;
        }
        Profill.fillAmount += ProValue[stage - 1];
    }

    public void DoStart()
    {
        Weaponimages = ProWeaponUi.GetComponentsInChildren<Image>();
        Pro1Skillimages = Pro1_Skill.GetComponentsInChildren<Image>();
        Pro2Skillimages = Pro2_Skill.GetComponentsInChildren<Image>();
        Pro3Skillimages = Pro3_Skill.GetComponentsInChildren<Image>();
        pro_text_setting(proWeaponIndex);
        pro_image_setting(Weaponimages, Pro1Skillimages, Pro2Skillimages, Pro3Skillimages, proWeaponIndex);
    }
}
