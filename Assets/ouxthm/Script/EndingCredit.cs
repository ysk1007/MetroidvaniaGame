using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public GameObject skipButton;   // 스킵 이미지와 텍스트가 들어있는 오브젝트
    [SerializeField] private GameObject playerUI;   // 플레이어 UI
    public GameObject credit;   // 실질적 크레딧
    public Image Panel;     // 검은 박스

    public AudioSource audioSource;

    public bool isEnding = false;       // 엔딩인지 확인하는 변수
    public bool isSkip = false;   // 스킵
    public bool isShow = false;
    public bool volumDown = false;  // 볼륨 줄이기
    float time = 0f;
    float vtime = 0f;
    float F_time = 5f;
    float V_time = 150f;
    float currentVolume;        // 볼륨 바 이동시킬 변수

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeOut());  // 화면 페이드 아웃 
            credit.SetActive(true);     // 엔딩 크레딧 오브젝트 활성화
            playerUI.SetActive(false);  // 플레이어 UI 끄기
            StartCoroutine(GoTitleSceneVolume());   // 크레딧 끝나기 2초 전 볼륨 줄이는 코루틴
            StartCoroutine(GoTitleScene());     // 크레딧 끝나고타이틀 화면으로 이동하는 코루틴

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
                controlVolum();
                Invoke("SkipTitleScene", 1.5f);
            }
            if (volumDown)
            {
                controlVolum();
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
    IEnumerator GoTitleSceneVolume()
    {
        Debug.Log("58초 전");
        yield return new WaitForSeconds(58f);
        Debug.Log("58초 후");
        controlVolum();
    }
    IEnumerator GoTitleScene()      // 60초 뒤 로고 화면으로 이동
    {
        Debug.Log("60초 전");
        yield return new WaitForSeconds(60f);
        Debug.Log("60초 후");
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

    IEnumerator controlVolum()
    {
        vtime += Time.deltaTime / V_time;
        while (currentVolume < 1)
        {
            currentVolume = Mathf.Lerp(audioSource.volume, 0, vtime);  // Time.deltaTIme에 변수 값 추가해서 변동 범위 넓혀야 함.
            audioSource.volume = currentVolume;
            return null;
        }
        return null;
    }
}
