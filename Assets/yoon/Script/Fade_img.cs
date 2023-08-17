using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui 이미지를 건드리기위해 참조
using UnityEngine.SceneManagement; // 씬 이동 사용할때 참조

public class Fade_img : MonoBehaviour
{
    public GameObject img_obj; //효과 적용할 오브젝트를 담을 변수
    private Image img;  //이미지 컴포넌트를 담을 이미지 변수

    public float fadeSpeed = 0.5f; //Fade in/out 속도

    string currentSceneName; //현재 씬이름을 저장하는 변수

    // Start is called before the first frame update
    void Start()
    {
        img = img_obj.GetComponent<Image>(); //img 변수에 효과 적용할 오브젝트의 Image 컴포넌트 가져옴
        currentSceneName = SceneManager.GetActiveScene().name; //현재 씬 이름을 불러옴
            CallFadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallFadeIn() // Fade In 효과
    {
        img_obj.SetActive(true); //이미지를 SetActive 합니다 중복 클릭 방지
        StartCoroutine(FadeIn()); //Fade in 코루틴 시작
    }

    public void CallFadeOut() // Fade In 효과
    {
        StartCoroutine(FadeOut()); //Fade in 코루틴 시작
    }

    IEnumerator FadeIn() //Fade in 효과
        {
            // 0.5초 기다렸다 시작
            yield return new WaitForSeconds(0.5f);

            // Fade in
            float t = 0f; //0%
            while (t < 1f) //100%까지
            {
                t += Time.deltaTime * fadeSpeed; //프레임 * 효과속도를 곱해서 % 진행상황에 더합니다
                Color color = img.color; //현재 이미지의 색 컴포넌트를 가져옴

                //이미지의 투명도a alpha 값을 선형보간 (1차함수의 시작값, 끝값, 얻고싶은값) 매개변수로 진행상황을 넣어 알파값을 계산함
                color.a = Mathf.Lerp(0f, 1f, t); 
            

                img.color = color; // 적용
                yield return null; //이게 없으면 순식간에 효과가 지나감
            }
        }

    IEnumerator FadeOut() //Fade Out 효과
    {
        // 0.5초 기다렸다 시작
        yield return new WaitForSeconds(0.5f);

        // Fade Out
        float t = 0f; //0%
        while (t < 1f) //100%까지
        {
            t += Time.deltaTime * fadeSpeed; //프레임 * 효과속도를 곱해서 % 진행상황에 더합니다
            Color color = img.color; //현재 이미지의 색 컴포넌트를 가져옴

            //이미지의 투명도a alpha 값을 선형보간 (1차함수의 시작값, 끝값, 얻고싶은값) 매개변수로 진행상황을 넣어 알파값을 계산함
            color.a = Mathf.Lerp(1f, 0f, t);


            img.color = color; // 적용
            yield return null; //이게 없으면 순식간에 효과가 지나감
        }
        img_obj.SetActive(false);
    }
}
