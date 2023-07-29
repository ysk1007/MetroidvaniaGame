using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI 사용하려 참조

public class OptionScript : MonoBehaviour
{
    public TextMeshProUGUI ScreenText;
    public TextMeshProUGUI ResolutionText;
    enum ScreenState
    {
        FullScreen,
        Windowscreen,
        NoBorder
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChange()
    {
        
    }

    public void OptionClose()
    {
        this.gameObject.SetActive(false);
    }

    public void OptionOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void GameExit()
    {
        DataManager.instance.JsonSave("PlayerData");
        DataManager.instance.JsonSave("SliderData");
        DataManager.instance.JsonSave("ItemData");
        Application.Quit();
    }
}
