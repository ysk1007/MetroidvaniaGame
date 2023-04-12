using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public GameObject WeaponUi;
    public Image[] images;
    public int currentWeaponIndex = 0;
    public float swapCool = 2f;

    public bool swaping = false;

    public bool ableExe = false;
    public bool ableBow = false;

    public Image img_coolTime;
    public Image Tab_key;
    private void Awake()
    {
        if (!ableExe && !ableBow) Tab_key.enabled = false;
        images = WeaponUi.GetComponentsInChildren<Image>();
        images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
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
            currentWeaponIndex = 0;
            images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !swaping && ableExe)
        {
            currentWeaponIndex++;

            if (currentWeaponIndex == 2 && !ableBow)
            {
                currentWeaponIndex = 0;
            }
            // �ε��� ���� �̹��� ������ �ʰ��ϸ� ó�� �̹����� ���ư��ϴ�.
            if (currentWeaponIndex >= images.Length)
            {
                currentWeaponIndex = 0;
            }

            // ��� �̹����� ��Ȱ��ȭ�ϰ�, ������ �̹����� Ȱ��ȭ�մϴ�.
            for (int i = 0; i < images.Length; i++)
            {
                if (i == currentWeaponIndex)
                {
                    images[i].gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    images[i].gameObject.GetComponent<Image>().enabled = false;
                }
            }
            StartCoroutine(FillSliderOverTime());
        }
    }

    IEnumerator Ready()
    {
        Color color = img_coolTime.color;
        img_coolTime.color = new Color32 (255,255,255,200);
        yield return null;
        img_coolTime.color = color;
        img_coolTime.fillAmount = 1f;

        img_coolTime.enabled = false;
        swaping = false;
    }

    IEnumerator FillSliderOverTime()
    {
        swaping = true;
        img_coolTime.enabled = true;
        float time = swapCool;
        while (time >= 0f)
        {
            time -= Time.deltaTime;
            img_coolTime.fillAmount = time / swapCool;
            yield return null;
        }
        img_coolTime.fillAmount = 0f;
        StartCoroutine(Ready());
        
    }
}
