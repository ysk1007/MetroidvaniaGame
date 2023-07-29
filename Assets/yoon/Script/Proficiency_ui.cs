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
        if (true) //<- ���� � ���⸦ �����ߴ��� �Ű������� ������ �ε��� ���� �ʿ�
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
                    Pro1SkillName.text = "���� ����";
                    Pro1SkillText.text = "�� ���ݽ� ����Ȯ���� ������ �ο� �մϴ�. �ִ� 8�� ��ø �˴ϴ�";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "�ͺ� ����";
                    Pro2SkillText.text = "ü�� 20% ������ ���� ��� ó�� �մϴ�.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "���� : �����Ŀ�";
                    Pro3SkillText.text = "���� ������� ��� ���ְ�, ��ø�� ���ø�ŭ ����� �������� �ݴϴ�";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
            case 1:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "���� ����";
                    Pro1SkillText.text = "��¡ ������ ���������ϴ�. ��¡�߿��� �޴� ���ذ� �����ϰ�, ��¡�ð��� ����Ͽ� �������� �ݴϴ�.";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "ö�� �μ���";
                    Pro2SkillText.text = "������ ���ݿ� ���� �Ǿ� ������ �߰� ���ظ� �����ϴ�.";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "���� : �Ƹ����׽��� ��ȣ";
                    Pro3SkillText.text = "������ �Ƹ����׽��� ���� �ο� �޽��ϴ�. 5�ʵ��� ��¡ �ӵ��� ���� �����ϰ�, ö�� �μ��⸦ ��� ����մϴ�.";
                    TextAligmentTopLeft(Pro3SkillText);
                }
                break;
            case 2:
                if (proLevel >= 1)
                {
                    Pro1SkillName.text = "��Ÿ����";
                    Pro1SkillText.text = "�⺻������ ����ü�� 1�� �߰��˴ϴ�.";
                    TextAligmentTopLeft(Pro1SkillText);
                }
                if (proLevel >= 2)
                {
                    Pro2SkillName.text = "��ɲ��� ����";
                    Pro2SkillText.text = "�⺻������ ���� ���� ������ ���ư��ϴ�. �������� �Ұ�";
                    TextAligmentTopLeft(Pro2SkillText);
                }
                if (proLevel >= 3)
                {
                    Pro3SkillName.text = "���� : �̱׵����� ����";
                    Pro3SkillText.text = "������ �̱׵����� ������ �ɽ��ϴ�. ���� ������ �ڶ� ���� �ְ���, �Ѹ��� ���� �����մϴ�.";
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
