using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public GameObject skipButton;   // ��ŵ �̹����� �ؽ�Ʈ�� ����ִ� ������Ʈ
    [SerializeField] private GameObject playerUI;   // �÷��̾� UI
    public GameObject credit;   // ������ ũ����
    public Image Panel;     // ���� �ڽ�

    public AudioSource audioSource;

    public bool isEnding = false;       // �������� Ȯ���ϴ� ����
    public bool isSkip = false;   // ��ŵ
    public bool isShow = false;
    public bool volumDown = false;  // ���� ���̱�
    float time = 0f;
    float vtime = 0f;
    float F_time = 5f;
    float V_time = 150f;
    float currentVolume;        // ���� �� �̵���ų ����

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeOut());  // ȭ�� ���̵� �ƿ� 
            credit.SetActive(true);     // ���� ũ���� ������Ʈ Ȱ��ȭ
            playerUI.SetActive(false);  // �÷��̾� UI ����
            StartCoroutine(GoTitleSceneVolume());   // ũ���� ������ 2�� �� ���� ���̴� �ڷ�ƾ
            StartCoroutine(GoTitleScene());     // ũ���� ������Ÿ��Ʋ ȭ������ �̵��ϴ� �ڷ�ƾ

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
                controlVolum();
                Invoke("SkipTitleScene", 1.5f);
            }
            if (volumDown)
            {
                controlVolum();
            }
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
    IEnumerator GoTitleSceneVolume()
    {
        Debug.Log("58�� ��");
        yield return new WaitForSeconds(58f);
        Debug.Log("58�� ��");
        controlVolum();
    }
    IEnumerator GoTitleScene()      // 60�� �� �ΰ� ȭ������ �̵�
    {
        Debug.Log("60�� ��");
        yield return new WaitForSeconds(60f);
        Debug.Log("60�� ��");
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

    IEnumerator controlVolum()
    {
        vtime += Time.deltaTime / V_time;
        while (currentVolume < 1)
        {
            currentVolume = Mathf.Lerp(audioSource.volume, 0, vtime);  // Time.deltaTIme�� ���� �� �߰��ؼ� ���� ���� ������ ��.
            audioSource.volume = currentVolume;
            return null;
        }
        return null;
    }
}
