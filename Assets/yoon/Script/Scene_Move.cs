using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동 사용할때 참조
public class Scene_Move : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

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
