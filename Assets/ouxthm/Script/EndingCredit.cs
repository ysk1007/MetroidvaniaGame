using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public GameObject skipButton;   // ��ŵ �̹����� �ؽ�Ʈ�� ����ִ� ������Ʈ
    public GameObject credit;   // ������ ũ����
    public Image Panel;     // ���� �ڽ�

    public bool isEnding = false;       // �������� Ȯ���ϴ� ����
    public bool isSkip = false;   // ��ŵ
    public bool isShow = false;
    float time = 0f;
    float F_time = 5f;

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeOut());  // ȭ�� ���̵� �ƿ� 
            credit.SetActive(true);     // ���� ũ���� ������Ʈ Ȱ��ȭ
            StartCoroutine(GoTitleScene());     // Ÿ��Ʋ ȭ������ �̵�
            if (!isShow)
            {
                Invoke("ShowSkipButton", 15f);      // ��ŵ ��ư ������Ʈ Ȱ��ȭ 15�� ��
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
    IEnumerator FadeOut()       // ���̵� �ƿ� Ȱ��ȭ
    {
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }
    IEnumerator GoTitleScene()      // 60�� �� Ÿ��Ʋ ȭ������ �̵�
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("Title_Scene");
    }
    public void SkipTitleScene()        // ��� Ÿ��Ʋ ȭ������ �̵�
    {
        time = 0f;
        SceneManager.LoadScene("Title_Scene");
    }

    public void ShowSkipButton()        // ��ŵ ��ư Ȱ��ȭ ������
    {
        skipButton.SetActive(true);
        isShow = true;
    }
    public void GoSkip()        // isSkip ���� ������ ����� �Լ�
    {
        if(isShow && Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;
        }
    }
}