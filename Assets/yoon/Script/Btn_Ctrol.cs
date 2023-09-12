using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui �̹����� �ǵ帮������ ����
using TMPro; //TextMeshProUGUI ����Ϸ� ����
using System.Runtime.InteropServices;
using System.Linq;

public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons;
    public Image[] ClearBadge;
    public Image[] SelectImage;
    public GameObject OptionPanel;
    public GameObject DifficultyScreen;
    public TextMeshProUGUI DifficultyInfo;
    public GameObject CheckBtn;
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
    public int Difficulty = 4;
    public List<bool> GameClear;
    private void Start()
    {
        dm = DataManager.instance;
        sm = SoundManager.instance;
        GameClear = dm.returnClear();
        for (int i = 0; i < GameClear.Count; i++)
        {
            if (GameClear[i] == true)
            {
                ClearBadge[i].enabled = true;
            }
            else
            {
                ClearBadge[i].enabled = false;
            }
        }
        if (dm != null && !dm.findPlayerData())
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
        if (Input.GetKeyUp(KeyCode.DownArrow)&& !OptionManager.instance.option_panel.activeSelf)
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
        else if (Input.GetKeyUp(KeyCode.UpArrow) && !OptionManager.instance.option_panel.activeSelf )
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
        else if (Input.GetKeyUp(KeyCode.Return) && !OptionManager.instance.option_panel.activeSelf )
        {
            BtnFunction(currentIndex);
        }
        select_btn(currentIndex);
        if (Difficulty != 4)
        {
            CheckBtn.GetComponent<Button>().enabled = true;
            CheckBtn.GetComponent<Image>().color = new Color32(84, 84, 84, 255);
        }
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
                Debug.Log("�� �̾߱� ����");
                OpenDifficultyScreen();
                break;
            case 1:
                Debug.Log("�̾ ����");
                if (!dm.findPlayerData())
                {
                    Debug.Log("������ ����");
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
                Debug.Log("���� ����");
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

    public void SelectDifficulty(int Level)
    {
        sm.SFXPlay("Seleect", clip);
        Difficulty = Level;
        for (int i = 0; i < SelectImage.Length; i++)
        {
            if (i != Level)
            {
                SelectImage[i].enabled = false;
            }
            else
            {
                SelectImage[i].enabled = true;
            }
        }
        switch (Difficulty)
        {
            case 0:
                DifficultyInfo.text = "+���� ������\n���� �ɷ�ġ 80%";
                break;
            case 1:
                DifficultyInfo.text = "+���� ������\n���� �ɷ�ġ 100%";
                break;
            case 2:
                DifficultyInfo.text = "���� �ɷ�ġ 120%";
                break;
        }
    }

    public void OpenDifficultyScreen()
    {
        DifficultyScreen.SetActive(true);
    }

    public void CloseDifficultyScreen()
    {
        sm.SFXPlay("Seleect", clip);
        DifficultyScreen.SetActive(false);
    }

    public void newGameStart()
    {
        dm.DeleteJson();
        dm.CreateJson(Difficulty);
        fade.CallFadeIn();
        loading_ui.DoLoading = true;
        Invoke("GoStory", 4f);
    }
}
