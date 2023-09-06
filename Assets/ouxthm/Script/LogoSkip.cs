using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;
    [SerializeField] private Image fadeOutScreen;

    private bool isSkip = false;        // Enter Ű�� ���ȴ��� Ȯ���ϴ� ����
    private bool isShow = false;        // ShowSkipButton�� Update ������ �� ���� �ҷ��� �� �ֵ��� �ϴ� ����
    private float etime = 0f;       // blackScreen�� ���
    private float E_Time = 300f;    // blackScreen�� ���
    public bool story = false;

    void Update()
    {
        if (!isShow)
        {
            Invoke("ShowSkipButton", 6f);       // 6�� �� ��ŵ �̹��� Ȱ��ȭ 
        }        
        else if (isShow)
        {
            GoSkip();       // Enter�� ���ȴ��� Ȯ���ϴ� �Լ� (���ȴٸ� isSkip ��)
        }
        if (isSkip) 
        {
            StartCoroutine(Logo_FadeOut());
            if (story)
            {
                Invoke("GO_Ingame_Scene", 4f);      // �ΰ��� ������ �̵�
            }
            /*else
            {
                Invoke("GO_Title_Scene", 1.5f);      // Ÿ��Ʋ ������ �̵�
            }*/
        }
    }

    public void ShowSkipButton()        // ��ŵ�� �����ϴٴ� �̹����� �����ִ� �Լ�
    {
        enterButton.SetActive(true);   // ��ŵ �̹��� Ȱ��ȭ
        isShow = true;
    }

    public void GoSkip()        // isSkip ���� ������ ����� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;      

        }
    }
    IEnumerator Logo_FadeOut()       // ���� ũ���� ���̵� �ƿ� Ȱ��ȭ
    {
        Color blackalpha = fadeOutScreen.color;
        while (blackalpha.a < 1f)
        {
            etime += Time.deltaTime / E_Time;
            blackalpha.a = Mathf.Lerp(0, 1, etime);       // �̹��� ���İ� 0���� 1���� ������ ����
            fadeOutScreen.color = blackalpha;
            yield return null;
        }
        yield return null;
    }
    void GO_Ingame_Scene()  // �ΰ��� ������ �̵��ϴ� �Լ�
    {
        SceneManager.LoadScene("ingame_scene");
    }

    void GO_Title_Scene()
    {
        SceneManager.LoadScene("Title_Scene");
    }
}
