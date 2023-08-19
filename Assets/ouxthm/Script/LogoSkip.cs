using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;

    private bool isSkip = false;        // Enter Ű�� ���ȴ��� Ȯ���ϴ� ����
    private bool isShow = false;        // ShowSkipButton�� Update ������ �� ���� �ҷ��� �� �ֵ��� �ϴ� ����

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
            SceneManager.LoadScene("Title_Scene");      // Ÿ��Ʋ ȭ������
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
}
