using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� �̵� ����Ҷ� ����
public class Scene_Move : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void SceneLoader(string sceneName) // �� �̵� �Լ� (�Ű������� �� �̸�)
    {
        SceneManager.LoadScene(sceneName); // ���� �ҷ��ɴϴ�.
    }

    public void Wait_And_SceneLoader(string sceneName) // ��ٷȴ� �� �̵� �Լ� (�Ű������� �� �̸�)
    {
        StartCoroutine(Wait(sceneName)); //Wait�ڷ�ƾ ����
        
    }

    IEnumerator Wait(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName); // ���� �ҷ��ɴϴ�.
    }


}
