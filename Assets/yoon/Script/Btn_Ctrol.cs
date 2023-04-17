using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui 이미지를 건드리기위해 참조
using TMPro; //TextMeshProUGUI 사용하려 참조
public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons; //버튼들 담을 배열
    public OptionScript OptionCanvas; //옵션 캔버스 담을 오브젝트 변수
    public GameObject Panel; //옵션 Panel_ui 담을 오브젝트 변수
    public AudioClip clip; // 오디오 클립(효과음) 담을 변수
    Scene_Move Scene_Move; //씬 이동 스크립트 참조

    public int[] index = { }; //버튼의 인덱스를 담을 배열
    public int currentIndex; //현재 인덱스를 가르킬 변수
    private void Start()
    {
        OptionCanvas = GameObject.Find("Option_Canvas").GetComponent<OptionScript>(); //옵션 캔버스를 찾아와 OptionScript를 가져옵니다
        Panel = OptionCanvas.transform.GetChild(0).gameObject; //옵션 캔버스 바로 하위 오브젝트를 가져옴
        Scene_Move = GetComponent<Scene_Move>(); //씬 이동 스크립트 가져옴
        buttons = GetComponentsInChildren<Button>(); //버튼들 배열에 담음
        index = new int[buttons.Length]; // 인덱스 길이는 버튼배열의 길이만큼
        for (int i = 0; i < buttons.Length; i++) //배열에 인덱스 추가함
        {
            index[i] = i;
        }
        currentIndex = 0; //현재 인덱스는 0으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow)&& !Panel.activeSelf) //옵션 캔버스가 꺼져있을때 아래 방향키를 누르면
        {
            if (currentIndex == buttons.Length - 1) //현재 인덱스가 마지막이라면
            {
                GetBtnImpo(0); //처음 인덱스로 돌아감
            }
            else { GetBtnImpo(currentIndex + 1); } //현재 인덱스 1 추가
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && !Panel.activeSelf) //옵션 캔버스가 꺼져있을때 위 방향키를 누르면
        {
            if (currentIndex == 0) //현재 인덱스가 첫 번째라면
            {
                GetBtnImpo(buttons.Length - 1); //마지막 인덱스로 감
            }
            else { GetBtnImpo(currentIndex - 1); } //현재 인덱스 1 감소
        }
        else if (Input.GetKeyUp(KeyCode.Return) && !Panel.activeSelf)  //엔터키를 누르면 현재 인덱스 기능 사용
        {
            BtnFunction(currentIndex); //-> BtnFunction 함수
        }
        select_btn(currentIndex); //-> select_btn 함수
    }

    void select_btn(int index) //선택된 인덱스의 버튼 강조하는 함수
    {
        TextMeshProUGUI text = buttons[index].GetComponentInChildren<TextMeshProUGUI>(); //현재 인덱스의 버튼의 문자를 가져옴
        text.fontSize = 50; //폰트 사이즈를 키움
        text.color = new Color32(226, 190, 50, 255); //노란색으로 강조
    }

    public void GetBtnImpo(int index) //버튼 클릭이 들어왔을때 정보를 받는 함수
    {
        if(index == currentIndex) //현재 인덱스와 선택 인덱스가 같으면
        {
            BtnFunction(currentIndex); //기능 실행
        }
        else //새로운 버튼을 누르면
        {
            SoundManager.instance.SFXPlay("Seleect", clip); //버튼 클릭 효과음
            TextMeshProUGUI text = buttons[currentIndex].GetComponentInChildren<TextMeshProUGUI>();

            //전에 눌렀던 버튼 강조 효과를 품
            text.fontSize = 40; 
            text.color = new Color32(255, 255, 255, 255);

            currentIndex = index; //현재 인덱스를 클릭한 버튼의 인덱스로 설정
        }
        
    }

    void BtnFunction(int index) //기능 함수
    {
        switch(index) //스위치문으로 인덱스 번호를 받아 각 기능 실행
        {
            case 0:
                Debug.Log("새 이야기 시작");
                Scene_Move.SceneLoader("ingame");
                break;
            case 1:
                Debug.Log("이어서 시작");
                break;
            case 2:
                OptionCanvas.GetComponent<OptionScript>().OptionOpen();
                break;
            case 3:
                Debug.Log("게임 종료");
                break;
        }
    }
}
