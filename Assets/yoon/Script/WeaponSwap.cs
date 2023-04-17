using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public GameObject WeaponUi; //���� �̹������� ���� ������Ʈ
    public Image[] images; //���� �̹������� ���� ����
    public int currentWeaponIndex = 0; //���� ���� �ε���
    public float swapCool = 2f; //���� ���� ��Ÿ��

    public bool swaping = false; //���� ���� �������ΰ�?

    public bool ableExe = false; //���� ��� ����?
    public bool ableBow = false; //Ȱ ��� ����?

    public Image img_coolTime; //��Ÿ�� �̹���
    public Image Tab_key; //���� Ű �̹���
    private void Awake()
    {
        if (!ableExe && !ableBow) Tab_key.enabled = false; //��ü�� ���Ⱑ ���� ������ �� Ű �̹��� �� ����
        images = WeaponUi.GetComponentsInChildren<Image>(); //���� �̹����� �迭�� �Ҵ�
        images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true; //���� �ε��� ���⸸ �̹��� Ȱ��ȭ
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ableExe || ableBow) //��ü�� ���Ⱑ ���� ������ �� Ű �̹��� �� ����
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
            // �ε��� ���� �̹��� ������ �ʰ��ϸ� ó�� �̹����� ���ư�
            if (currentWeaponIndex >= images.Length)
            {
                currentWeaponIndex = 0;
            }

            // ��� �̹����� ��Ȱ��ȭ�ϰ�, ������ �̹����� Ȱ��ȭ
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
