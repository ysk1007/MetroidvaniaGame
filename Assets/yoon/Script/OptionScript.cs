using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI 사용하려 참조

public class OptionScript : MonoBehaviour
{
    public TextMeshProUGUI ScreenText;
    public TextMeshProUGUI ResolutionText;
    public GameObject Loading_Screen;
    public GameObject Check_Screen;
    public Scene_Move sm;
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

    public void CheckScreenOn()
    {
        Check_Screen.SetActive(true);
    }

    public void CheckScreenOff()
    {
        Check_Screen.SetActive(false);
    }

    public void GameExit()
    {
        CheckScreenOff();
        Loading_Screen.GetComponent<Loading>().Load();
        OptionManager.instance.Playing = false;
        OptionManager.instance.Timer.SetActive(false);
        OptionManager.instance.Stacks.SetActive(false);
        sm.Wait_And_SceneLoader("Main_Scene");
        this.gameObject.SetActive(false);
    }
}
