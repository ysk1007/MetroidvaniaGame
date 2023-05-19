using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동 사용할때 참조
using TMPro; //TextMeshProUGUI 사용하려 참조

public class TitleText : MonoBehaviour
{
    public float amplitude = 1f;        // 위아래 움직임의 크기
    public float frequency = 2f;        // 위아래 움직임의 속도

    string currentSceneName; //현재 씬이름을 저장하는 변수
    bool currentSceneTitle = false; //현재 씬이름이 타이틀씬이 판단하는 변수

    private Vector3 startPosition; //시작위치를 담을 Vector3 위치 변수

    void Start()
    {

        startPosition = transform.position; //시작위치는 오브젝트 현재 위치를 담음
        currentSceneName = SceneManager.GetActiveScene().name; //현재 씬 이름을 불러옴
        if (currentSceneName == "Title_Scene") currentSceneTitle = true; //씬 이름이 타이틀 화면이면 감지
    }

    void Update()
    {
        if (currentSceneTitle)
        {
        //Sin 그래프 1, -1 을 반복 한다.이를 이용하여 반복 움직임을 구현

        // 시간에 따른 y값 계산
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude; //yOffset 변수에 y값을 담는다 프레임 * 크기 * 변수

        // y값을 적용하여 오브젝트의 위치 변경함
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
        }

    }
}
