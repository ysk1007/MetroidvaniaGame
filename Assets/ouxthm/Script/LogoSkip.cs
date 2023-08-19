using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;

    private bool isSkip = false;        // Enter 키가 눌렸는지 확인하는 변수
    private bool isShow = false;        // ShowSkipButton를 Update 문에서 한 번만 불러올 수 있도록 하는 변수

    void Update()
    {
        if (!isShow)
        {
            Invoke("ShowSkipButton", 6f);       // 6초 뒤 스킵 이미지 활성화 
        }        
        else if (isShow)
        {
            GoSkip();       // Enter이 눌렸는지 확인하는 함수 (눌렸다면 isSkip 참)
        }
        if (isSkip) 
        {
            SceneManager.LoadScene("Title_Scene");      // 타이틀 화면으로
        }
    }

    public void ShowSkipButton()        // 스킵이 가능하다는 이미지를 보여주는 함수
    {
        enterButton.SetActive(true);   // 스킵 이미지 활성화
        isShow = true;
    }

    public void GoSkip()        // isSkip 변수 참으로 만드는 함수
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;      

        }
    }
}
