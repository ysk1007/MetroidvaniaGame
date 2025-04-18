using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea]
    public string dialogue;
    public Image poImage;
}

public class OwnerTalk : MonoBehaviour
{
    public MarketScript market;                                       
    public GameObject allObject;   // 모든 오브젝트
    public GameObject portrait; // 초상화 icon
    public TextMeshProUGUI txt_name;    // 초상화 이름
    public TextMeshProUGUI txt_dialogue;    // 대화 내용
    public TextMeshProUGUI txt_Enter;     // Enter 안내 문구

    public bool isDialogue; 
    public bool isShow;     // 오브젝트 켜는 변수
    private int count = 0;
    
    public Dialogue[] dialogue;

    public DataManager dm; //8.22 윤성권 추가
    public bool firstShow; //8.22 처음 보는지 확인하는 bool 값

    public void ShowDialogue()  // UI
    {
        isDialogue = false;
        allObject.SetActive(true);  // 모든 오브젝트 ON
        count = 0;
        NextDialogue();
        Time.timeScale = 0;
    }

    private void HideDialogue()
    {
        allObject.SetActive(false); // 모든 오브젝트 OFF
        Time.timeScale = 1;
    }

    private void NextDialogue() // 다음 대화 
    {
        txt_name.text = dialogue[count].name;
        txt_dialogue.text = dialogue[count].dialogue;
        portrait.GetComponent<Image>().sprite = dialogue[count].poImage.sprite;
        count++;
    }

    void Start() //8.22 윤성권 추가
    {
        dm = DataManager.instance.GetComponent<DataManager>();
        firstShow = dm.CanCenemaPlay();
        market = MarketScript.instance.GetComponent<MarketScript>();
        isDialogue = true;
    }

    void Update()
    {
        isShow = market.PlayerVisit;    // 플레이어가 상점에 닿았는지 확인하는 변수
        if (isShow && firstShow) // 8.22 처음 보는지 확인하는 조건문 추가
        {
            if (isDialogue)
            {
                ShowDialogue(); // 대화창 열기
            }
            if (Input.GetKeyUp(KeyCode.Return))     // 엔터 키를 눌렀을 때
            {
                if(count < dialogue.Length) 
                {
                    NextDialogue();     // 다음 대화
                }
                else
                {
                    HideDialogue();     // 대화 창 숨기기
                }
            }
        }
    }
}
