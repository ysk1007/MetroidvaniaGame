using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform[] Boss;  // ����������Ʈ (2023-08-14�߰�)
    [SerializeField] private GameObject[] bossName;  // ����������Ʈ (2023-08-14�߰�)

    [SerializeField] private bool startFightBoss = false;   // ������ ���۽� true �Ǿ��ٰ� false�� ��ȯ
    public int stage;   // ���� �߰��Ǵ� MapManager���� ���� �������� ��.

    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        if (startFightBoss)
        {
            Invoke("bossNameOn", 1.5f);
            WathcingBoss();
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
    }

    void SituationTerminated()  // �������� ����
    {
        startFightBoss = false;
    }

    void WatchingPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void WathcingBoss()
    {
        transform.position = Vector3.Lerp(transform.position, Boss[stage].position, Time.deltaTime * speed);

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
