using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;

    private bool isSkip = false;

    void Update()
    {
        Invoke("ShowSkipButton", 5f);
        GoSkip();       // Enter이 눌렸는지 확인하는 함수 (눌렸다면 isSkip 참)
        if (isSkip) 
        {
            SceneManager.LoadScene("Title_Scene");      // 타이틀 화면으로
        }

    }

    public void ShowSkipButton()
    {
        enterButton.SetActive(true);   
    }

    public void GoSkip()        // isSkip 변수 참으로 만드는 함수
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;
        }
    }
}
