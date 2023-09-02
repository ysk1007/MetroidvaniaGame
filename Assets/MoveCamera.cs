using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera instance;
    [SerializeField] private GameObject[] bossName;  // 보스오브젝트 (2023-08-14추가)
    [SerializeField] private GameObject UI;     // InGameUI 
    public MapManager map;
    public Player player;
    public bool startFightBoss = false;   // 보스전 시작시 true 되었다가 false로 변환
    public int stage;   // 추후 추가되는 MapManager에서 변수 가져오면 됨.  큰 스테이지
    public int stageSmall;  // 작은 스테이지
    public bool watchBossname = false;  // 보스 이름 띄운 걸 봤는지 확인하는 변수

    float changeX;
    float changeY;

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
        map = MapManager.instance.GetComponent<MapManager>();
        height = Camera.main.orthographicSize;  // 카메라의 월드 공간에서의 세로 절반 사이즈
        width = height * Screen.width / Screen.height; // 카메라의 월드 공간에서의 가로 절반 사이즈
    }

    void LateUpdate()
    {
        stage = map.CurrentStage[0];
        stageSmall = map.CurrentStage[1];

        if(stageSmall == 7 && !startFightBoss && !watchBossname)
        {
            Invoke("OnStartBool", 1f);
        }
        else if(stageSmall != 7)    // 작은 스테이지가 7 아닐 때
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
    void bossNameOn()   // 보스 이름 텍스트 띄우는 함수
    {
        bossName[stage].gameObject.SetActive(true);
    }
    void bossNameOff()  // 보스 이름 텍스트 내리는 함수
    {
        bossName[stage].gameObject.SetActive(false);
    }
    void SituationTerminated()  // 보스시작 종료
    {
        startFightBoss = false;
        UI.SetActive(true); // 플레이어 UI ON
    }
    void OnOffText()    // 보스 텍스트 띄웠다 내리는 함수를 실행하는 함수
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
    void WatchingPlayer()   // 플레이어 비추는 카메라 함수
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
        if (player.UseMirror)   //거울 아이템 장착중일 때
        {
            if (stage == 2 && stageSmall <= 6) // 3스테이지
            {
                center.x = 120;
                center.y = 7;
                size.x = 270;
                size.y = 40;
            }
            else if (stage == 2 && stageSmall > 6)   // 3스테이지 보스 
            {
                center.x = 25;
                center.y = 12;
                size.x = 75;
                size.y = 35;
            }
            if ((stage == 0 || stage == 1) && stageSmall <= 6) // 1,2스테이지
            {
                center.x = 120;
                center.y = 7;
                size.x = 270;
                size.y = 25;
            }
            else if (stage < 2 && stageSmall > 6)   // 1,2스테이지 보스
            {
                center.x = 20;
                center.y = 4;
                size.x = 55;
                size.y = 20;
            }
        }
        else if (!player.UseMirror) //거울 아이템 장착중이 아닐 때
        {
            if (stage == 2 && stageSmall <= 6) // 3스테이지
            {
                center.x = 120;
                center.y = 7;
                size.x = 265;
                size.y = 35;
            }
            else if(stage == 2 && stageSmall > 6)   // 3스테이지 보스
            {
                center.x = 25;
                center.y = 12;
                size.x = 70;
                size.y = 30;
            }
            if (stage < 2 && stageSmall <= 6) // 1,2스테이지
            {
                center.x = 120;
                center.y = 7;
                size.x = 265;
                size.y = 20;
            }
            else if(stage == 1 && stageSmall > 6)    // 2스테이지 보스
            {
                center.x = 20;
                center.y = 6;
                size.x = 50;
                size.y = 20;
            }
            else if(stage == 0 && stageSmall > 6) // 1스테이지 보스
            {
                center.x = 480;
                center.y = 3;
                size.x = 1000;
                size.y = 20;
            }
        }
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);    //value 값이 min 과 max 사이면 value값을 반환, min보다 작으면 min, max보다 크면 max값을 반환

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void WatchingBoss()     // 보스 비추는 카메라 함수 2023-09-01 수정
    {
        UI.SetActive(false);    // InGameUI Off

        switch (stage)
        {
            case 0: // Stage 1
                Camera.main.orthographicSize = 5f;
                changeX = 19f;
                changeY = 0f;
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
