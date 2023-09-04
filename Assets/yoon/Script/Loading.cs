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
    string BaseText = "�ε���";
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
        if (TipText != null) // TipText�� null�� �ƴ��� Ȯ��
        {
            string text = "#�� : ";
            int randNum = Random.Range(0, 4);
            Debug.Log(randNum);
            switch (randNum)
            {
                case 0:
                    text += "�뽬�� ����ϸ� ��� ���� ������ ȸ���� �� �ֽ��ϴ�.";
                    break;
                case 1:
                    text += "�������� �Ǹ��ϸ� ���� ������ 1/3 ��带 ȹ�� �� �� �ֽ��ϴ�.";
                    break;
                case 2:
                    text += "������ ����� �Ϲ� -> ��� -> ��� -> ���� -> ���� -> ��ȭ ������� �������� �ֽ��ϴ�.";
                    break;
                case 3:
                    text += "�� �� ȹ���� �������� �ٽ� �������� �ʽ��ϴ�.";
                    break;
            }
            TipText.text = text;
            Invoke("UpdateTip", 2.5f) ;
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
