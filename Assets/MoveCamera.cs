using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform[] Boss;  // ����������Ʈ (2023-08-14�߰�)
    [SerializeField] private GameObject[] bossName;  // ����������Ʈ (2023-08-14�߰�)
    [SerializeField] private GameObject UI;     // InGameUI 

    public bool startFightBoss = false;   // ������ ���۽� true �Ǿ��ٰ� false�� ��ȯ
    public int stage;   // ���� �߰��Ǵ� MapManager���� ���� �������� ��.

    float changeX;
    float changeY;

    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;
    public float screenSize;

    void Start()
    {
        height = Camera.main.orthographicSize;  // ī�޶��� ���� ���������� ���� ���� ������
        width = height * Screen.width / Screen.height; // ī�޶��� ���� ���������� ���� ���� ������
    }

    void LateUpdate()
    {
        if (startFightBoss)
        {
            Invoke("bossNameOn", 1.5f);
            WatchingBoss();
            Invoke("bossNameOff", 3f);
            Invoke("SituationTerminated", 3.5f);
        }
        else if (!startFightBoss)
        {
            WatchingPlayer();
        }
    }
    void bossNameOn()   // ���� �̸� �ؽ�Ʈ ���� �Լ�
    {
        bossName[stage].gameObject.SetActive(true);
    }
    void bossNameOff()  // ���� �̸� �ؽ�Ʈ ������ �Լ�
    {
        bossName[stage].gameObject.SetActive(false);
        UI.SetActive(true); // InGameUI On
    }

    void SituationTerminated()  // �������� ����
    {
        startFightBoss = false;
    }

    void WatchingPlayer()   // �÷��̾� ���ߴ� ī�޶� �Լ�
    {
        //Camera.main.orthographicSize = 9f;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);    //value ���� min �� max ���̸� value���� ��ȯ, min���� ������ min, max���� ũ�� max���� ��ȯ

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void WatchingBoss()     // ���� ���ߴ� ī�޶� �Լ�
    {
        UI.SetActive(false);    // InGameUI Off

        switch (stage)
        {
            case 0:
                Camera.main.orthographicSize = 5f;
                changeX = 4f;
                changeY = 5.15f;
                break;
            case 1:
                Camera.main.orthographicSize = 3f;
                changeX = 0f;
                changeY = -6f;
                break;
        }

        Vector3 vector3 = new Vector3(changeX, changeY);
        transform.position = Vector3.Lerp(transform.position, Boss[stage].position + vector3, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
