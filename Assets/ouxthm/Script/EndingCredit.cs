using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] private GameObject skipButton;   // ��ŵ �̹����� �ؽ�Ʈ�� ����ִ� ������Ʈ
    [SerializeField] private GameObject playerUI;   // �÷��̾� UI
    [SerializeField] private GameObject credit;   // ������ ũ����
    [SerializeField] private Image EndingUI;     // ���� �ڽ�
    [SerializeField] private Image balckScreen;

    [SerializeField] private AudioSource audioSource;

    public bool isEnding = false;       // �������� Ȯ���ϴ� ����
    private bool isSkip = false;   // ��ŵ
    private bool isShow = false;
    private bool volumDown = false;  // ���� ���̱�
    private float time = 0f;        // �� ó�� ���̵� �ƿ��� ���
    private float vtime = 0f;       // ���� ������ ���
    private float etime = 0f;       // blackScreen�� ���
    private float F_time = 200f;    // �� ó�� ���̵� �ƿ��� ���
    private float V_time = 150f;    // ���� ������ ���
    private float E_Time = 200f;    // blackScreen�� ���
    private float currentVolume;        // ���� �� �̵���ų ����

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeOut());  // ȭ�� ���̵� �ƿ� 
            credit.SetActive(true);     // ���� ũ���� ������Ʈ Ȱ��ȭ
            playerUI.SetActive(false);  // �÷��̾� UI ����
            StartCoroutine(GoTitleSceneFadeOut());
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
                StartCoroutine(EndingCreditFadeOut());
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
        Color alpha = EndingUI.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);       // �̹��� ���İ� 0���� 1���� ������ ����
            EndingUI.color = alpha;
            yield return null;
        }
        yield return null;
    }
    IEnumerator EndingCreditFadeOut()       // ���� ũ���� ���̵� �ƿ� Ȱ��ȭ
    {
        Color blackalpha = balckScreen.color;
        while (blackalpha.a < 1f)
        {
            etime += Time.deltaTime / E_Time;
            blackalpha.a = Mathf.Lerp(0, 1, etime);       // �̹��� ���İ� 0���� 1���� ������ ����
            balckScreen.color = blackalpha;
            yield return null;
        }
        yield return null;
    }

    IEnumerator GoTitleSceneFadeOut()       // 58�� �� ȭ�� ���̵� �ƿ�
    {
        yield return new WaitForSeconds(58f);
        StartCoroutine(EndingCreditFadeOut());
    }
    IEnumerator GoTitleSceneVolume()        // 58�� �� ���� ������ ���̱�
    {
        yield return new WaitForSeconds(58f);
        controlVolum();
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

    IEnumerator controlVolum()  // ���� ���̴� �ڷ�ƾ
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
