using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    public GameObject option_panel;
    private bool open_option = false;

    public GameObject Stacks;
    public TextMeshProUGUI[] SelectStacks;

    public GameObject Timer;
    public TextMeshProUGUI PlayTimerText;
    public float TotalPlayTime = 0f;
    public bool Playing = false;

    public int minutes;
    public int seconds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            TotalPlayTime += Time.deltaTime;
            UpdateTimerText();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!open_option)
                {
                    option_panel.SetActive(true);
                    open_option = true;
                    Select_Stack_Setting();
                }
                else
                {
                    option_panel.SetActive(false);
                    open_option = false;
                }
            }
        }
    }

    void Select_Stack_Setting()
    {
        if (Player.instance != null)
        {
            Timer.SetActive(true);
            Stacks.SetActive(true);
            int[] getLevel = new int[9];
            getLevel = Player.instance.returnPlayerSelectLevel();
            for (int i = 0; i < getLevel.Length; i++)
            {
                if (getLevel[i] == 3)
                {
                    SelectStacks[i].text = "MAX";
                    SelectStacks[i].fontSize = 30;
                }
                else
                {
                    SelectStacks[i].text = getLevel[i].ToString();
                }
            }
        }
    }

    void UpdateTimerText()
    {
        minutes = Mathf.FloorToInt(TotalPlayTime / 60);
        seconds = Mathf.FloorToInt(TotalPlayTime % 60);

        PlayTimerText.text = "플레이 시간 : "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string returnTimerText()
    {
        string returnStr;
        int hours;
        int min;

        hours = Mathf.FloorToInt(TotalPlayTime / 60)/60;
        min = Mathf.FloorToInt(TotalPlayTime / 60) % 60;

        returnStr = string.Format("{0}시간 {1}분", hours, min);
        return returnStr;
    }

}
