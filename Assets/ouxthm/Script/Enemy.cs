using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: 달팽이, 2: 근접공격 가능 몬스터, 3:비행몬스터, 4:제라스, 5: 자폭, 7: 투사체 원거리, 9: 분열, 11: 돌진하여 충돌
    public float Enemy_HP;  // 적의 체력
    public float Enemy_Power;   //적의 공격력
    public float Enemy_Speed;   // 적의 이동속도
    public float Enemy_Atk_Speed;    // 적의 공격속도
    public bool Attacking = false;  // 공격 중인이 확인하는 변수
    public float Gap_Distance_X;  // 적과 Player X 거리차이
    public float Gap_Distance_Y;  // 적과 Player Y 거리차이
    public float Enemy_Sensing_X;  // 적의 X축 감지 사거리
    public float Enemy_Sensing_Y;  // 적의 Y축 감지 사거리
    public float Enemy_Range_X; //적의 X축 공격 사거리
    public float Enemy_Range_Y; //적의 Y축 공격 사거리
    public float Pdamage;   // 몬스터가 받는 데미지
    public float Bump_Power;    // 플레이어와 충돌 시 줄 데미지
    public float atkDelay;  // 공격 딜레이
    public int nextDirX;    // 비행 몬스터의 X 방향
    public int nextDirY;    // 비행 몬스터의 Y 방향  
    public bool Dying = false; // 죽는 중을 확인하는 변수
    public float Enemy_Dying_anim_Time;     // 죽는 애니메이션 시간 변수
    public float atkX;  // 공격 콜라이더의 x값
    public float atkY;  // 공격 콜라이더의 y값
    public bool enemyHit = false;   // 적이 피해입은 상태인지 확인하는 변수
    public float old_Speed;     // 속도 값 변하기 전 속도 값
    public float dTime;
    public float atkTime;   // 공격 모션 시간
    public bool Attacker;  // 비행 몬스터가 공격형인지 아닌지 구분짓는 변수
    public float endTime;   // 투사체 사라지는 시간

    public bool turning;    // 보스가 뒤돌 수 있는 상황인지 확인하는 변수
    public int atkPattern;  // boss의 공격 패턴 번호
    public float playerLoc; // player의 X좌표
    public float bossLoc;   // boss의 X좌표
    public float myLocY;    // boss의 y값
    public bool bossMoving;  // boss가 움직이도록 rock 풂

    public bool Enemy_Left; // 적의 방향
    public bool Hit_Set;    // 몬스터를 깨우는 변수
    public float boarLoc;    // 멧돼지의 현재위치 X



    Transform soulSpawn;    // 보스 바닥 터뜨리기 생성 위치
    Transform soulSpawn1;   // 보스 바닥 터뜨리기 생성 위치
    Transform soulSpawn2;   // 보스 바닥 터뜨리기 생성 위치

    public GameObject SoulFloor; // 보스 영혼 공격
    public GameObject Split_Slime;
    public GameObject fire; // 프리펩 투사체
    public GameObject ProObject;    // 클론 투사체
    

    Transform spawn;    // 분열된 슬라임 생성될 위치 1
    Transform spawn2;   // 분열된 슬라임 생성될 위치 2
    public Transform PObject;    // 투사체 생성 위치
    Rigidbody2D rigid;
    Animator animator;
   // public Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    BoxCollider2D Box;
    Transform posi;
    BoxCollider2D Boxs;
    BoxCollider2D bossBox;  // 보스 공격 콜라이더 켰다 끄는 변수
    BoxCollider2D BossSpriteBox;    // 보스 이미지 콜라이더

    /* Enemy_Attack 스크립트에 GiveDamage()함수를 넣을거임 콜라이더 범위에 들어와도 몬스터 자체 콜라이더에 플레이어가 충돌해야 데미지가 들어가기 때문에 몬스터 공격 콜라이더만 들어 갈 수 있도록
     스크립트를 몬스터 공격 오브젝트에 넣을거임*/
    //Enemy_Attack Enemy_Attack;  

    public Transform Pos;
    public Arrow arrow;
    public Effect slash;

    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수(추상)

    public virtual void Short_Monster(Transform target) 
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x); //X축 거리 계산
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y); //Y축 거리 계산
        Sensing(target, rayHit);
        Sensor();
        if(nextDirX != 0)   // 특정 몬스터에만 Run 애니메이션이 있기 때문에 지정해줘야 함
        {
            animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
            if (Enemy_Mod != 1 && Enemy_Mod != 3 && Enemy_Mod != 4)
            {
                animator.SetBool("Run", true);
                if (enemyHit)
                {
                    animator.SetBool("Run", false);
                    if (!enemyHit)
                    {
                        animator.SetBool("Run", true);
                    }
                }
            }
        }
        else if(nextDirX == 0)
        {
            animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
            if (Enemy_Mod != 1 && Enemy_Mod != 3 && Enemy_Mod != 4)
            {
                animator.SetBool("Run", false);
            }
        }
    }

    public virtual void Boss(Transform target)  // boss용 Update문
    {
        playerLoc = target.position.x;
        bossLoc = this.gameObject.transform.position.x;
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            bossMove();
            BossAtk();
        }
    }

    public virtual void Boar(Transform target)  // boar용
    {
        playerLoc = target.position.x;
        boarLoc = this.gameObject.transform.position.x;

        StartCoroutine(boarMove());
    }
    
    public virtual void onetime()   // Awake에 적용
    {
        Pos = GetComponent<Transform>();
        StartCoroutine(Think());
        if(Enemy_Mod == 7)
        {
            PObject = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        }

    }

    public virtual void bossOnetime()   // boss용 Awake문
    {
        PObject = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        BossSpriteBox = this.gameObject.GetComponent<BoxCollider2D>();
        bossBox = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Pos = GetComponent<Transform>();
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        soulSpawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        soulSpawn1 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        soulSpawn2 = this.gameObject.transform.GetChild(4).GetComponent<Transform>();

        randomAtk();
    }

    public virtual void boarOntime()
    {
        Hit_Set = false;    // 플레이어에게 맞지 않은 상태
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Pos = GetComponent<Transform>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Boxs = GetComponent<BoxCollider2D>();
            Bump();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Enemy_Mod == 11 && collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(StopRush());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Arrow")
            {
                arrow = collision.GetComponent<Arrow>();
                if (arrow != null)
                {
                    Pdamage = arrow.Dmg;
                    StartCoroutine(Hit(Pdamage));  
                }
            }
            else if (collision.tag == "Slash")
            {
                slash = collision.GetComponent<Effect>();
                if (slash != null)
                {
                    Pdamage = slash.Dmg;
                    StartCoroutine(Hit(Pdamage));
                }
            }
        }
    }
    void switchCollider()   // 제라스 박스 콜라이더 위치 옮겨주는 함수
    {
        if (Enemy_Mod == 4)
        {
            Box = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
            Vector2 firoffset = Box.offset;
            Vector2 offset = Box.offset;

            if (nextDirX == -1)
            {
                Box.offset = firoffset;
            }
            else if (nextDirX == 1)
            {
                offset.x *= -1;
                Box.offset = offset;
            }
        }
        if(Enemy_Mod == 2 || Enemy_Mod == 3)
        {
            Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // 본인 오브젝트의 첫번째 자식 오브젝트에 포함된 BoxCollider2D를 가져옴.
            spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
            posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
            Bcollider.enabled = true;   // 공격 박스 콜라이더 생성
            if (spriteRenderer.flipX == true)   // 이미지 플립했을 때 공격 범위 x값 전환 조건문
            {
                posi.localPosition = new Vector3(atkX, atkY);   // 몬스터의 공격 콜라이더 박스의 x좌표와 y좌표
            }
            else if (spriteRenderer.flipX == false) // 왼쪽을 볼 때
            {
                posi.localPosition = new Vector3(-atkX, atkY);  // 몬스터의 공격 콜라이더 박스의 -x좌표와 y좌표
            }
        }
    }
    void Move() // 이동
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        gameObject.transform.Translate(new Vector2(nextDirX, 0) * Time.deltaTime * Enemy_Speed);

        if (nextDirX == -1)
        {
            spriteRenderer.flipX = false;
        }
        else if (nextDirX == 1)
        {
            spriteRenderer.flipX = true;
        }
    }

    IEnumerator Think() // 자동으로 다음 방향을 정하는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX = Random.Range(-1, 2);     // 적의 X방향 랜덤( -1 ~ 1)
        if(Enemy_Mod == 3)
        {
        nextDirY = Random.Range(-1, 2);     // 적의 Y방향 랜덤( -1 ~ 1)
        }
        
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true;       // nextDirX의 값이 1이면 x축을 flip함
        }
        // 재귀
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    void Turn() // 이미지를 뒤집는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX *= -1;   // nextDirX에 -1을 곱해 방향전환
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true; // nextDirX 값이 1이면 x축을 flip함
        }
        StopAllCoroutines();
        StartCoroutine(Think());
    }

    public IEnumerator Hit(float damage) // 피해 함수
    {
        posi = this.gameObject.GetComponent<Transform>();
        enemyHit = true;
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();
        this.GetComponentInChildren<EnemyUi>().ShowDamgeText(damage); //윤성권 추가함

        Enemy_HP -= damage;
        if(Enemy_Mod == 11)
        {
            Hit_Set = true;
            StartCoroutine(Rush());
        }

        if (Enemy_HP > 0) // Enemy의 체력이 0 이상일 때
        {
            if (!animator.GetBool("Hit") && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
            {
                if (Enemy_Mod != 1 && Enemy_Mod != 3 && Enemy_Mod != 4 && Enemy_Mod != 11)
                    if (animator.GetBool("Run") == true)
                    {
                        animator.SetBool("Run", false);
                    }
                if (Enemy_Speed > 0)
                {
                    old_Speed = Enemy_Speed;  // 이전 속도 값으로 돌리기 위해 다른 변수에 속도 값을 저장
                }
                animator.SetTrigger("Hit");
                Enemy_Speed = 0;
                yield return new WaitForSeconds(0.5f);
                Enemy_Speed = old_Speed;    // 이전 속도 값으로 복구
                enemyHit = true;
            }
        }
        else if (Enemy_HP <= 0 && Enemy_Mod != 3 && Enemy_Mod != 11 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy")) // Enemy의 체력이 0과 같거나 이하일 때(죽음)
        {
            Dying = true;
            if (Enemy_Mod != 1 && Enemy_Mod != 3 && Enemy_Mod != 4 && Enemy_Mod != 11)
            {
                if (animator.GetBool("Run") == true)
                {
                    animator.SetBool("Run", false);
                }
            }
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            if(Enemy_Mod != 11)
            {
                animator.SetTrigger("Die");
            }
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            yield return new WaitForSeconds(Enemy_Dying_anim_Time );
            enemyHit = false;
            if(Enemy_Mod == 9 && posi.localScale.y > 1f)   // 분열 몬스터일 경우
            {
                StartCoroutine(Split());
                this.gameObject.SetActive(false);
            }       
            else if (Enemy_Mod == 9 && posi.localScale.y <= 1f)
            {
                this.gameObject.SetActive(false);   // clone slime 제거
                //Destroy();
            }
            else if(Enemy_Mod != 9)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 3 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy")) // 비행 몬스터 죽음)
        {
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;
            if (Attacker)
            {
                for (int i = 0; i < 4; i++)
                {
                    // 스프라이트 블링크
                    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                    yield return new WaitForSeconds(0.1f);
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    yield return new WaitForSeconds(0.1f);
                }
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            }
            else if (!Attacker)
            {
                animator.SetTrigger("Die");
                rigid.isKinematic = false;
            }
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.gameObject.SetActive(false);
        }
        else if(Enemy_HP <= 0 && Enemy_Mod == 11 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            animator.SetBool("Rush", false);
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;
            Debug.Log("죽는 애니메이션 직전");
            for (int i = 0; i < 4; i++)
            {
                // 스프라이트 블링크
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Debug.Log("반짝반짝");
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            this.gameObject.gameObject.SetActive(false);
        }
        animator.SetBool("Hit", false);
        enemyHit = false;
    }


    void Sensing(Transform target, RaycastHit2D rayHit)  // 플레이어 추적
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        if (Gap_Distance_X <= Enemy_Sensing_X && Gap_Distance_Y <= Enemy_Sensing_Y)      // Enemy의 X축 사거리에 있을 때, Y축 사거리에 있을 때
        {
            if (transform.position.x < target.position.x)            // 오른쪽 방향
            {
                nextDirX = 1;
                if (Enemy_Mod != 3)  // 몬스터가 비행타입이 아닐 때
                {
                    if (nextDirX == 1 && rayHit.collider != null)  // nextDirX가 1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = true;
                        if(Enemy_Mod == 5)  // 자폭 몬스터가 자폭할 때 제자리에 있기 위한 코드
                        {
                            Enemy_Speed = 5f;
                            if (Attacking == true)
                            {
                                Enemy_Speed = 0f;
                            }
                        }
                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if(Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                if (Enemy_Mod == 5) // 자폭이라 딜레이 없이 바로 공격해야 함.
                                {
                                    Attack();
                                }
                                else
                                {
                                    Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                                }

                            }
                        }
                    }
                    else if (nextDirX == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (0,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if(Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                            }
                        }
                    }
                }
                else if (nextDirX == 1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = true;
                    if (Attacker)
                    {
                        Vector2 resHeight = new Vector2(-1.5f, 1f);
                        Vector2 playerPoint = (Vector2)target.transform.position + resHeight;   // 플레이어와 겹쳐서 공격하는 것을 방지하기 위해 새로운 지점을 정의함
                        transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);   // resHeight를 더해주어 플레이어의 아래에서 공격하지 않도록 했음
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                        }
                    }
                    else if (!Attacker) 
                    { 
                        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Enemy_Speed * Time.deltaTime);
                    }
                }
            }
            else if (transform.position.x > target.position.x)      // 왼쪽 방향
            {
                nextDirX = -1;
                if (Enemy_Mod != 3) // 몬스터가 비행타입이 아닐 때
                {
                    if (nextDirX == -1 && rayHit.collider != null) // nextDirX가 -1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = false;
                        if (Enemy_Mod == 5)
                        {
                            Enemy_Speed = 5f;
                            if (Attacking == true)
                            {
                                Enemy_Speed = 0f;
                            }
                        }
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if(Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                if (Enemy_Mod == 5)
                                {
                                    Attack();
                                }
                                else
                                {
                                    Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                                }
                            }
                        }
                    }
                    else if (nextDirX == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                            }
                        }
                    }
                }
                else if (nextDirX == -1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = false;
                    if (Attacker)
                    {
                        Vector2 resHeight = new Vector2(1.5f, 1f);
                        Vector2 playerPoint = (Vector2)target.transform.position + resHeight;       // 플레이어와 겹쳐서 공격하는 것을 방지하기 위해 새로운 지점을 정의함(Vector2를 정의하여 +를 쓸 때 Vector2인지 3인지 모호하지 않게 함)
                        transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y) // 타겟의 위치에 2.5f를 더해서 Bee가 플레이어의 아래쪽에서 공격하는 것을 방지
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                        }
                    }
                    else if (!Attacker)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Enemy_Speed * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            if (Enemy_Mod != 3)
            {
                Move();     // Move 함수 실행
            }
        }
    }

    public void Sensor()    // 플랫폼 감지 함수
    {
        rigid = this.GetComponent<Rigidbody2D>();
        if (Enemy_Mod != 3)
        {
            // Enemy의 한 칸 앞의 값을 얻기 위해 자기 자신의 위치 값에 (x)에 + nextDirX값을 더하고 1.2f를 곱한다.
            Vector2 frontVec = new Vector2(rigid.position.x + nextDirX * 1.2f, rigid.position.y);

            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));

            // 레이저를 아래로 쏘아서 실질적인 레이저 생성(물리기반), LayMask.GetMask("")는 해당하는 레이어만 스캔함
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Tilemap", "Pad", "wall"));
            if (rayHit.collider == null)
            {
                Turn();
            }
        }
    }

    public void Attack() //공격 함수
    {
        animator = this.GetComponentInChildren<Animator>();
        if (!Dying && Enemy_Mod != 5)
        {
            if (Enemy_Mod == 3)
            {
                switchCollider();
                GiveDamage();       // 플레이어에게 데미지 주는 함수 실행    
                animator.SetTrigger("Attack");  // 벌 공격용
                animator.SetBool("Attacking", true);    // 벌 공격 확인하는 용인 듯(너무 오래되서 기억 안 남)
                Enemy_Speed = 0;
            }
            else if (Enemy_Mod != 3 && Enemy_Mod != 7 && Enemy_Mod != 2)
            {
                switchCollider();
                onAttack();
                Invoke("GiveDamage", 0.8f);
            }
            else if (Enemy_Mod == 7)
            {
                onAttack();
                Invoke("ProjectiveBody", 0.5f);
            }
            else if(Enemy_Mod == 2)
            {
                switchCollider();
                onAttack();
                Invoke("GiveDamage", 0.5f);
            }

            if (Attacking == true)
            {
                Invoke("offAttkack", atkTime);
            }
        }
        else if(!Dying && Enemy_Mod == 5)
        {
            StartCoroutine(Boom());
        }         
    }

    public void onAttack()
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("Attacking");
        animator.SetBool("Run", false); 
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void offAttkack() // 공격 종료 함수
    {
        if(Enemy_Mod == 3)
        {
            Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // 본인 오브젝트의 첫번째 자식 오브젝트에 포함된 BoxCollider2D를 가져옴. 
            animator.SetBool("Attacking", false);
            Enemy_Speed = 3f;
            Bcollider.enabled = false;
            Attacking = false;
        }
        else if(Enemy_Mod != 3 && Enemy_Mod !=7 )
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Attacking = false;
        }
        else if(Enemy_Mod == 7)
        {
            Attacking = false;
        }
    }

    public void GiveDamage()    // 플레이어에게 데미지를 주는 함수
    {
        posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        Box = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
       
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(posi.position, Box.size, 0);
        
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
            {
                collider.GetComponent<Player>().Playerhurt(Enemy_Power, Pos.position);
                Debug.Log("데미지 줌");
            }
            else
            {
                Debug.Log("데미지 못 줌");
            }
        }
    }

    public void Bump()      // 충돌 데미지 함수
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(Pos.position, Boxs.size, 0);
        Player player = GetComponent<Player>();
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
                collider.GetComponent<Player>().Playerhurt(Bump_Power, Pos.position);
        }
    }

    public IEnumerator Boom()  // 폭발 함수
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        animator.SetBool("Attacking", true);
        Attacking = true;
        yield return new WaitForSeconds(0.5f);
        /*Enemy_Attack.*/GiveDamage();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }

    public IEnumerator Split()  // 슬라임 분열 함수
    {
        spawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        spawn2 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        GameObject splitSlime = Instantiate(Split_Slime, spawn.position, spawn.rotation);
        GameObject splitSlime2 = Instantiate(Split_Slime, spawn2.position, spawn2.rotation);
        yield return null;
    }


    public void slimeJump() //슬라임 점프공격
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("Attacking");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    /*public void Destroy()
    {
        Debug.Log("아예 삭제");
        Destroy(Split_Slime); 

    }*/

    public void ProjectiveBody()    // 투사체 생성 (위치 저장)
    {
        Rigidbody2D rigid = PObject.GetComponent<Rigidbody2D>();
        if(Enemy_Mod != 2)
        {
            if (nextDirX == 1)
            {
                PObject.localPosition = new Vector2(-0.8f, 0);
            }
            else if (nextDirX == -1)
            {
                PObject.localPosition = new Vector2(0.8f, 0);
            }
        }
        else if(Enemy_Mod == 2)
        {
            if(nextDirX == 1)
            {
                PObject.localPosition = new Vector2(3f, 0.15f);
            } 
            else if(nextDirX == -1)
            {
                PObject.localPosition = new Vector2(-3f, 0.15f);
            }
        }
        
        ProObject = Instantiate(fire, PObject.position, PObject.rotation);

        Projective_Body Pb = ProObject.GetComponent<Projective_Body>();
        Pb.Dir = nextDirX;   // Projective_Body 스크립트에 있는 Dir 변수에 현재 스크립트의 변수 nextDirX를 저장
        Pb.Power = Enemy_Power;
        Pb.Time = endTime;
    }

    public void soulSpawning()
    {
        GameObject Soul = Instantiate(SoulFloor, soulSpawn.position, soulSpawn.rotation);
        SoulEff Se = Soul.GetComponent<SoulEff>();

        Se.Time = endTime;
        Se.Power = Enemy_Power;
        Se.Dir = nextDirX;
       
    }
    public void soulSpawning1()
    {
        GameObject Soul1 = Instantiate(SoulFloor, soulSpawn1.position, soulSpawn1.rotation);
        SoulEff Se1 = Soul1.GetComponent<SoulEff>();

        Se1.Time = endTime;
        Se1.Power = Enemy_Power;
        Se1.Dir = nextDirX;
    }

    public void soulSpawning2()
    {
        GameObject Soul2 = Instantiate(SoulFloor, soulSpawn2.position, soulSpawn2.rotation);
        SoulEff Se2 = Soul2.GetComponent<SoulEff>();

        Se2.Time = endTime;
        Se2.Power = Enemy_Power;
        Se2.Dir = nextDirX;
        turning = true;
    }


    public void BossAtk()
    {
        if (turning == true)    // 돌기 가능할 때만
        {
            if (playerLoc < bossLoc)
            {
                spriteRenderer.flipX = true;
                nextDirX = -1;
                BossSpriteBox.offset = new Vector2(0.3042426f, -0.3726118f);

                soulSpawn.localPosition = new Vector2(-5.5f, 0.197f);
                soulSpawn1.localPosition = new Vector2(-9.22f, 0.197f);
                soulSpawn2.localPosition = new Vector2(-12.94f, 0.197f);
            }
            else if (playerLoc > bossLoc)
            {
                spriteRenderer.flipX = false;
                nextDirX = 1;
                BossSpriteBox.offset = new Vector2(-0.3042426f, -0.3726118f);

                soulSpawn.localPosition = new Vector2(5.5f, 0.197f);
                soulSpawn1.localPosition = new Vector2(9.22f, 0.197f);
                soulSpawn2.localPosition = new Vector2(12.94f, 0.197f);
            }
        }
        switch (atkPattern)
        {
            case 1:
                bossSoul();
                Invoke("ProjectiveBody", 3f);
                atkPattern = 0;
                break;

            case 2:
                StartCoroutine(bossJump());
                atkPattern = 0;
                break;

            case 3:
                bossFloor();
                atkPattern = 0;
                break;
        }
    }
 
    public void bossMove()  // boss의 움직이도록 하는 함수
    {
        if (bossMoving)
        {
            gameObject.transform.Translate(new Vector2(nextDirX, 0) * Time.deltaTime * Enemy_Speed);   
        }
    }
    public void randomAtk() // 공격 패턴 랜덤으로 정하기
    {
        int nextNum;

        atkPattern = Random.Range(1, 4);
        nextNum = Random.Range(4, 7);
        Invoke("randomAtk", nextNum);
    }

    public void bossSoul()      // 영혼 발사
    {
        animator.SetTrigger("Attacking");
    }

    public IEnumerator bossJump()       // 몸통박치기
    {
        animator.SetTrigger("Jump");
        Enemy_Speed = 12f;
        bossMoving = true;
        yield return new WaitForSeconds(0.5f);
        bossMoving = false;
    }

    public void bossFloor()     // 바닥 터뜨리는 기술
    {
        Enemy_Speed = 1f;
        animator.SetTrigger("Run");
        bossMoving = true;
        Invoke("offFloor", 2f);
        
    }

    public void offFloor()  // 보스 바닥 터뜨리는 공격 마무리 함수
    {
        myLocY = this.gameObject.transform.position.y;
        if(playerLoc < bossLoc) 
        {
            this.gameObject.transform.localPosition = new Vector2(playerLoc + 3f, myLocY);
        }
        else if (playerLoc > bossLoc)
        {
            this.gameObject.transform.localPosition = new Vector2(playerLoc - 3f, myLocY);
        }
        
        animator.SetTrigger("Spawn");
        bossMoving = false;
        turning = false;
        Invoke("onBox", 0.9f);
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("soulSpawning", 1f);
            Invoke("soulSpawning1", 1.3f);
            Invoke("soulSpawning2", 1.6f);    
        }
    }
    public void onBox() // 보스 공격 콜라이더 on 함수
    {
        if(spriteRenderer.flipX == true)
        {
            bossBox.transform.localPosition = new Vector2(-1.71f, 0);
            
        }
        else if(spriteRenderer.flipX == false)
        {
            bossBox.transform.localPosition = new Vector2(1.71f, 0);
            
        }
        bossBox.enabled = true;
        GiveDamage();
        bossBox.enabled = false;
    }

    IEnumerator boarMove()
    {
        if (Hit_Set == true)    // 플레이어에게 맞았다면
        {
            if (animator.GetBool("Rush") && Enemy_Left == true)       // 뛰는 애니메이션이 실행 중이고, Fat_Left의 값이 true라면
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
                spriteRenderer.flipX = false;
            }
            else if (animator.GetBool("Rush") && Enemy_Left == false)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
                spriteRenderer.flipX = true;
            }
        }
        yield return null;
    }

    IEnumerator Rush()   // 돌진 코루틴.
    {
        if (playerLoc < boarLoc) // 플레이어가 왼쪽에 있다면.
        {
            Enemy_Left = true;
        }
        else if (playerLoc > boarLoc)    // 플레이어가 오른쪽에 있다면.
        {
            Enemy_Left = false;
        }
        yield return new WaitForSeconds(1f);    // 플레이어 인지 후 달려가기 위한 기 모음 1초
        Attacking = true;

        animator.SetBool("Rush", true);

        Enemy_Speed = 10f;        //  속도 10 설정.
    }

    IEnumerator StopRush()
    {
        animator.SetBool("Rush", false);
        animator.SetTrigger("Hit");
        Attacking = false;  // 공격 중 끄기
        if (Enemy_Left == true)
        {
            Enemy_Left = false;
            Debug.Log("바꿀게1");
        }
        else if (Enemy_Left == false)
        {
            Enemy_Left = true;
            Debug.Log("바꿀게2");
        }
        yield return null; 
        Hit_Set = false;
    }

}