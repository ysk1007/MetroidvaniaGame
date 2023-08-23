using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform[] Boss;  // 보스오브젝트 (2023-08-14추가)
    [SerializeField] private GameObject[] bossName;  // 보스오브젝트 (2023-08-14추가)
    [SerializeField] private GameObject UI;     // InGameUI 

    public bool startFightBoss = false;   // 보스전 시작시 true 되었다가 false로 변환
    public int stage;   // 추후 추가되는 MapManager에서 변수 가져오면 됨.

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
        height = Camera.main.orthographicSize;  // 카메라의 월드 공간에서의 세로 절반 사이즈
        width = height * Screen.width / Screen.height; // 카메라의 월드 공간에서의 가로 절반 사이즈
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
    void bossNameOn()   // 보스 이름 텍스트 띄우는 함수
    {
        bossName[stage].gameObject.SetActive(true);
    }
    void bossNameOff()  // 보스 이름 텍스트 내리는 함수
    {
        bossName[stage].gameObject.SetActive(false);
        UI.SetActive(true); // InGameUI On
    }

    void SituationTerminated()  // 보스시작 종료
    {
        startFightBoss = false;
    }

    void WatchingPlayer()   // 플레이어 비추는 카메라 함수
    {
        //Camera.main.orthographicSize = 9f;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);    //value 값이 min 과 max 사이면 value값을 반환, min보다 작으면 min, max보다 크면 max값을 반환

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void WatchingBoss()     // 보스 비추는 카메라 함수
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
