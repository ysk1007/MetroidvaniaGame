using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera instance;
    [SerializeField] private GameObject[] bossName;  // ����������Ʈ (2023-08-14�߰�)
    [SerializeField] private GameObject UI;     // InGameUI 
    public MapManager map;
    public Player player;
    public Orc_Boss orcboss;
    public bool startFightBoss = false;   // ������ ���۽� true �Ǿ��ٰ� false�� ��ȯ
    public int stage;   // ���� �߰��Ǵ� MapManager���� ���� �������� ��.  ū ��������
    public int stageSmall;  // ���� ��������
    public bool watchBossname = false;  // ���� �̸� ��� �� �ô��� Ȯ���ϴ� ����

    public float changeX;
    public float changeY;

    public Transform target;
    public float speed;

    public Vector2 center;
    public Vector2 size;
    float height;
    float width;
    public float screenSize;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = Player.instance.GetComponent<Player>();
        if (Orc_Boss.instance != null)
        {
            orcboss = Orc_Boss.instance.GetComponent<Orc_Boss>();
        }
        map = MapManager.instance;
        height = Camera.main.orthographicSize;  // ī�޶��� ���� ���������� ���� ���� ������
        width = height * Screen.width / Screen.height; // ī�޶��� ���� ���������� ���� ���� ������
    }

    void LateUpdate()
    {
        stage = map.CurrentStage[0];
        stageSmall = map.CurrentStage[1];

        if (stageSmall == 7 && !startFightBoss && !watchBossname)
        {
            Invoke("OnStartBool", 1f);
        }
        else if (stageSmall != 7)    // ���� ���������� 7 �ƴ� ��
        {
            startFightBoss = false;
            watchBossname = false;
        }

        if (startFightBoss)
        {
            WatchingBoss();
        }

        if (startFightBoss && !watchBossname)
        {
            OnOffText();
        }
        else if (!startFightBoss)
        {
            WatchingPlayer();
            CameraLimit();
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
        UI.SetActive(true); // �÷��̾� UI ON
    }
    void OnOffText()    // ���� �ؽ�Ʈ ����� ������ �Լ��� �����ϴ� �Լ�
    {
        Invoke("bossNameOn", 1.5f);
        Invoke("bossNameOff", 3f);
        Invoke("SituationTerminated", 3.5f);
        watchBossname = true;
    }
    void OnStartBool()
    {
        startFightBoss = true;
    }
    void WatchingPlayer()   // �÷��̾� ���ߴ� ī�޶� �Լ�
    {
        if (player.UseMirror)
        {
            Camera.main.orthographicSize = 7f;
        }
        else if (!player.UseMirror)
        {
            Camera.main.orthographicSize = 9f;
        }
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
    }
    void CameraLimit()
    {
        if (player.UseMirror)   //�ſ� ������ �������� ��
        {
            if (stage == 2 && stageSmall != 7) // 3��������
            {
                if (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8)    // ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 15;
                    center.y = 6;
                    size.x = 60;
                    size.y = 22;
                }
                else
                {
                    center.x = 120;
                    center.y = 7;
                    size.x = 270;
                    size.y = 40;
                }
                
            }
            else if (stage == 2 && stageSmall == 7)   // 3�������� ���� 
            {
                center.x = 25;
                center.y = 12;
                size.x = 75;
                size.y = 35;
            }
            if (stage < 2 && stageSmall != 7) // 1,2��������
            {
                if(stage == 0 && (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8))    // 1�������� ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 20;
                    center.y = 8;
                    size.x = 70;
                    size.y = 25;
                }
                else if (stage == 1 && (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8))    // 2�������� ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 20;
                    center.y = 8;
                    size.x = 58;
                    size.y = 25;
                }
                else
                {
                    center.x = 120;
                    center.y = 7;
                    size.x = 270;
                    size.y = 25;
                }
            }
            else if (stage == 0 && stageSmall == 7)   // 1�������� ����
            {
                center.x = 480;
                center.y = 3;
                size.x = 1000;
                size.y = 20;
            }
            else if (stage == 1 && stageSmall == 7)   // 2�������� ����
            {
                center.x = 20;
                center.y = 5;
                size.x = 58;
                size.y = 25;
            }
        }
        else if (!player.UseMirror) //�ſ� ������ �������� �ƴ� ��
        {
            if (stage == 2 && stageSmall != 7) // 3��������
            {
                if (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8)   // ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 15;
                    center.y = 6;
                    size.x = 60;
                    size.y = 20;
                }
                else
                {
                    center.x = 120;
                    center.y = 7;
                    size.x = 265;
                    size.y = 35;
                }
                
            }
            else if (stage == 2 && stageSmall == 7)   // 3�������� ����
            {
                center.x = 25;
                center.y = 12;
                size.x = 70;
                size.y = 30;
            }
            if (stage < 2 && stageSmall != 7) // 1,2��������
            {
                if (stage == 0 && (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8))   // 1�������� ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 20;
                    center.y = 7;
                    size.x = 65;
                    size.y = 20;
                }
                else if (stage == 1 && (stageSmall == 1 || stageSmall == 3 || stageSmall == 6 || stageSmall == 8))   // 2�������� ���� �� ���� ���������� �� ī�޶� ����
                {
                    center.x = 20;
                    center.y = 7;
                    size.x = 50;
                    size.y = 20;
                }
                else
                {
                    center.x = 120;
                    center.y = 7;
                    size.x = 265;
                    size.y = 20;
                }
            }
            else if (stage == 1 && stageSmall == 7)    // 2�������� ����
            {
                center.x = 20;
                center.y = 6;
                size.x = 50;
                size.y = 20;
            }
            else if (stage == 0 && stageSmall == 7) // 1�������� ����
            {
                center.x = 15 + orcboss.transform.position.x;
                center.y = 3;
                size.x = 40;
                size.y = 20;
            }
        }
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);    //value ���� min �� max ���̸� value���� ��ȯ, min���� ������ min, max���� ũ�� max���� ��ȯ

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);

    }

    void WatchingBoss()     // ���� ���ߴ� ī�޶� �Լ� 2023-09-01 ����
    {
        UI.SetActive(false);    // InGameUI Off

        switch (stage)
        {
            case 0: // Stage 1
                Camera.main.orthographicSize = 5f;
                changeX = 7f;
                changeY = 4f;
                break;
            case 1: // Stage 2
                Camera.main.orthographicSize = 3f;
                changeX = 16f;
                changeY = 1f;
                break;
            case 2: // Stage 3
                Camera.main.orthographicSize = 8f;
                changeX = 25f;
                changeY = 10f;
                break;
        }
        Vector3 vector3 = new Vector3(changeX, changeY, -10f);
        transform.position = Vector3.Lerp(transform.position, vector3, Time.deltaTime * speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
