using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동 사용할때 참조
public class Scene_Move : MonoBehaviour
{
    string currentSceneName; //현재 씬이름을 저장하는 변수
    bool currentSceneTitle = false; //현재 씬이름이 타이틀씬이 판단하는 변수
    bool SceneChange = false; //현재 씬을 전환중인가? 변수

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name; //현재 씬 이름을 불러옴
        if (currentSceneName == "Title_Scene") currentSceneTitle = true; //씬 이름이 타이틀 화면이면 감지
    }

    void Update()
    {
        if (Input.anyKeyDown && currentSceneTitle && !SceneChange) //아무키나입력 받았을때, 현재 타이틀 화면일때, 씬 전환중이 아닐때
        {
            SceneChange = true; //씬 전환중 (중복으로 호출하는것을 방지)
            Fade_img fade = gameObject.GetComponent<Fade_img>(); //코드가 담긴 오브젝트의 Fade_img 스크립트를 참조
            fade.CallFadeIn(); //Fade_img 스크립트의 CallFadeIn()함수를 호출함

            FadeEffect_Text fade_text = gameObject.GetComponent<FadeEffect_Text>(); //코드가 담긴 오브젝트의 FadeEffect_Text 스크립트를 참조
            fade_text.FastEffect(); //fade_text 스크립트의 FastEffect()함수를 호출함

            Wait_And_SceneLoader("Main_Scene"); //기다렸다 씬 전환
        }
    }

    public void SceneLoader(string sceneName) // 씬 이동 함수 (매개변수는 씬 이름)
    {
        SceneManager.LoadScene(sceneName); // 씬을 불러옵니다.
    }

    public void Wait_And_SceneLoader(string sceneName) // 기다렸다 씬 이동 함수 (매개변수는 씬 이름)
    {
        StartCoroutine(Wait(sceneName)); //Wait코루틴 시작
        
    }

    IEnumerator Wait(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName); // 씬을 불러옵니다.
    }


    }
