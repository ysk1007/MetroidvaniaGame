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
    //[SerializeField] private
    public GameObject blackBox;
    public GameObject portrait;
    public TextMeshProUGUI txt_name;
    public TextMeshProUGUI txt_dialogue;
    public bool isDialogue = false;
    public bool isShow = false;
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
            if (isShow)
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
