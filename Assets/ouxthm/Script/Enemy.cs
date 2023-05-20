using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: 달팽이, 2: 근접공격 가능 몬스터, 3:비행몬스터, 4:원거리 몬스터
    public float Enemy_HP;  // 적의 체력
    public float Enemy_Power;   //적의 공격력
    public float Enemy_Speed;   // 적의 이동속도
    public float Enemy_Atk_Speed;    // 적의 공격속도
    public bool Attacking = false;  // 공격 중인이 확인하는 변수
    public bool Hit_Set;    // 피해를 입었는지 확인하는 변수
    public float Gap_Distance_X;  // 적과 Player X 거리차이
    public float Gap_Distance_Y;  // 적과 Player Y 거리차이
    public int NextMove;  // 방향을 숫자로 표현
    public float Enemy_Sensing_X;  // 적의 X축 감지 사거리
    public float Enemy_Sensing_Y;  // 적의 Y축 감지 사거리
    public float Enemy_Range_X; //적의 X축 공격 사거리
    public float Enemy_Range_Y; //적의 Y축 공격 사거리
    public float Pdamage;   // 몬스터가 받는 데미지
                            //public float Cooltime;  // 공격 쿨타임

    public bool Dying = false; // 죽는 중을 확인하는 변수
    public float Enemy_Dying_anim_Time;     // 죽는 애니메이션 시간 변수

    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    public BoxCollider2D Box;
    public Transform posi;
    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수(추상 클래스)

    public virtual void Short_Monster(Transform target) // 근거리 공격 몬스터
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        Sensing(target, rayHit);
        Sensor();
    }
    public virtual void onetime()   // Awake에 적용
    {
        StartCoroutine(Think());
    }
    public virtual void Long_Monster()  // 원거리 공격 몬스터
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()   // 비행 몬스터
    {
        Debug.Log("Fly_Monster");
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider2D)
    {
        yield return null;

        animator = this.GetComponentInChildren<Animator>();
        if (collider2D.gameObject.tag == "Wall")
        {
            Debug.Log(collider2D.gameObject.tag);
        }
        else if (collider2D.gameObject.tag == "Sword")
        {
            StartCoroutine(Hit(Pdamage));
        }
        else if (collider2D.gameObject.tag == "Axe")
        {
            StartCoroutine(Hit(Pdamage));
        }
        else if (collider2D.gameObject.tag == "Arrow")
        {
            StartCoroutine(Hit(Pdamage));
        }
    }
    void Move() // 플레이어 감지 전 move
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Enemy_Sensing_X < Gap_Distance_X)    // 사정거리 밖에 있을 때 
        {
            if (NextMove == -1)       // Enemy의 값이 true라면
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
                spriteRenderer.flipX = true;
            }
        }
        else if (Enemy_Sensing_X > Gap_Distance_X && Enemy_Sensing_Y < Gap_Distance_Y)
        {
            if (NextMove == -1)       // Enemy의 값이 true라면
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
                spriteRenderer.flipX = true;
            }
        }
    }



    IEnumerator Think() // 자동으로 다음 방향을 정하는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1까지의 랜덤한 수 저장
        if (NextMove != 0 && NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true;       // NextMove의 값이 1이면 x축을 flip함
        }
        // 재귀
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    IEnumerator Turn() // 이미지를 뒤집는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        NextMove *= -1;   // NextMove에 -1을 곱해 방향전환
        if (NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true; // NextMove 값이 1이면 x축을 flip함
        }
        StopAllCoroutines();
        StartCoroutine(Think());
        yield return null;
    }

    public IEnumerator Hit(float damege) // 피해 함수
    {
        Debug.Log(damege + "Enemy");
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();
        Enemy_HP -= damege;
        if (Enemy_HP > 0) // Enemy의 체력이 0 이상일 때
        {
            float old_Speed = Enemy_Speed;  // 이전 속도 값으로 돌리기 위해 다른 변수에 속도 값을 저장
            animator.SetTrigger("Hit");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(0.5f);
            Enemy_Speed = old_Speed;    // 이전 속도 값으로 복구

        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 1) // Enemy의 체력이 0과 같거나 이하일 때(죽음)
        {
            Dying = true;
            animator.SetTrigger("Die");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.SetActive(false);   // 오브젝트 사라지게 함
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 3)// 비행 몬스터 죽음)
        {
            Dying = true;
            Enemy_Speed = 0;
            NextMove = 0;
            for (int i = 0; i < 4; i++)  // 3번 반복
            {
                // 스프라이트 블링크
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.gameObject.SetActive(false);
        }
    }


    void Sensing(Transform target, RaycastHit2D rayHit)  // 플레이어 추적
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X && Gap_Distance_Y <= Enemy_Sensing_Y)      // Enemy의 X축 사거리에 있을 때
        {
            if (transform.position.x < target.position.x)            // 오른쪽 방향
            {
                NextMove = 1;
                if (Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4)  // 몬스터가 비행타입이 아닐 때
                {
                    if (NextMove == 1 && rayHit.collider != null)  // NextMove가 1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = true;

                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 4))
                        {
                            Attacking = true;
                            Attack();
                        }
                    }
                    else if (NextMove == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (0,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 4))
                        {
                            Attacking = true;
                            Attack();
                        }
                    }
                }
                else if (NextMove == 1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = true;
                    Vector2 resHeight = new Vector2(0f, 2.5f);
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)target.position + resHeight, Enemy_Speed * Time.deltaTime);   // resHeight를 더해주어 플레이어의 아래에서 공격하지 않도록 했음(실제로 되는지 제대로 확인해야 함)
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 2f <= transform.position.y)
                    {
                        Attacking = true;
                        Attack();
                    }
                }

            }
            else if (transform.position.x > target.position.x)      // 왼쪽 방향
            {
                NextMove = -1;
                if (Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4) // 몬스터가 비행타입이 아닐 때
                {
                    if (NextMove == -1 && rayHit.collider != null) // NextMove가 -1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = false;
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            Attacking = true;
                            Attack();
                        }
                    }
                    else if (NextMove == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            Attacking = true;
                            Attack();
                        }
                    }
                }
                else if (NextMove == -1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = false;
                    Vector2 resHeight = new Vector2(0f, 2.5f);
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)target.position + resHeight, Enemy_Speed * Time.deltaTime); // target.position 앞에 Vector2를 정의하여 +를 쓸 때 Vector2인지 3인지 모호하지 않게 함
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 2f <= transform.position.y) // 타겟의 위치에 2.5f를 더해서 Bee가 플레이어의 아래쪽에서 공격하는 것을 방지
                    {
                        Attacking = true;
                        Attack();
                    }
                }
            }
        }
        else
        {
            Move();     // Move 함수 실행
        }
    }

    public void Sensor()    // 플랫폼 감지 함수
    {
        rigid = this.GetComponent<Rigidbody2D>();
        if (Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4)
        {
            // Enemy의 한 칸 앞의 값을 얻기 위해 자기 자신의 위치 값에 (x)에 + NextMove값을 더하고 1.2f를 곱한다.
            Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));
            // 레이저를 아래로 쏘아서 실질적인 레이저 생성(물리기반), LayMask.GetMask("")는 해당하는 레이어만 스캔함
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Tilemap"));
            if (rayHit.collider == null)
            {
                StartCoroutine(Turn());
            }
        }

    }

    public void Attack() //공격 함수
    {
        Transform AtkTransform = transform.GetChild(0);
        animator = this.GetComponentInChildren<Animator>();
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // 본인 오브젝트의 첫번째 자식 오브젝트에 포함된 BoxCollider2D를 가져옴.
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (!Dying)
        {
            if (spriteRenderer.flipX == true)   // 이미지 플립했을 때 공격 범위 x값 전환 조건문
            {
                AtkTransform.localPosition = new Vector3(0.8f, -1.2f);
            }
            else if (spriteRenderer.flipX == false)
            {
                AtkTransform.localPosition = new Vector3(-0.8f, -1.2f);
            }
            animator.SetTrigger("Attack");
            GiveDamage();
            animator.SetBool("Attacking", true);
            Enemy_Speed = 0;
            if (Attacking == true)
            {
                Invoke("offAttkack", 0.5f);

            }
        }

    }
    public void offAttkack() // 공격 종료 함수
    {
        Attacking = false;
        animator.SetBool("Attacking", false);
        Enemy_Speed = 3f;
    }

    public void GiveDamage()
    {
        posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        Box = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(posi.position, Box.size, 0);
        Player player = GetComponent<Player>();
        foreach (Collider2D collider in collider2D)
        {
            if (player != null)
            {
                collider.GetComponent<Player>().Playerhurt(Enemy_Power);
            }
        }
    }






}