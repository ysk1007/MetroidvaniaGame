using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public GameObject skipButton;   // 스킵 이미지와 텍스트가 들어있는 오브젝트
    public GameObject credit;   // 실질적 크레딧
    public Image Panel;     // 검은 박스

    public bool isEnding = false;       // 엔딩인지 확인하는 변수
    public bool isSkip = false;   // 스킵
    public bool isShow = false;
    float time = 0f;
    float F_time = 5f;

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeOut());  // 화면 페이드 아웃 
            credit.SetActive(true);     // 엔딩 크레딧 오브젝트 활성화
            StartCoroutine(GoTitleScene());     // 타이틀 화면으로 이동
            if (!isShow)
            {
                Invoke("ShowSkipButton", 15f);      // 스킵 버튼 오브젝트 활성화 15초 뒤
            }
            else if (isShow)
            {
                GoSkip();      
            }

            if (isSkip)
            {
                SkipTitleScene(); 
            }

        }
    }
    IEnumerator FadeOut()       // 페이드 아웃 활성화
    {
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);       // 이미지 알파값 0에서 1까지 서서히 변경
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }
    IEnumerator GoTitleScene()      // 60초 뒤 로고 화면으로 이동
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("Logo_Scene");      
    }
    public void SkipTitleScene()        // 즉시 로고 화면으로 이동
    {
        time = 0f;
        SceneManager.LoadScene("Logo_Scene");
    }

    public void ShowSkipButton()        // 스킵 버튼 활성화 함수
    {
        skipButton.SetActive(true);
        isShow = true;
    }
    public void GoSkip()        // isSkip 변수 참으로 만드는 함수
    {
        if(isShow && Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;
        }
    }
}
