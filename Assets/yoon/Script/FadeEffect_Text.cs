using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //TextMeshProUGUI ����Ϸ� ����


public class FadeEffect_Text : MonoBehaviour
{
    public float fadeSpeed = 2f; //Fade in/out �ӵ�
    public float startDelay = 0.5f; //ȿ�� ������ ������

    public TextMeshProUGUI Text; //�ؽ�Ʈ ������Ʈ�� ���� ����

    void Start()
    {
        StartCoroutine(FadeInOut()); //�ڷ�ƾ ����
    }

    IEnumerator FadeInOut()
    {
        //������
        yield return new WaitForSeconds(startDelay);


        while (true) //���ѷ���
        {
            // Fade in
            float t = 0f; //0%
            while (t < 1f) //100%����
            {
                t += Time.deltaTime * fadeSpeed; //������ * ȿ���ӵ��� ���ؼ� % �����Ȳ�� ���մϴ�
                Color color = Text.color; //���� �̹����� �� ������Ʈ�� ������

                //�̹����� ����a alpha ���� �������� (1���Լ��� ���۰�, ����, ��������) �Ű������� �����Ȳ�� �־� ���İ��� �����
                color.a = Mathf.Lerp(0f, 1f, t);


                Text.color = color; // ����
                yield return null; //�̰� ������ ���İ��� ȿ���� ������
            }

            //������
            yield return new WaitForSeconds(startDelay);

            // Fade out
            t = 0f; //0%
            while (t < 1f) //100%����
            {
                t += Time.deltaTime * fadeSpeed; 
                Color color = Text.color;
                color.a = Mathf.Lerp(1f, 0f, t); //���� 1�� �׷���
                Text.color = color;
                yield return null;
            }

            //������
            yield return new WaitForSeconds(startDelay);
        }
    }

    public void FastEffect() //����Ʈ �ӵ��� ������ �Ϸ��� ����
    {
        fadeSpeed = 6f;
        startDelay = 0.05f;
    }
}
