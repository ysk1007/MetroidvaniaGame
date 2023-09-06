using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui 이미지를 건드리기위해 참조
using TMPro; //TextMeshProUGUI 사용하려 참조
public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons;
    public GameObject OptionPanel;
    public AudioClip clip;
    Scene_Move Scene_Move;
    public Fade_img fade;
    public Loading loading_ui;
    public DataManager dm;
    SoundManager sm;

    public GameObject continueBtn;
    public bool noContinue = false;

    public int[] index = { };
    public int currentIndex;
    private void Start()
    {
        dm = DataManager.instance;
        if (!dm.findPlayerData())
        {
            Destroy(continueBtn.GetComponent<Button>());
            Color32 color = new Color32(255, 255, 255, 100);
            continueBtn.GetComponentInChildren<TextMeshProUGUI>().color = color;
            noContinue = true;
        }
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
            else 
            {
                if (noContinue && currentIndex == 0)
                {
                    GetBtnImpo(currentIndex + 2);
                }
                else
                {
                    GetBtnImpo(currentIndex + 1);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && !OptionPanel.activeSelf)
        {
            if (currentIndex == 0)
            {
                GetBtnImpo(buttons.Length - 1);
            }
            else 
            {
                if (noContinue && currentIndex == 2)
                {
                    GetBtnImpo(currentIndex - 2);
                }
                else
                {
                    GetBtnImpo(currentIndex - 1);
                }
            }
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
            sm = SoundManager.instance;
            sm.SFXPlay("Seleect", clip);
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
                Debug.Log("새 이야기 시작");
                dm.DeleteJson();
                dm.CreateJson();
                fade.CallFadeIn();
                loading_ui.DoLoading = true;
                Invoke("GoStory", 4f);
                break;
            case 1:
                Debug.Log("이어서 시작");
                if (!dm.findPlayerData())
                {
                    Debug.Log("데이터 없음");
                    break;
                }
                else
                {
                    fade.CallFadeIn();
                    loading_ui.DoLoading = true;
                    Invoke("GoInGame", 4f);
                    break;
                }
            case 2:
                if (OptionManager.instance != null)
                {
                    OptionManager.instance.option_panel.SetActive(true);
                }
                break;
            case 3:
                Debug.Log("게임 종료");
                Application.Quit();
                break;
        }
    }

    void GoInGame()
    {
        Scene_Move.SceneLoader("ingame_scene");
    }

    void GoStory()
    {
        Scene_Move.SceneLoader("Story_Scene");
    }

}
