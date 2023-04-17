using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI 사용하려 참조
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour
{
    public TextMeshProUGUI ScreenText;
    public TextMeshProUGUI ResolutionText;
    public GameObject OptionCanvas;
    public GameObject Ui_Panel;
    public static GameObject instance; //사운드 매니저 인스턴스(클래스의 정의를 통해 만들어진 객체)
    enum ScreenState
    {
        FullScreen,
        Windowscreen,
        NoBorder
    }

    private void Awake()
    {
        //싱글톤 패턴
        if (instance == null) //사운드 매니저가 없으면
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance); //씬 전환시 파괴되지 않음
        }
        else //사운드 매니저가 이미 있으면
        {
            Destroy(gameObject); //자신은 파괴
        }
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

    public void OptionClose() //옵션 창 닫는 함수
    {
        Ui_Panel.SetActive(false);
    }

    public void OptionOpen() //옵션 창 활성화 함수
    {
        Ui_Panel.SetActive(true);
    }
}
