using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //TextMeshProUGUI 사용하려 참조


public class FadeEffect_Text : MonoBehaviour
{
    public float fadeSpeed = 2f; //Fade in/out 속도
    public float startDelay = 0.5f; //효과 시작전 딜레이
    Scene_Move scene_Move;
    public AudioClip clip;
    public TextMeshProUGUI Text; //텍스트 컴포넌트를 담을 변수
    bool SceneChange = false; //현재 씬을 전환중인가? 변수
    public Fade_img fade;
    SoundManager sm;
    void Start()
    {
        scene_Move = GetComponent<Scene_Move>();
        StartCoroutine(FadeInOut()); //코루틴 시작
    }

    void Update()
    {
        if (Input.anyKeyDown && !SceneChange) //아무키나입력 받았을때, 현재 타이틀 화면일때, 씬 전환중이 아닐때
        {
            SceneChange = true; //씬 전환중 (중복으로 호출하는것을 방지)
            fade.CallFadeIn(); //Fade_img 스크립트의 CallFadeIn()함수를 호출함

            FastEffect();

        }
    }
    IEnumerator FadeInOut()
    {
        //딜레이
        yield return new WaitForSeconds(startDelay);


        while (true) //무한루프
        {
            // Fade in
            float t = 0f; //0%
            while (t < 1f) //100%까지
            {
                t += Time.deltaTime * fadeSpeed; //프레임 * 효과속도를 곱해서 % 진행상황에 더합니다
                Color color = Text.color; //현재 이미지의 색 컴포넌트를 가져옴

                //이미지의 투명도a alpha 값을 선형보간 (1차함수의 시작값, 끝값, 얻고싶은값) 매개변수로 진행상황을 넣어 알파값을 계산함
                color.a = Mathf.Lerp(0f, 1f, t);


                Text.color = color; // 적용
                yield return null; //이게 없으면 순식간에 효과가 지나감
            }

            //딜레이
            yield return new WaitForSeconds(startDelay);

            // Fade out
            t = 0f; //0%
            while (t < 1f) //100%까지
            {
                t += Time.deltaTime * fadeSpeed; 
                Color color = Text.color;
                color.a = Mathf.Lerp(1f, 0f, t); //하향 1차 그래프
                Text.color = color;
                yield return null;
            }

            //딜레이
            yield return new WaitForSeconds(startDelay);
        }
    }

    public void FastEffect() //이펙트 속도를 빠르게 하려고 만듬
    {
        scene_Move.Wait_And_SceneLoader("Main_Scene");
        sm = SoundManager.instance;
        sm.SFXPlay("PTB", clip);
        fadeSpeed = 6f;
        startDelay = 0.05f;
    }
}
