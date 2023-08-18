using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui �̹����� �ǵ帮������ ����
using TMPro; //TextMeshProUGUI ����Ϸ� ����
public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons;
    public GameObject OptionPanel;
    public AudioClip clip;
    Scene_Move Scene_Move;
    public Fade_img fade;
    public Loading loading_ui;

    public int[] index = { };
    public int currentIndex;
    private void Start()
    {
        Scene_Move = GetComponent<Scene_Move>();
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
        if (Input.GetKeyUp(KeyCode.DownArrow)&& !OptionPanel.activeSelf)
        {
            if (currentIndex == buttons.Length - 1)
            {
                GetBtnImpo(0);
            }
            else { GetBtnImpo(currentIndex + 1); }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && !OptionPanel.activeSelf)
        {
            if (currentIndex == 0)
            {
                GetBtnImpo(buttons.Length - 1);
            }
            else { GetBtnImpo(currentIndex - 1); }
        }
        else if (Input.GetKeyUp(KeyCode.Return) && !OptionPanel.activeSelf)
        {
            BtnFunction(currentIndex);
        }
        select_btn(currentIndex);
    }

    void select_btn(int index)
    {
        TextMeshProUGUI text = buttons[index].GetComponentInChildren<TextMeshProUGUI>();
        text.fontSize = 36;
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
            SoundManager.instance.SFXPlay("Seleect", clip);
            TextMeshProUGUI text = buttons[currentIndex].GetComponentInChildren<TextMeshProUGUI>();
            text.fontSize = 30;
            text.color = new Color32(255, 255, 255, 255);
            currentIndex = index;
        }
        
    }

    void BtnFunction(int index)
    {
        switch(index)
        {
            case 0:
                DataManager dm = DataManager.instance.GetComponent<DataManager>();
                Debug.Log("�� �̾߱� ����");
                dm.DeleteJson();
                dm.JsonLoad("Default");
                fade.CallFadeIn();
                loading_ui.DoLoading = true;
                Invoke("GoInGame",4f);
                break;
            case 1:
                Debug.Log("�̾ ����");
                fade.CallFadeIn();
                loading_ui.DoLoading = true;
                Invoke("GoInGame", 4f);
                break;
            case 2:
                OptionPanel.SetActive(true);
                break;
            case 3:
                Debug.Log("���� ����");
                break;
        }
    }

    void GoInGame()
    {
        Scene_Move.SceneLoader("ingame_scene");
    }

}
