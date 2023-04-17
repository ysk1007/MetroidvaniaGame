using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour
{
    public GameObject WeaponUi; //무기 이미지들의 상위 오브젝트
    public Image[] images; //무기 이미지들을 담을 변수
    public int currentWeaponIndex = 0; //현재 무기 인덱스
    public float swapCool = 2f; //무기 변경 쿨타임

    public bool swaping = false; //현재 무기 변경중인가?

    public bool ableExe = false; //도끼 사용 가능?
    public bool ableBow = false; //활 사용 가능?

    public Image img_coolTime; //쿨타임 이미지
    public Image Tab_key; //변경 키 이미지
    private void Awake()
    {
        if (!ableExe && !ableBow) Tab_key.enabled = false; //교체할 무기가 아직 없으면 탭 키 이미지 안 보임
        images = WeaponUi.GetComponentsInChildren<Image>(); //무기 이미지들 배열에 할당
        images[currentWeaponIndex].gameObject.GetComponent<Image>().enabled = true; //현재 인덱스 무기만 이미지 활성화
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ableExe || ableBow) //교체할 무기가 아직 없으면 탭 키 이미지 안 보임
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
            // 인덱스 값이 이미지 개수를 초과하면 처음 이미지로 돌아감
            if (currentWeaponIndex >= images.Length)
            {
                currentWeaponIndex = 0;
            }

            // 모든 이미지를 비활성화하고, 선택한 이미지만 활성화
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
