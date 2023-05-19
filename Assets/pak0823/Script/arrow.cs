using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public float speed; // 화살 이동 속도
    public float distance; // 화살이 감지하는 거리
    public LayerMask islayer; // 충돌 감지를 할 레이어
    public Transform pos; // 화살 위치 정보
    public bool SetSkill = false; // 스킬 사용 여부
    public int Dmg; //대미지 변수
    public float turnSpeed = 1f; // 화살의 유도 속도
    public float maxTrackingDistance = 10f; // 유도 가능한 최대 거리
    public float maxTrackingAngle = 80f; // 유도 가능한 최대 각도
    public int level;

    private Collider2D coll; // Arrow 오브젝트의 콜라이더
    private Transform target; // 유도 대상
    private bool isTracking; // 화살이 유도 중인지 여부
    private float trackingDistance = 5f; // 유도 가능한 거리
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // 이미 적에게 대미지를 입혔는지 여부를 기록하는 Dictionary 변수

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>(); // Player 스크립트를 가진 게임 오브젝트를 찾아서 할당
        coll = GetComponent<Collider2D>(); // Arrow 오브젝트의 콜라이더를 가져옴
        Invoke("DestroyArrow", 2f); // 일정 시간이 지난 후 화살을 제거하는 Invoke 함수를 호출
    }

    private void Update()
    {
        
        if (player != null && player.isSkill == true)
        {
            SetSkill = true; // 스킬 사용 중이면 SetSkill 변수를 true로 설정
            Dmg = 3;
        }
        else
        {
            SetSkill = false; // 스킬 사용 중이 아니면 SetSkill 변수를 false로 설정
            Dmg = 1;
        }
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.right, distance, islayer); // 화살이 감지할 수 있는 거리 내에서 충돌하는 물체를 감지
        if (rayHit.collider != null)
        {
            if (rayHit.collider.tag == "Enemy")
            {
                if (!hitDict.ContainsKey(rayHit.collider)) // 이미 적에게 대미지를 입힌 경우, Dictionary 체크
                {
                    Debug.Log("Hit!");
                    /*rayHit.collider.GetComponent<Enemy>().EnemyHurt(Dmg, transform.position); // Enemy 스크립트의 EnemyHurt 함수를 호출해 적에게 대미지*/
                    hitDict.Add(rayHit.collider, true); // 적 정보를 Dictionary에 추가
                }

                if (SetSkill == false)
                {
                    DestroyArrow(); // 스킬을 사용하지 않았다면 화살을 제거
                }
            }
        }
        if(SetSkill == true)
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(transform.right * speed * Time.deltaTime); // 화살을 오른쪽으로 이동
            }
            else
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime); // 화살을 왼쪽으로 이동
            }
        } 
        else if (level == 1 && SetSkill == false)
        {
            // 유도 중일 때
            if (isTracking)
            {
                if (target != null)
                {
                    // 적 캐릭터를 향하는 방향 벡터 계산
                    Vector2 direction = target.position - transform.position;
                    direction.Normalize(); // 방향 벡터를 정규화하여 길이가 1인 단위 벡터로 만듦

                    // 화살을 해당 방향으로 이동 및 회전
                    transform.Translate(direction * speed * Time.deltaTime);
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    // 대상과의 거리가 유도 가능한 거리보다 작아지면 유도 중지
                    if (Vector3.Distance(transform.position, target.position) <= trackingDistance)
                    {
                        isTracking = false;
                    }
                }
                else
                {
                    // 대상이 없으면 유도 중지
                    isTracking = false;
                }
            }
            else
            {
                // 유도 중이 아니면 일반 이동
                if (transform.rotation.y == 0)
                {
                    transform.Translate(transform.right * speed * Time.deltaTime); // 화살을 오른쪽으로 이동
                }
                else
                {
                    transform.Translate(transform.right * -1 * speed * Time.deltaTime); // 화살을 왼쪽으로 이동
                }

                // 유도 대상 탐색
                RaycastHit2D rayHittargetX = Physics2D.Raycast(transform.position, transform.right, maxTrackingDistance, islayer);  //정면에 있는 대상 탐색
                RaycastHit2D rayHittargetU = Physics2D.Raycast(transform.position, Vector2.up, maxTrackingDistance, islayer);   //위쪽에 있는 대상 탐색
                RaycastHit2D rayHittargetD = Physics2D.Raycast(transform.position, Vector2.down, maxTrackingDistance, islayer); //아래쪽에 있는 대상 탐색

                if (rayHittargetX.collider != null && rayHittargetX.collider.tag == "Enemy")
                {
                    // 유도 가능한 최대 각도 안에 있으면 유도 시작
                    Vector3 dir = rayHittargetX.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetX.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
                if (rayHittargetU.collider != null && rayHittargetU.collider.tag == "Enemy")
                {
                    Vector3 dir = rayHittargetU.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetU.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
                if (rayHittargetD.collider != null && rayHittargetD.collider.tag == "Enemy")
                {
                    Vector3 dir = rayHittargetD.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetD.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
            }
        }  
    }
    private void DestroyArrow() //Arrow 오브젝트 제거
    {
        Destroy(gameObject);
    }
}