using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform[] Boss;  // 보스오브젝트 (2023-08-14추가)
    [SerializeField] private GameObject[] bossName;  // 보스오브젝트 (2023-08-14추가)

    [SerializeField] private bool startFightBoss = false;   // 보스전 시작시 true 되었다가 false로 변환
    public int stage;   // 추후 추가되는 MapManager에서 변수 가져오면 됨.

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
