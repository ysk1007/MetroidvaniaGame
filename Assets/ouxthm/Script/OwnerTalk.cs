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
    [SerializeField] private GameObject blackBox; // ���� ���, ���� ��ȭ�ڽ� �θ� ������Ʈ
    [SerializeField] private GameObject portrait; // �ʻ�ȭ icon
    [SerializeField] private TextMeshProUGUI txt_name;    // �ʻ�ȭ �̸�
    [SerializeField] private TextMeshProUGUI txt_dialogue;    // ��ȭ ����
    [SerializeField] private TextMeshProUGUI txt_Enter;     // Enter �ȳ� ����
    public bool isDialogue; 
    public bool isShow;     // ������Ʈ �Ѵ� ����
    private int count = 0;
    
    [SerializeField] private Dialogue[] dialogue;

    public void ShowDialogue()
    {
        blackBox.SetActive(true);
        portrait.SetActive(true);
        txt_name.gameObject.SetActive(true);
        txt_dialogue.gameObject.SetActive(true);
        txt_Enter.gameObject.SetActive(true);

        count = 0;
        isDialogue = false;
        NextDialogue();
    }

    private void HideDialogue()
    {
        blackBox.gameObject.SetActive(false);
        portrait.gameObject.SetActive(false);
        txt_name.gameObject.SetActive(false);
        txt_dialogue.gameObject.SetActive(false);
        txt_Enter.gameObject.SetActive(false);
    }

    private void NextDialogue()
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
