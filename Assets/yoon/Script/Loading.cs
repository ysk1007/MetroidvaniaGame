using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static Loading instance;
    public SoundManager sm;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI TipText;
    int count = 1;
    string mark = ".";
    string BaseText = "로딩중";
    public bool ingame = false;
    public bool DoLoading = false;
    public GameObject FadeImg;
    public Fade_img fade;
    // Update is called once per frame

    void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        sm = SoundManager.instance;
        if (ingame)
        {
            DoLoading = true;
            Invoke("off",3f);
        }
        Mark();
        UpdateTip();
    }

    public void Load()
    {
        sm.Loading();
        fade.CallFadeIn();
        DoLoading = true;
        Invoke("off", 3f);
        Mark();
        UpdateTip();
    }

    void Mark()
    {
        if (!DoLoading)
        {
            return;
        }
        loadingText.text = BaseText;
        for (int i = 0; i < count; i++)
        {
            loadingText.text += mark;
        }
        count++;
        if (count > 3)
        {
            count = 1;
        }
        Invoke("Mark",0.5f);
    }

    void UpdateTip()
    {
        if (TipText != null) // TipText가 null이 아닌지 확인
        {
            string text = "#팁 : ";
            int randNum = Random.Range(0, 7);
            Debug.Log(randNum);
            switch (randNum)
            {
                case 0:
                    text += "대쉬를 사용하면 잠깐 적의 공격을 회피할 수 있습니다.";
                    break;
                case 1:
                    text += "아이템을 판매하면 원래 가격의 1/3 골드를 획득 할 수 있습니다.";
                    break;
                case 2:
                    text += "아이템 등급은 일반 -> 고급 -> 희귀 -> 영웅 -> 전설 -> 신화 등급으로 나뉘어져 있습니다.";
                    break;
                case 3:
                    text += "한 번 획득한 아이템은 다시 등장하지 않습니다.";
                    break;
                case 4:
                    text += "선택한 숙련도 무기는 변경할 수 없습니다.";
                    break;
                case 5:
                    text += "상점 주인은 돈이 많습니다.";
                    break;
                case 6:
                    text += "한 대도 맞지 않고 엔딩을 본다면... 무슨 일이 일어날지도 모릅니다.";
                    break;
            }
            TipText.text = text;
        }
    }

    void off()
    {
        FadeImg.SetActive(true);
        gameObject.SetActive(false);
        DoLoading = false;
        fade.CallFadeOut();
    }
}
