using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSkip : MonoBehaviour
{
    [SerializeField] GameObject enterButton;

    private bool isSkip = false;

    void Update()
    {
        Invoke("ShowSkipButton", 5f);
        GoSkip();       // Enter�� ���ȴ��� Ȯ���ϴ� �Լ� (���ȴٸ� isSkip ��)
        if (isSkip) 
        {
            SceneManager.LoadScene("Title_Scene");      // Ÿ��Ʋ ȭ������
        }

    }

    public void ShowSkipButton()
    {
        enterButton.SetActive(true);   
    }

    public void GoSkip()        // isSkip ���� ������ ����� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isSkip = true;
        }
    }
}
