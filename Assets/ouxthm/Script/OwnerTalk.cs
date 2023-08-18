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
    public GameObject allObject;   // ��� ������Ʈ
    public GameObject portrait; // �ʻ�ȭ icon
    public TextMeshProUGUI txt_name;    // �ʻ�ȭ �̸�
    public TextMeshProUGUI txt_dialogue;    // ��ȭ ����
    public TextMeshProUGUI txt_Enter;     // Enter �ȳ� ����

    public bool isDialogue; 
    public bool isShow;     // ������Ʈ �Ѵ� ����
    private int count = 0;
    public bool isStop = false; // ��ȭ�� �� Ÿ�� ������ 0,1�� �����ϴ� ����
    
    public Dialogue[] dialogue;

    public void ShowDialogue()  // UI
    {
        isDialogue = false;
        allObject.SetActive(true);  // ��� ������Ʈ ON
        count = 0;
        NextDialogue();
        isStop = true;
    }

    private void HideDialogue()
    {
        allObject.SetActive(false); // ��� ������Ʈ OFF
        isStop = false;
    }

    private void NextDialogue() // ���� ��ȭ 
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
        isShow = market.PlayerVisit;    // �÷��̾ ������ ��Ҵ��� Ȯ���ϴ� ����
        if (isShow)
        {
            if (isDialogue)
            {
                ShowDialogue(); // ��ȭâ ����
            }
            if (Input.GetKeyUp(KeyCode.Return))     // ���� Ű�� ������ ��
            {
                if(count < dialogue.Length) 
                {
                    NextDialogue();     // ���� ��ȭ
                }
                else
                {
                    HideDialogue();     // ��ȭ â �����
                }
            }
        }
        if (isStop) 
        {
            Time.timeScale = 0;
        }
        else if (!isStop)
        {
            Time.timeScale = 1;
        }
    }
}
