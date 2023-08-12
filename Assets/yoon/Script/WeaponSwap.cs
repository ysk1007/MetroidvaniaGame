using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSwap : MonoBehaviour
{
    public GameObject WeaponUi;
    public Image[] images;
    public TextMeshProUGUI SwapCoolRemain;

    public GameObject SkillUi;
    public Image[] skills;
    public TextMeshProUGUI SkillCoolRemain;

    public GameObject UltUi;
    public Image[] UltSkills;
    public TextMeshProUGUI UltCoolRemain;

    public int currentWeaponIndex = 0;
    public float swapCool = 2f;
    public float skillcool = 6.3f;
    public float ultcool = 180f;

    public float SkillremainTime = 0f;
    public float SwapremainTime = 0f;
    public float UltremainTime = 0f;

    public bool swaping = false;
    public bool skilling = false;
    public bool ultting = false;

    public bool ableExe = false;
    public bool ableBow = false;

    public Image img_Swap_coolTime;
    public Image img_Skill_coolTime;
    public Image img_Ult_coolTime;
    public Image Tab_key;

    public Player player;

    float time = 0;
    float _size = 1.5f;
    float _upSizeTime = 0.1f;
    private void Awake()
    {
        if (!ableExe && !ableBow) Tab_key.enabled = false;
        images = WeaponUi.GetComponentsInChildren<Image>();
        images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        skills = SkillUi.GetComponentsInChildren<Image>();
        skills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        UltSkills = UltUi.GetComponentsInChildren<Image>();
        UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ableExe || ableBow) 
            Tab_key.enabled = true;
        if (!ableExe && !ableBow)
        {
            Tab_key.enabled = false;
            images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = false;
            skills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = false;
            UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = false;
            currentWeaponIndex = 0;
            images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
            skills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
            UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !swaping && ableExe && player.isSkill == false && player.isMasterSkill == false && player.isCharging == false)   // 8/6 플레이어의 스킬사용 유무확인 추가
        {
            currentWeaponIndex++;

            if (currentWeaponIndex == 2 && !ableBow)
            {
                currentWeaponIndex = 0;
            }
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
                    skills[i].gameObject.GetComponent<Image>().enabled = true;
                    UltSkills[i].gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    images[i].gameObject.GetComponent<Image>().enabled = false;
                    skills[i].gameObject.GetComponent<Image>().enabled = false;
                    UltSkills[i].gameObject.GetComponent<Image>().enabled = false;
                }
            }
            StartCoroutine(FillSliderOverTime(img_Swap_coolTime, swapCool, "swap"));
        }
        if (Input.GetKeyDown(KeyCode.S) && !skilling)
        {
            StartCoroutine(FillSliderOverTime(img_Skill_coolTime, skillcool, "skill"));
        }
        if (Input.GetKeyDown(KeyCode.U) && !ultting)
        {
            StartCoroutine(FillSliderOverTime(img_Ult_coolTime, ultcool, "ult"));
        }
    }

    IEnumerator Ready(Image img, string type)
    {
        Color color = img.color;
        img.color = new Color32 (255,255,255,200);
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
            skilling = false;
            StartCoroutine(ReadyAnim(skills[currentWeaponIndex].gameObject.GetComponent<Image>()));
        }
        else if (type == "ult")
        {
            ultting = false;
            StartCoroutine(ReadyAnim(UltSkills[currentWeaponIndex].gameObject.GetComponent<Image>()));
        }
    }

    IEnumerator ReadyAnim(Image img)
    {
        while (time < 1f)
        {
        //Debug.Log("시작");
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
            skilling = true;
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
        StartCoroutine(Ready(img, type));
        
    }
}
