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

    public AudioSource audioSource;
    //public AudioClip endingBGM;     // ����ũ���� �������

    public bool isEnding = false;       // �������� Ȯ���ϴ� ����
    public bool isSkip = false;   // ��ŵ
    public bool isShow = false;
    public bool volumDown = false;  // ���� ���̱�
    float time = 0f;
    float F_time = 5f;
    float V_time = 10f;

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
            if (volumDown)
            {
                controlVolum();
            }
            voluemDD();
        }
    }
    IEnumerator FadeOut()       // ���̵� �ƿ� Ȱ��ȭ
    {
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);       // �̹��� ���İ� 0���� 1���� ������ ����
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
    }
    IEnumerator GoTitleScene()      // 60�� �� �ΰ� ȭ������ �̵�
    {
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("Logo_Scene");      
    }
    public void SkipTitleScene()        // ��� �ΰ� ȭ������ �̵�
    {
        time = 0f;
        SceneManager.LoadScene("Logo_Scene");
    }

    public void ShowSkipButton()        // ��ŵ ��ư Ȱ��ȭ �Լ�
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

    void controlVolum()
    {
        
        while(audioSource.volume > 0.2f)
        {
            time += Time.deltaTime / V_time;
            audioSource.volume = Mathf.Lerp(1, 0, 0.3f);  // Time.deltaTIme�� ���� �� �߰��ؼ� ���� ���� ������ ��.
        }
    }
    void voluemDD()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            volumDown = true;
        }
    }
}
