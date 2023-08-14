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

    [SerializeField] private GameObject blackBox; // 검은 배경, 검은 대화박스 부모 오브젝트
    [SerializeField] private GameObject portrait; // 초상화 icon
    [SerializeField] private TextMeshProUGUI txt_name;    // 초상화 이름
    [SerializeField] private TextMeshProUGUI txt_dialogue;    // 대화 내용
    public bool isDialogue = false; 
    public bool isShow = false;     // 잠깐 보이게 하려 제작
    private int count = 0;

    [SerializeField] private Dialogue[] dialogue;

    public void ShowDialogue()
    {
        blackBox.gameObject.SetActive(true);
        portrait.gameObject.SetActive(true);
        txt_name.gameObject.SetActive(true);
        txt_dialogue.gameObject.SetActive(true);
        count = 0;
        isDialogue = true;
        NextDialogue();
    }

    private void HideDialogue()
    {
        blackBox.gameObject.SetActive(false);
        portrait.gameObject.SetActive(false);
        txt_name.gameObject.SetActive(false);
        txt_dialogue.gameObject.SetActive(false);

        isDialogue = false;
    }

    private void NextDialogue()
    {
        txt_name.text = dialogue[count].name;
        txt_dialogue.text = dialogue[count].dialogue;
        portrait.GetComponent<Image>().sprite = dialogue[count].poImage.sprite;
        count++;
    }

    void Update()
    {
        if (isDialogue)
        {
            if (isShow) // 잠시 추가한 조건
            {
                ShowDialogue();
                isShow = false;
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
