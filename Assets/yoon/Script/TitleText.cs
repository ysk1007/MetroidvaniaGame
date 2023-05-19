using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� �̵� ����Ҷ� ����
using TMPro; //TextMeshProUGUI ����Ϸ� ����

public class TitleText : MonoBehaviour
{
    public float amplitude = 1f;        // ���Ʒ� �������� ũ��
    public float frequency = 2f;        // ���Ʒ� �������� �ӵ�

    string currentSceneName; //���� ���̸��� �����ϴ� ����
    bool currentSceneTitle = false; //���� ���̸��� Ÿ��Ʋ���� �Ǵ��ϴ� ����

    private Vector3 startPosition; //������ġ�� ���� Vector3 ��ġ ����

    void Start()
    {

        startPosition = transform.position; //������ġ�� ������Ʈ ���� ��ġ�� ����
        currentSceneName = SceneManager.GetActiveScene().name; //���� �� �̸��� �ҷ���
        if (currentSceneName == "Title_Scene") currentSceneTitle = true; //�� �̸��� Ÿ��Ʋ ȭ���̸� ����
    }

    void Update()
    {
        if (currentSceneTitle)
        {
        //Sin �׷��� 1, -1 �� �ݺ� �Ѵ�.�̸� �̿��Ͽ� �ݺ� �������� ����

        // �ð��� ���� y�� ���
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude; //yOffset ������ y���� ��´� ������ * ũ�� * ����

        // y���� �����Ͽ� ������Ʈ�� ��ġ ������
        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
        }

    }
}
