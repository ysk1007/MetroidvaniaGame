using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public GameObject WeaponUi;
    public Image[] images;

    public GameObject SkillUi;
    public Image[] skills;

    public int currentWeaponIndex = 0;
    public float swapCool = 2f;
    public float skillcool = 6.3f;

    public bool swaping = false;
    public bool skilling = false;

    public bool ableExe = false;
    public bool ableBow = false;

    public Image img_Swap_coolTime;
    public Image img_Skill_coolTime;
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
            currentWeaponIndex = 0;
            images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
            skills[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !swaping && ableExe)
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
                }
                else
                {
                    images[i].gameObject.GetComponent<Image>().enabled = false;
                    skills[i].gameObject.GetComponent<Image>().enabled = false;
                }
            }
            StartCoroutine(FillSliderOverTime(img_Swap_coolTime, swapCool, "swap"));
        }
        if (Input.GetKeyDown(KeyCode.S) && !skilling)
        {
            StartCoroutine(FillSliderOverTime(img_Skill_coolTime, skillcool, "skill"));
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
    }

    IEnumerator ReadyAnim(Image img)
    {
        while (time < 1f)
        {
        Debug.Log("시작");
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
        Debug.Log(time);
        yield return null;
        }
        time = 0;

    }

    IEnumerator FillSliderOverTime(Image img, float coolTime, string type)
    {
        if (type == "swap")
        {
            swaping = true;
        }
        else if (type == "skill")
        {
            skilling = true;
        }
        img.enabled = true;
        float time = coolTime;
        while (time >= 0f)
        {
            time -= Time.deltaTime;
            img.fillAmount = time / coolTime;
            yield return null;
        }
        img.fillAmount = 0f;
        StartCoroutine(Ready(img, type));
        
    }
}
