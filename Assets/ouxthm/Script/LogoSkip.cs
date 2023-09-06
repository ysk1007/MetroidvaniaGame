using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;
    [SerializeField] private Image fadeOutScreen;

    private bool isSkip = false;        // Enter 키가 눌렸는지 확인하는 변수
    private bool isShow = false;        // ShowSkipButton를 Update 문에서 한 번만 불러올 수 있도록 하는 변수
    private float etime = 0f;       // blackScreen에 사용
    private float E_Time = 300f;    // blackScreen에 사용
    public bool story = false;

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
            StartCoroutine(Logo_FadeOut());
            if (story)
            {
                Invoke("GO_Ingame_Scene", 4f);      // 인게임 씬으로 이동
            }
            /*else
            {
                Invoke("GO_Title_Scene", 1.5f);      // 타이틀 씬으로 이동
            }*/
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
    IEnumerator Logo_FadeOut()       // 엔딩 크레딧 페이드 아웃 활성화
    {
        Color blackalpha = fadeOutScreen.color;
        while (blackalpha.a < 1f)
        {
            etime += Time.deltaTime / E_Time;
            blackalpha.a = Mathf.Lerp(0, 1, etime);       // 이미지 알파값 0에서 1까지 서서히 변경
            fadeOutScreen.color = blackalpha;
            yield return null;
        }
        yield return null;
    }
    void GO_Ingame_Scene()  // 인게임 씬으로 이동하는 함수
    {
        SceneManager.LoadScene("ingame_scene");
    }

    void GO_Title_Scene()
    {
        SceneManager.LoadScene("Title_Scene");
    }
}
