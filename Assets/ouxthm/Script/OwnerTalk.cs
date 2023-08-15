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
    MarketScript market;                                       
    public GameObject allObject;   // 모든 오브젝트
    public GameObject portrait; // 초상화 icon
    public TextMeshProUGUI txt_name;    // 초상화 이름
    public TextMeshProUGUI txt_dialogue;    // 대화 내용
    public TextMeshProUGUI txt_Enter;     // Enter 안내 문구

    public bool isDialogue; 
    public bool isShow;     // 오브젝트 켜는 변수
    private int count = 0;
    
    public Dialogue[] dialogue;

    public void ShowDialogue()  // UI
    {
        isDialogue = false;
        allObject.SetActive(true);  // 모든 오브젝트 ON
        count = 0;
        NextDialogue();
    }

    private void HideDialogue()
    {
        allObject.SetActive(false); // 모든 오브젝트 OFF
    }

    private void NextDialogue() // 다음 대화 
    {
        txt_name.text = dialogue[count].name;
        txt_dialogue.text = dialogue[count].dialogue;
        portrait.GetComponent<Image>().sprite = dialogue[count].poImage.sprite;
        count++;
    }
    void Awake()
    {
        market = MarketScript.instance.GetComponent<MarketScript>();   
        isDialogue = true;
    }
    void Update()
    {
        isShow = market.PlayerVisit;  
        if (isShow)
        {
            if (isDialogue)
            {
                ShowDialogue();
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if(count < dialogue.Length)
                {
                    NextDialogue();
                }
                else
                {
                    HideDialogue();
                }
            }
        }
    }
}
