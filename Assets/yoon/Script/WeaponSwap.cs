using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSwap : MonoBehaviour
{
    public static WeaponSwap instance;
    public Player player;
    public GameObject WeaponUi;
    public Image[] images;
    public TextMeshProUGUI SwapCoolRemain;

    public GameObject SkillUi;
    public GameObject[] skills;

    public GameObject[] SkillCools;
    public TextMeshProUGUI sword_skill_cool_text;
    public TextMeshProUGUI axe_skill_cool_text;
    public TextMeshProUGUI bow_skill_cool_text;

    public GameObject UltUi;
    public Image[] UltSkills;
    public TextMeshProUGUI UltCoolRemain;

    public int currentWeaponIndex = 0;
    public float swapCool = 2f;
    public float skillcool;
    public float ultcool = 180f;

    public float SkillremainTime = 0f;
    public float SwapremainTime = 0f;
    public float UltremainTime = 0f;

    public bool DoSwap = false;
    public bool swaping = false;
    public bool skilling = false;
    public bool[] Weapon_skilling = { false, false, false };
    public bool ultting = false;

    public Image img_Swap_coolTime;
    public Image Sword_Skill_coolTime;
    public Image Axe_Skill_coolTime;
    public Image Bow_Skill_coolTime;
    public Image img_Ult_coolTime;
    public Image Tab_key;
    public GameObject LockImage;

    public GameObject Ult_onAnim;
    public Animator[] Cool_Anims;
    public Animator Ult_cool_Anim;

    float time = 0;
    float _size = 1.5f;
    float _upSizeTime = 0.1f;
    private void Awake()
    {
        instance = this;
        images = WeaponUi.GetComponentsInChildren<Image>();
        images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        skills[currentWeaponIndex].SetActive(true);
        UltSkills = UltUi.GetComponentsInChildren<Image>();
        UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        if (currentWeaponIndex != player.proSelectWeapon || player.proLevel != 3)
        {
            LockImage.SetActive(true);
            Ult_onAnim.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeaponIndex == player.proSelectWeapon && player.proLevel == 3)
        {
            LockImage.SetActive(false);
            Ult_onAnim.SetActive(true);
        }
        if (DoSwap && !swaping && !player.isCharging && !player.isSkill && !player.isMasterSkill)
        {
            DoSwap = false;
            currentWeaponIndex++;

            // 인덱스 값이 이미지 개수를 초과하면 처음 이미지로 돌아갑니다.
            if (currentWeaponIndex >= images.Length)
            {
                currentWeaponIndex = 0;
            }

            // 모든 이미지를 비활성화하고, 선택한 이미지만 활성화합니다.
            for (int i = 0; i < images.Length; i++)
            {
                if (i == currentWeaponIndex)
                {
                    images[i].gameObject.GetComponent<Image>().enabled = true;
                    skills[i].SetActive(true);
                    UltSkills[i].gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    images[i].gameObject.GetComponent<Image>().enabled = false;
                    skills[i].SetActive(false);
                    UltSkills[i].gameObject.GetComponent<Image>().enabled = false;
                }
            }
            for (int i = 0; i < SkillCools.Length; i++)
            {
                if (i == currentWeaponIndex)
                {
                    SkillCools[i].SetActive(true);
                }
                else
                {
                    SkillCools[i].SetActive(false);
                }
            }


            if (currentWeaponIndex == player.proSelectWeapon && player.proLevel == 3)
            {
                LockImage.SetActive(false);
                Ult_onAnim.SetActive(true);
            }
            else
            {
                LockImage.SetActive(true);
                Ult_onAnim.SetActive(false);
            }

            StartCoroutine(FillSliderOverTime(img_Swap_coolTime, swapCool, "swap"));
        }
    }

    public void Skill()
    {
        Image i = SkillCools[currentWeaponIndex].GetComponent<Image>();
        float cooltime = player.DeCoolTimeCarcul(player.SkillTime[currentWeaponIndex]);
        StartCoroutine(FillSliderOverTime(i, cooltime, "skill"));
    }

    public void Ult()
    {
        float cooltime = player.DeCoolTimeCarcul(player.MasterSkillTime[currentWeaponIndex]);
        StartCoroutine(FillSliderOverTime(img_Ult_coolTime, cooltime, "ult"));
        Ult_onAnim.SetActive(false);
    }

    IEnumerator Ready(Image img, string type, int cur_index)
    {
        Color color = img.color;
        img.color = new Color32(255, 255, 255, 200);
        yield return null;
        img.color = color;
        img.fillAmount = 1f;

        img.enabled = false;
        if (type == "swap")
        {
            swaping = false;
        }
        else if (type == "skill")
        {
            Weapon_skilling[cur_index] = false;
            StartCoroutine(ReadyAnim(skills[cur_index].gameObject.GetComponent<Image>()));
            Cool_Anims[cur_index].SetTrigger("cooltime");
        }
        else if (type == "ult")
        {
            ultting = false;
            StartCoroutine(ReadyAnim(UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>()));
            Ult_cool_Anim.SetTrigger("cooltime");
            Ult_onAnim.SetActive(true);
        }
    }

    IEnumerator ReadyAnim(Image img)
    {
        while (time < 1f)
        {
            if (time <= _upSizeTime)
            {
                img.rectTransform.localScale = Vector3.one * (1 + _size * time);
            }
            else if (time <= _upSizeTime * 2)
            {
                img.rectTransform.localScale = Vector3.one * (2 * _size * _upSizeTime + 1 - time * _size);
            }
            else
            {
                img.rectTransform.localScale = Vector3.one;
                break;
            }
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
    }

    IEnumerator FillSliderOverTime(Image img, float coolTime, string type)
    {
        int cur_index = currentWeaponIndex;
        float remainTime;
        img.enabled = true;
        if (type == "swap")
        {
            swaping = true;
            SwapCoolRemain.enabled = true;
            SwapremainTime = coolTime;
            remainTime = SwapremainTime;
            while (remainTime >= 0f)
            {
                remainTime -= Time.deltaTime;
                img.fillAmount = remainTime / coolTime;
                if (remainTime > 1.0f)
                {
                    SwapCoolRemain.text = remainTime.ToString("F0");
                }
                else
                {
                    SwapCoolRemain.text = remainTime.ToString("F1");
                }

                yield return null;
            }
            SwapCoolRemain.enabled = false;
        }
        else if (type == "skill")
        {
            Weapon_skilling[cur_index] = true;
            TextMeshProUGUI SkillCoolRemain;
            SkillCoolRemain = SkillCools[cur_index].GetComponentInChildren<TextMeshProUGUI>();
            SkillCools[cur_index].SetActive(true);
            SkillCoolRemain.enabled = true;
            SkillremainTime = coolTime;
            remainTime = SkillremainTime;
            while (remainTime >= 0f)
            {
                remainTime -= Time.deltaTime;
                img.fillAmount = remainTime / coolTime;
                if (remainTime > 1.0f)
                {
                    SkillCoolRemain.text = remainTime.ToString("F0");
                }
                else
                {
                    SkillCoolRemain.text = remainTime.ToString("F1");
                }

                yield return null;
            }
            SkillCoolRemain.enabled = false;
            SkillCools[cur_index].SetActive(false);
        }
        else if (type == "ult")
        {
            ultting = true;
            UltCoolRemain.enabled = true;
            UltremainTime = coolTime;
            remainTime = UltremainTime;
            while (remainTime >= 0f)
            {
                remainTime -= Time.deltaTime;
                img.fillAmount = remainTime / coolTime;
                if (remainTime > 1.0f)
                {
                    UltCoolRemain.text = remainTime.ToString("F0");
                }
                else
                {
                    UltCoolRemain.text = remainTime.ToString("F1");
                }

                yield return null;
            }
            UltCoolRemain.enabled = false;
        }
        img.enabled = false;
        img.fillAmount = 0f;
        StartCoroutine(Ready(img, type, cur_index));

    }
}
