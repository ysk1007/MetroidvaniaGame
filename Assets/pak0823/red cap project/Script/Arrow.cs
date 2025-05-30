using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public Enemy enemy;
    public LayerMask islayer; // 충돌 감지를 할 레이어
    public Transform pos; // 화살 위치 정보
    public float Dmg = 1; //대미지 변수, 몬스터가 피격시 화살 데미지값을 받기 위해
    public float SkillDmg = 2;
    public float speed = 20f; // 화살 이동 속도
    public bool isSkill = false; // 스킬 사용 여부
    public BoxCollider2D box;   // 화살 박스 콜라이더
    private Vector3 moveDirection = Vector3.right; // 화살이 나가는 방향
    private float detectRadius = 2.5f; // 화살이 감지할 수 있는 반경 (적이 있는지 없는지 확인)
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D collider;
    public bool hit = false;    // 적을 맞췄는지 확인하는 변수
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // 이미 적에게 대미지를 입혔는지 여부를 기록하는 Dictionary 변수

    private void Awake()
    {
        player = Player.instance.GetComponent<Player>();
        Dmg = (player.ATP + player.AtkPower + player.GridPower + player.VulcanPower) * player.WeaponsDmg[2];
        SkillDmg = (player.ATP + player.AtkPower + player.GridPower + player.VulcanPower) * 2.5f;
        if (player.isSkill == true)
        {
            isSkill = true; // 스킬 사용 중이면 SetSkill 변수를 true로 설정
        }
        else
        {
            isSkill = false; // 스킬 사용 중이 아니면 SetSkill 변수를 false로 설정
        }
    }

    private void Start()
    {
        Invoke("DestroyArrow", player.ArrowDistance); // 일정 시간이 지난 후 화살을 제거하는 Invoke 함수를 호출
        pos = transform;
        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // 플레이어가 오른쪽을 바라보면 화살을 오른쪽으로 발사
                moveDirection = Vector3.right;
                spriteRenderer.flipX = false;
                if(isSkill)
                    box.offset = new Vector2(0.8f, 0);
                else
                    box.offset = new Vector2(0.4f, 0);
            }
            else
            {
                // 플레이어가 왼쪽을 바라보면 화살을 왼쪽으로 발사
                moveDirection = Vector3.left;
                spriteRenderer.flipX = true;
                if (isSkill)
                    box.offset = new Vector2(-0.8f, 0);
                else
                    box.offset = new Vector2(-0.4f, 0);
            }
        }
    }


    Collider2D FindCollider(Collider2D[] colliders)// 가장 가까운 적 찾기
    {
        Collider2D closestCollider = null;
        float distance = float.MaxValue;
        foreach (Collider2D coll in colliders)
        {
            if (LayerMask.LayerToName(coll.gameObject.layer) != "Pad" && LayerMask.LayerToName(coll.gameObject.layer) != "Tilemap")
            {
                float tempDist = Vector2.Distance(transform.position, coll.transform.position);
                if (tempDist < distance)
                {
                    closestCollider = coll;
                    distance = tempDist;
                }
            }
        }
        return closestCollider;
    }

    private void Update()
    {
        // 화살 탐지 기능 추가
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, islayer);
        Collider2D closestCollider = FindCollider(hitColliders);

        if (isSkill == true) // 스킬일 때
        {
            pos.position += moveDirection * speed * Time.deltaTime; // 화살 직진 이동
        }
        else
        {
            if (player.proSelectWeapon == 2 && closestCollider != null && closestCollider.tag != "Wall" && closestCollider.tag != "Pad" && closestCollider.tag != "Tilemap" && player.proLevel >= 1) // 일정 거리 내에 적이 있으면 가장 가까운 적으로 이동
            {
                if (hitColliders.Length > 0)
                {
                    Vector2 direction = (closestCollider.transform.position - transform.position).normalized;
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg); // 화살 각도 변경
                    Vector2 pos2D = new Vector2(pos.position.x, pos.position.y);
                    pos.rotation = rotation;
                    pos2D += speed * Time.deltaTime * direction;
                    pos.position = new Vector3(pos2D.x, pos2D.y, pos.rotation.z);
                }
            }
            else
            {
                pos.position += moveDirection * speed * Time.deltaTime; // 화살 직진 이동
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //화살이 충돌시 확인후 공격 및 사라지게함
    {
        if (collision.tag == "Enemy")
        {
            if (isSkill == true && !hitDict.ContainsKey(collision)) // 스킬 사용 중이고, 이미 적에게 대미지를 입힌 경우가 아닐 때
            {
                hitDict.Add(collision, true); // 적 정보를 Dictionary에 추가
            }
            else 
            {
                hit  = true;
                Off();
                Invoke("DestroyArrow", 3f);
            }
        }
        else if (collision.tag == "Wall" || collision.tag == "Tilemap") // 벽이나 땅에 맞으면 화살 사라짐 패드는 없는게 나은것 같아서 뺐음
        {
            if(!isSkill)
            {
                if (hit)
                {
                    return;
                }
                Off();
                Invoke("DestroyArrow", 3f);
            }
        }
    }

    public void DestroyArrow()  // 화살 제거 함수
    {
        Destroy(gameObject);
    }
    public void Off()
    {
        CancelInvoke();
        spriteRenderer.enabled = false;
        collider.enabled = false;
    }
}


