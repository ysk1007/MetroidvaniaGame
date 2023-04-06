using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� �̵� ����Ҷ� ����
public class Scene_Move : MonoBehaviour
{
    string currentSceneName; //���� ���̸��� �����ϴ� ����
    bool currentSceneTitle = false; //���� ���̸��� Ÿ��Ʋ���� �Ǵ��ϴ� ����
    bool SceneChange = false; //���� ���� ��ȯ���ΰ�? ����

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name; //���� �� �̸��� �ҷ���
        if (currentSceneName == "Title_Scene") currentSceneTitle = true; //�� �̸��� Ÿ��Ʋ ȭ���̸� ����
    }

    void Update()
    {
        if (Input.anyKeyDown && currentSceneTitle && !SceneChange) //�ƹ�Ű���Է� �޾�����, ���� Ÿ��Ʋ ȭ���϶�, �� ��ȯ���� �ƴҶ�
        {
            SceneChange = true; //�� ��ȯ�� (�ߺ����� ȣ���ϴ°��� ����)
            Fade_img fade = gameObject.GetComponent<Fade_img>(); //�ڵ尡 ��� ������Ʈ�� Fade_img ��ũ��Ʈ�� ����
            fade.CallFadeIn(); //Fade_img ��ũ��Ʈ�� CallFadeIn()�Լ��� ȣ����

            FadeEffect_Text fade_text = gameObject.GetComponent<FadeEffect_Text>(); //�ڵ尡 ��� ������Ʈ�� FadeEffect_Text ��ũ��Ʈ�� ����
            fade_text.FastEffect(); //fade_text ��ũ��Ʈ�� FastEffect()�Լ��� ȣ����

            Wait_And_SceneLoader("Main_Scene"); //��ٷȴ� �� ��ȯ
        }
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
