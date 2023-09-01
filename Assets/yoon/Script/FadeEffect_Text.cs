using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //TextMeshProUGUI ����Ϸ� ����


public class FadeEffect_Text : MonoBehaviour
{
    public float fadeSpeed = 2f; //Fade in/out �ӵ�
    public float startDelay = 0.5f; //ȿ�� ������ ������
    Scene_Move scene_Move;
    public AudioClip clip;
    public TextMeshProUGUI Text; //�ؽ�Ʈ ������Ʈ�� ���� ����
    bool SceneChange = false; //���� ���� ��ȯ���ΰ�? ����
    public Fade_img fade;
    SoundManager sm;
    void Start()
    {
        scene_Move = GetComponent<Scene_Move>();
        StartCoroutine(FadeInOut()); //�ڷ�ƾ ����
    }

    void Update()
    {
        if (Input.anyKeyDown && !SceneChange) //�ƹ�Ű���Է� �޾�����, ���� Ÿ��Ʋ ȭ���϶�, �� ��ȯ���� �ƴҶ�
        {
            SceneChange = true; //�� ��ȯ�� (�ߺ����� ȣ���ϴ°��� ����)
            fade.CallFadeIn(); //Fade_img ��ũ��Ʈ�� CallFadeIn()�Լ��� ȣ����

            FastEffect();

        }
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
        scene_Move.Wait_And_SceneLoader("Main_Scene");
        sm = SoundManager.instance;
        sm.SFXPlay("PTB", clip);
        fadeSpeed = 6f;
        startDelay = 0.05f;
    }
}
