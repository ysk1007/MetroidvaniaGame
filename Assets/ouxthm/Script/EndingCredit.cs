using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCredit : MonoBehaviour
{

    public GameObject credit;   // 실질적 크레딧
    public Image Panel;     // 검은 박스

    public bool isEnding;       // 엔딩인지 확인하는 변수
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
