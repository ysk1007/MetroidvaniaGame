using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCredit : MonoBehaviour
{

    public GameObject credit;   // ������ ũ����
    public Image Panel;     // ���� �ڽ�

    public bool isEnding;       // �������� Ȯ���ϴ� ����
    float time = 0f;
    float F_time = 30f;

    void Update()
    {
        if (isEnding)
        {
            StartCoroutine(FadeFlow());
            credit.SetActive(true);
        }

    }

    IEnumerator FadeFlow()
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
}
