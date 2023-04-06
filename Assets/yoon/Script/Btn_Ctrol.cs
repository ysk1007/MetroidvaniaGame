using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui 이미지를 건드리기위해 참조
using TMPro; //TextMeshProUGUI 사용하려 참조
public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons;

    public AudioClip clip;

    public int[] index = { };
    public int currentIndex;
    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        index = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            index[i] = i;
        }
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SoundManager.instance.SFXPlay("Seleect",clip);
            if (currentIndex == buttons.Length - 1)
            {
                GetBtnImpo(0);
            }
            else { GetBtnImpo(currentIndex + 1); }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SoundManager.instance.SFXPlay("Seleect",clip);
            if (currentIndex == 0)
            {
                GetBtnImpo(buttons.Length - 1);
            }
            else { GetBtnImpo(currentIndex - 1); }
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            BtnFunction(currentIndex);
        }
        select_btn(currentIndex);
    }

    void select_btn(int index)
    {
        TextMeshProUGUI text = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
        text.fontSize = 50;
        text.color = new Color32(226, 190, 50, 255);
    }

    public void GetBtnImpo(int index)
    {
        if(index == currentIndex)
        {
            BtnFunction(currentIndex);
        }
        else
        {
            TextMeshProUGUI text = buttons[currentIndex].GetComponentInChildren<TextMeshProUGUI>();
            text.fontSize = 40;
            text.color = new Color32(255, 255, 255, 255);
            currentIndex = index;
        }
        
    }

    void BtnFunction(int index)
    {
        switch(index)
        {
            case 0:
                Debug.Log("새 이야기 시작");
                break;
            case 1:
                Debug.Log("이어서 시작");
                break;
            case 2:
                Debug.Log("옵션창");
                break;
            case 3:
                Debug.Log("게임 종료");
                break;
        }
    }
}
