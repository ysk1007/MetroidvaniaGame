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
    float F_time = 30f;

    void Update()
    {
        if (isEnding)
        {
            FadeFlow();
            credit.SetActive(true);
            StartCoroutine(GoTitleScene());
            if (!isShow)
            {
                Invoke("ShowSkipButton", 10f);
            }
            else if (isShow)
            {
                GoSkip();
            }

            if (isSkip)
            {
                credit.SetActive(false);
                SkipTitleScene();
            }

        }
    }
    void FadeFlow()
    {
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
        }
    }
    IEnumerator GoTitleScene()
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("Title_Scene");
    }
    public void SkipTitleScene()
    {
        SceneManager.LoadScene("Title_Scene");
    }

    public void ShowSkipButton()
    {
        skipButton.SetActive(true);
        isShow = true;
    }
    public void GoSkip()
    {
        if(isShow && Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;
        }
    }
}
