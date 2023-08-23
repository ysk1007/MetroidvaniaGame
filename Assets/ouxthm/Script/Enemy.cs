using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Player player;
    public string Enemy_Name; //윤성권 추가함
    public bool AmIBoss = false; //윤성권 추가함
    public int BossHpLine; //윤성권 추가함
    public int Stage;

    public int Enemy_Mod;   // 1: 달팽이, 2: 근접공격 가능 몬스터, 3:비행몬스터, 4:제라스, 5: 자폭, 7: 투사체 원거리, 9: 분열, 11: 돌진하여 충돌
    public float Enemy_HP;  // 적의 체력
    public float Enemy_HPten;   // 적의 체력의 10%
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
    public float atkFlipx; // 공격 콜라이더의 x값(늑대 용)
    public float atkY;  // 공격 콜라이더의 y값
    public bool enemyHit = false;   // 적이 피해입은 상태인지 확인하는 변수
    public float old_Speed;     // 속도 값 변하기 전 속도 값
    public float dTime;
    public float atkTime;   // 공격 모션 시간
    public bool Attacker;  // 비행 몬스터가 공격형인지 아닌지 구분짓는 변수
    public float endTime;   // 투사체 사라지는 시간

    public float scaleX; // scale X 값(hit_eff가 스케일이 -되어있으면 반대방향으로 뜨기에 설정하기 위한 변수)
    public string weaponTag;    // 무기 태그 ("Sword"일 때만 출혈)
    public int Swordlevel;  // 플레이어 검 숙련도
    public int selectWeapon;    // 숙련도를 올릴 무기 선택 0 = 칼, 1 = 도끼, 2 = 활, 4 = 선택 X
    public int bleedLevel;  // 출혈 스택
    public int slashBleedLevel; // 검기로 넣은 출혈스택
    public float bleedingDamage;    // 출혈 데미지
    public float bleedingTime;  // 출혈 지속시간
    public float bloodBoomDmg;  // 플레이어의 출혈 스택 터뜨리는 데미지

    public bool turning;    // 보스가 뒤돌 수 있는 상황인지 확인하는 변수
    public int atkPattern;  // boss의 공격 패턴 번호
    public float playerLoc; // player의 X좌표
    public float enemyLoc;  // 몬스터의 X좌표
    public float bossLoc;   // boss의 X좌표
    public float myLocY;    // boss의 y값
    public bool bossMoving;  // boss가 움직이도록 rock 풂

    public bool Enemy_Left; // 적의 방향
    public bool Hit_Set;    // 몬스터를 깨우는 변수
    public float boarLoc;    // 멧돼지의 X 현재위치 

    public GameObject hiteff;  // 히트 이펙트 
    public GameObject blood;   // 출혈 폭발 이펙트
    Transform hit_bloodTrans; // 히트/출혈폭발 이펙트 위치
    
    Transform soulSpawn;    // 보스 바닥 터뜨리기 생성 위치
    Transform soulSpawn1;   // 보스 바닥 터뜨리기 생성 위치
    Transform soulSpawn2;   // 보스 바닥 터뜨리기 생성 위치
    Transform PbSpawn;    // 보스가 던지는 돌 생성 위치

    public GameObject SoulFloor; // 보스 영혼 공격
    public GameObject Rock; // 보스가 던지는 돌
    public GameObject CaninePb; // 보스가 던지는 몬스터
    public GameObject Split_Slime;  // 분열 슬라임
    public GameObject fire; // 프리펩 투사체
    public GameObject ProObject;    // 클론 투사체

    Transform spawn;    // 분열된 슬라임 생성될 위치 1
    Transform spawn2;   // 분열된 슬라임 생성될 위치 2
    public Transform PObject;    // 투사체 생성 위치
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SpriteRenderer orcWaringmark;
    SpriteRenderer sprite;  // Nec 보스의 위험마크
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    BoxCollider2D Box;
    Transform posi;
    Transform transformPosition;
    BoxCollider2D BoxCollider2DSize;

    BoxCollider2D Boxs;
    BoxCollider2D bossBox;  // 보스 공격 콜라이더 켰다 끄는 변수
    BoxCollider2D BossSpriteBox;    // 보스 이미지 콜라이더

    public Transform Pos;
    public Arrow arrow;
    public Effect slash;

    public int BossPage;
    public List<int> GolemPattern = new List<int>{ 1, 2, 3, 4, 5, 6 };
    public List<int> Pattern = new List<int>(6);
    public int LastPattern = 1;
    public Transform[] TeleportPos;
    public Transform[] ThornPos;
    public Transform[] LaserPos;
    public GameObject CircleMissilePB;
    public GameObject MissilePB;
    public GameObject ThornPB;
    public GameObject LaserPB;
    public GameObject MagicArrowPB;
    public GameObject PunchEfc;
    public Animator TeleportEfc;
    float[] angles0 = { -135f, -112.5f, -90f, -67.5f, -45f };
    float[] angles1 = { -123.75f, -101.25f, -78.75f, -56.25f };
    float[] CircleAngles = { 0f, 15f, 30f, 45f, 60f, 75f, 90f, 105f, 120f, 135f, 150f, 165f, 180f, 195f, 210f, 225f, 240f, 255f, 270f, 285f, 300f, 315f, 330f, 345f };
    float[] MagicArrowAngles = { 0f, 30f, 60f, 90f, 120f, 150f, 180f, 210f, 240f, 270f, 300f, 330f};
    public int MissileCount;
    public int CircleMissileCount = 0;
    public int ThornCount = 0;
    public bool CircleMissileShoot;
    public int PunchCount = 1;
    public int MagicArrowCount = 0;
    public Animator PunchCharge;
    public Animator Thunder;
    public RuntimeAnimatorController[] PageAnimators;
    public SpriteRenderer[] WaringArea;
    public float TargetFind; //좌우 구별
    public GameObject BossCenter;

    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수(추상)

    public void Start()
    {
        player = Player.instance.GetComponent<Player>();
    }

    public virtual void Short_Monster(Transform target) 
    { 
        weaponTag = Player.playerTag;
        playerLoc = target.position.x;
        enemyLoc = this.gameObject.transform.position.x;
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x); //X축 거리 계산
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y); //Y축 거리 계산
        Sensing(target, rayHit);
        Sensor();
        Swordlevel = player.proLevel;
        selectWeapon = player.proSelectWeapon;
        bleedingDamage = player.bleedDamage;
        bloodBoomDmg = Player.bloodBoomDmg;
        if (bleedingTime >= 0)
        {
            bleedingTime -= Time.deltaTime;
        }
        if (nextDirX != 0)   // 특정 몬스터에만 Run 애니메이션이 있기 때문에 지정해줘야 함
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
        weaponTag = Player.playerTag;
        Swordlevel = player.proLevel;
        selectWeapon = player.proSelectWeapon;
        bleedingDamage = player.bleedDamage;
        bloodBoomDmg = Player.bloodBoomDmg;
        playerLoc = target.position.x;
        bossLoc = this.gameObject.transform.position.x;
        if (bleedingTime >= 0)
        {
            bleedingTime -= Time.deltaTime;
        }

        if (this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            bossMove();
            BossAtk();
        }
    }

    public virtual void OrcBoss(Transform target)
    {
        weaponTag = Player.playerTag;
        Swordlevel = player.proLevel;
        selectWeapon = player.proSelectWeapon;
        bleedingDamage = player.bleedDamage;
        bloodBoomDmg = Player.bloodBoomDmg;
        if (bleedingTime >= 0)
        {
            bleedingTime -= Time.deltaTime;
        }

        if (this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            if (animator.GetBool("Idle") == true)
            {
                Enemy_Speed = 0f;
            }
            if (animator.GetBool("Idle") == false)
            {
                Enemy_Speed = 1f;
            }
            orcMove();
            OrcAttack();
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("Dieenemy"))
        {
            animator.SetBool("Idle", true);
            Enemy_Speed = 0f;
        }
        orcDie();
    }

    public virtual void Boar(Transform target)  // boar용
    {
        if (bleedingTime >= 0)
        {
            bleedingTime -= Time.deltaTime;
        }
        weaponTag = Player.playerTag;
        playerLoc = target.position.x;
        boarLoc = this.gameObject.transform.position.x;
        Swordlevel = player.proLevel;
        selectWeapon = player.proSelectWeapon;
        bleedingDamage = player.bleedDamage;
        bloodBoomDmg = Player.bloodBoomDmg;
        StartCoroutine(boarMove());
    }
    
    public virtual void onetime()   // Awake에 적용
    {
        Enemy_HPten = Enemy_HP * 0.1f;
        bleedingTime = 0f;
        hit_bloodTrans = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        Pos = GetComponent<Transform>();
        Think();
        if(Enemy_Mod == 7)
        {
            PObject = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        }
        bleeding();
    }

    public virtual void bossOnetime()   // boss용 Awake문
    {
        Enemy_HPten = Enemy_HP * 0.1f;
        bleedingTime = 0f;
        PObject = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        BossSpriteBox = this.gameObject.GetComponent<BoxCollider2D>();
        bossBox = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Pos = GetComponent<Transform>();
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        sprite = this.gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>();
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        hit_bloodTrans = this.gameObject.transform.GetChild(1).GetComponent<Transform>();

        soulSpawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        soulSpawn1 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        soulSpawn2 = this.gameObject.transform.GetChild(4).GetComponent<Transform>();

        randomAtk();
        bleeding();
    }

    public virtual void orcbossOnetime()
    {
        Pos = GetComponent<Transform>();
        BossSpriteBox = this.gameObject.GetComponent<BoxCollider2D>();
        bossBox = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        orcWaringmark = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        hit_bloodTrans = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        PbSpawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        Enemy_HPten = Enemy_HP * 0.1f;
        bleedingTime = 0f;
        Invoke("OrcRandomAtk", 2f);
        bleeding();
    }

    public virtual void boarOntime()
    {
        Enemy_HPten = Enemy_HP * 0.1f;
        bleedingTime = 0f;
        Hit_Set = false;    // 플레이어에게 맞지 않은 상태
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        hit_bloodTrans = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Pos = GetComponent<Transform>();
        bleeding();
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
            else if(collision.tag == "Skill_arrow")
            {
                arrow = collision.GetComponent<Arrow>();
                if (arrow != null)
                {
                    Pdamage = arrow.SkillDmg;
                    StartCoroutine(Hit(Pdamage));
                }
            }
            else if (collision.tag == "Slash")
            {
                slash = collision.GetComponent<Effect>();
                if (slash != null)
                {
                    Pdamage = slash.Dmg;
                    if (bleedLevel <= 5 && player.proLevel > 0)
                        bleedLevel += 1;
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
            if (Enemy_Mod == 2)
            {
                if (spriteRenderer.flipX == true)   // 이미지 플립했을 때 공격 범위 x값 전환 조건문
                {
                    posi.localPosition = new Vector3(atkFlipx, atkY);   // 몬스터의 공격 콜라이더 박스의 x좌표와 y좌표
                }
                else if (spriteRenderer.flipX == false) // 왼쪽을 볼 때
                {
                    posi.localPosition = new Vector3(atkX, atkY);  // 몬스터의 공격 콜라이더 박스의 -x좌표와 y좌표
                }
            }
            else if (Enemy_Mod == 3)
            {
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

    void Think() // 자동으로 다음 방향을 정하는 코루틴
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
        Invoke("Think", nextThinkTime);
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
        Think();
    }

    public void bleeding()  // 도트데미지 주는 함수
    {
        if(bleedingTime > 0)
        {
            if (selectWeapon == 0 && Swordlevel > 0 && Enemy_HP > 0)
            {
                float damage = (bleedLevel * player.bleedDamage);
                this.GetComponentInChildren<EnemyUi>().ShowBleedText(damage); //윤성권 추가함 출혈딜
                Enemy_HP -= damage; // 체력을  출혈스택 * 출혈 데미지로 감소
                player.TotalDamaged += damage;
                if (Swordlevel > 1 && Enemy_HP <= Enemy_HPten && AmIBoss == false)  // 검 숙련도가 1 이상 일정 체력 이하의 몬스터가 출혈 중이면 즉사(보스 제외)
                {
                    Enemy_HP = 0f;
                }
            }
        }
        else if(bleedingTime < 0)
        {
            bleedLevel = 0;
        }
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            StartCoroutine(Die());
            if(this.gameObject.layer == LayerMask.NameToLayer("Dieenemy"))
            {
                return;
            }
        }
        Invoke("bleeding", 1f);
    }

    public void StackBleed()
    {
        if (selectWeapon == 0 && Swordlevel > 0)  // 플레이어가 출혈 업글했을 경우
        {
            if (bleedLevel <= 5)     // 출혈스택 6까지 쌓임
            {
                bleedLevel++;
            }
            bleedingTime = Player.BleedingTime;
        }
    }
    public IEnumerator Hit(float damage) // 피해 함수
    {
        Player player = Player.instance.GetComponent<Player>();
        Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>(); //윤성권 추가함
        Proficiency_ui pro = GameManager.Instance.GetComponent<Proficiency_ui>(); // 숙련도 추가함
        damage = damage * player.DmgIncrease; //딜 증가 추가
        if (player.UseRedCard)
        {
            float randNum = Random.Range(0.01f, 3.33f);
            damage *= randNum;
        }
        bool cc = false; // 추가
        if (player.CCGetRandomResult()) //치명타 계산 추가
        {
            damage *= player.CriDmgIncrease;
            cc = true;
        }
        if (player.CanlifeStill)
        {
            ui.Heal(player.lifeStill * damage);
        }
        posi = this.gameObject.GetComponent<Transform>();
        enemyHit = true;
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();
        Enemy_HP -= damage;
        player.TotalDamaged += damage;
        if (weaponTag == "Sword")
        {
            StackBleed();
        }
        if (AmIBoss)
        {
            GameManager.Instance.GetComponent<BossHpController>().BossHit(damage);
        }
        this.GetComponentInChildren<EnemyUi>().ShowDamgeText(damage,cc); //딜 폰트
        if(Enemy_Mod == 11)
        {
            Hit_Set = true;
            StartCoroutine(Rush());
        }

        if (Enemy_HP > 0) // Enemy의 체력이 0 이상일 때
        {
            if (!animator.GetBool("Hit") && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
            {
                hitEff();
                if (Enemy_Mod != 1 && Enemy_Mod != 3 && Enemy_Mod != 4 && Enemy_Mod != 6 && Enemy_Mod != 11)
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
        else if(Enemy_HP < 0)
        {
            StartCoroutine(Die());
            pro.GetProExp(Stage);
            ui.GetExp(Stage);
            ui.GetGold(Stage);
            player.EnemyKillCount++;
        }

        enemyHit = false;
    }
    public IEnumerator Die()    // 죽는 코루틴
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (Enemy_HP <= 0 && Enemy_Mod != 3 && Enemy_Mod != 11 && Enemy_Mod != 6 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy")) // Enemy의 체력이 0과 같거나 이하일 때(죽음)
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
            if (Enemy_Mod != 11)
            {
                animator.SetTrigger("Die");
            }
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            enemyHit = false;
            if (Enemy_Mod == 9 && posi.localScale.y == 1f)   // 분열 몬스터일 경우
            {
                StartCoroutine(Split());
                this.gameObject.SetActive(false);
                Invoke("enemyDestroy", 0.5f);
            }
            else if (Enemy_Mod == 9 && posi.localScale.y < 1f)
            {
                enemyDestroy();
                if(Enemy_Mod == 9 && posi.localScale.y == 1f)
                {
                    enemyDestroy();
                }
            }
            else if (Enemy_Mod != 9)
            {
                enemyDestroy();
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
            enemyDestroy();
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 6 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))  // Orc보스 죽음
        {
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;
            for (int i = 0; i < 10; i++)
            {
                // 스프라이트 블링크
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.3f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.3f);
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            enemyDestroy();
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 11 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            animator.SetBool("Rush", false);
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;

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
            enemyDestroy();
        }
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
        transformPosition = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        BoxCollider2DSize = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
       
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(transformPosition.position, BoxCollider2DSize.size, 0);
        
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player")
            {
                collider.GetComponent<Player>().Playerhurt(Enemy_Power, Pos.position);
            }
        }
    }
    public void Bump()      // 충돌 데미지 함수
    {
        Vector3 vector3;
        if(Enemy_Mod == 6)
        {
            vector3 = new Vector3(2f, 0.8f);
        }
        else
        {
            vector3 = new Vector3(0f, 0f);
        }
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(Pos.position + vector3, Boxs.size, 0);
        Player player = GetComponent<Player>();
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
                collider.GetComponent<Player>().Playerhurt(Bump_Power, Pos.position);
        }
    }

    private void OnDrawGizmos() // Bump()가 적용되는 위치를 그리는 함수
    {
        Pos = GetComponent<Transform>();
        Boxs = GetComponent<BoxCollider2D>();
        Vector3 vector3;
        if (Enemy_Mod == 6)
        {
            vector3 = new Vector2(2f, 0.8f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Pos.position + vector3, Boxs.size);
        }
        else if(Enemy_Mod != 6)
        {
            vector3 = new Vector2(0f, 0f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Pos.position + vector3, Boxs.size);
        }
    }

    public IEnumerator Boom()  // 폭발 함수
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        animator.SetBool("Attacking", true);
        Attacking = true;
        yield return new WaitForSeconds(0.5f);
        GiveDamage();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        enemyDestroy();
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

    public void enemyDestroy()
    {
        Destroy(this.gameObject); 
    }

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
    public void rockSpawning()
    {
        GameObject rock = Instantiate(Rock, PbSpawn.position, PbSpawn.rotation);
        GameObject rock1 = Instantiate(Rock, PbSpawn.position, PbSpawn.rotation);
        GameObject rock2 = Instantiate(Rock, PbSpawn.position, PbSpawn.rotation);

        Rock_Eff rE = rock.GetComponent<Rock_Eff>();
        Rock_Eff rE1 = rock1.GetComponent<Rock_Eff>();
        Rock_Eff rE2 = rock2.GetComponent<Rock_Eff>();

        rE.distance = 1f;
        rE.Time = endTime;
        rE.Power = Enemy_Power;
        rE.Dir = nextDirX;

        rE1.distance = 5f;
        rE1.Time = endTime;
        rE1.Power = Enemy_Power;
        rE1.Dir = nextDirX;

        rE2.distance = 9f;
        rE2.Time = endTime;
        rE2.Power = Enemy_Power;
        rE2.Dir = nextDirX;
    }

    public void canineSpawning()    // 보스가 몬스터를 던지는 함수
    {
        GameObject canine = Instantiate(CaninePb, PbSpawn.position, PbSpawn.rotation);
        canine.transform.eulerAngles = new Vector3(0, 0, 10); // 발사각 정하기

        GameObject canine1 = Instantiate(CaninePb, PbSpawn.position, PbSpawn.rotation);
        canine1.transform.eulerAngles = new Vector3(0, 0, 15);

        GameObject canine2 = Instantiate(CaninePb, PbSpawn.position, PbSpawn.rotation);
        canine2.transform.eulerAngles = new Vector3(0, 0, 20);

        CaninePb CPb = canine.GetComponent<CaninePb>();
        CaninePb CPb1 = canine1.GetComponent<CaninePb>();
        CaninePb CPb2 = canine2.GetComponent<CaninePb>();
        
        CPb.Power = Enemy_Power;
        CPb.Dir = nextDirX;
        CPb.Time = 7f;

        CPb1.Power = Enemy_Power;
        CPb1.Dir = nextDirX;
        CPb1.Time = 7f;

        CPb2.Power = Enemy_Power;
        CPb2.Dir = nextDirX;
        CPb2.Time = 7f;
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
    public void hitEff()    // 피격 이펙트 
    {
        GameObject hitEff = Instantiate(hiteff, hit_bloodTrans.position, hit_bloodTrans.rotation, hit_bloodTrans);
        scaleX = this.gameObject.transform.localScale.x;    // 오브젝트의 scale.x 값을 받아와서 -인 경우에도 이펙트가 정방향으로 뜰 수 있도록하는 변수
        hitEFF hitEFF = hitEff.GetComponent<hitEFF>();
        hitEFF.dir = nextDirX;
        hitEFF.scalX = scaleX;
    }

    public void bleedEff()  // 출혈 이펙트 터뜨리는 함수
    {
        GameObject bloodEff = Instantiate(blood, hit_bloodTrans.position, hit_bloodTrans.rotation, hit_bloodTrans);
        bloodEFF bloodEFF = bloodEff.GetComponent<bloodEFF>();
        bloodEFF.dir = nextDirX;    // 몬스터의 방향값
        bloodEFF.scalX = scaleX;    // 몬스터의 scale.x 값(-가 되어있는 경우가 있음)
        float Damage = bloodBoomDmg * bleedLevel;
        Damage = Damage * player.DmgIncrease; //딜 증가 추가
        if (player.UseRedCard)
        {
            float randNum = Random.Range(0.01f, 3.33f);
            Damage *= randNum;
        }
        Enemy_HP -= Damage;
        player.TotalDamaged += Damage;
        this.GetComponentInChildren<EnemyUi>().ShowBleedText(Damage);
        bleedLevel = 0;
        bleedingTime = 0;
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
        onSpriteNec();
        if (playerLoc < bossLoc) 
        {
            this.gameObject.transform.localPosition = new Vector2(playerLoc + 3f, myLocY);
            sprite.transform.transform.localPosition = new Vector2(-8, 0.2f);
        }
        else if (playerLoc > bossLoc)
        {
            this.gameObject.transform.localPosition = new Vector2(playerLoc - 3f, myLocY);
            sprite.transform.transform.localPosition = new Vector2(8, 0.2f);
        }
        Invoke("offSpriteNec", 0.5f);
        animator.SetTrigger("Spawn");
        bossMoving = false;
        turning = false;
        Invoke("locBox", 0.9f);
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("soulSpawning", 1f);
            Invoke("soulSpawning1", 1.3f);
            Invoke("soulSpawning2", 1.6f);    
        }
    }
    public void locBox() // 보스 공격 콜라이더 위치함수
    {
        if(spriteRenderer.flipX == true)
        {
            bossBox.transform.localPosition = new Vector2(-1.71f, 0);
        }
        else if(spriteRenderer.flipX == false)
        {
            bossBox.transform.localPosition = new Vector2(1.71f, 0);
        }
        onBox();
        offBox();
    }
    public void onBox() // 보스 공격 콜라이더 on 함수
    {
        bossBox.enabled = false;
        GiveDamage();
    }
    public void offBox()
    {
        bossBox.enabled = true;
    }
    void onSpriteNec()  // Nec용
    {
        sprite.enabled = true;
    }
    void offSpriteNec() // Nec용
    {
        sprite.enabled = false;
    }
    void onSprite() // orc용
    {
        if(atkPattern < 5)
        {
            orcWaringmark.enabled = true;
        }
    }
    void offSPrite() // orc용
    {
        orcWaringmark.enabled = false;
    }

    void OrcRandomAtk()
    {
        int randNum;
        randNum = Random.Range(4, 5); 
        atkPattern = Random.Range(1, 7);     // 패턴 번호를 1 ~ 6까지 랜덤으로 뽑음.
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("OrcRandomAtk", randNum);
        }
    }

    void orcMove()  // Orc 보스의 오른쪽으로 움직이는 함수
    {
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * Enemy_Speed);
    }
    void orcDie()   // Orc 보스의 죽는 애니메이션
    {
        if (Dying)
        {
            BossSpriteBox.enabled = false;
            rigid.isKinematic = true;
            gameObject.transform.Translate(Vector2.down * Time.deltaTime * 5);
        }
    }

    void OrcAttack()    // orc 보스의 공격 패턴
    {
        switch (atkPattern)
        {
            case 1:
                onSprite();
                Invoke("Left_Hooking", 1f);
                Invoke("GiveDamage", 1.7f);
                atkPattern = 0;
                break;
            case 2:
                onSprite();
                Invoke("Right_Hooking", 1f);
                Invoke("GiveDamage", 1.9f);
                atkPattern = 0;
                break;
            case 3:
                onSprite();
                Invoke("Left_Hooking", 1f);
                Invoke("GiveDamage", 1.7f);
                atkPattern = 0;
                break;
            case 4:
                onSprite();
                Invoke("Right_Hooking", 1f);
                Invoke("GiveDamage", 1.9f);
                atkPattern = 0;
                break;
            case 5:
                onSprite();
                Invoke("Left_Hooking", 1f);
                Invoke("GiveDamage", 1.7f);
                Invoke("rockSpawning", 1.7f);
                StartCoroutine(recoil());
                atkPattern = 0;
                break;
            case 6:
                onSprite();
                Invoke("Right_Hooking", 1f);
                Invoke("GiveDamage", 1.9f);
                Invoke("canineSpawning", 1.9f);
                StartCoroutine(recoil());
                atkPattern = 0;
                break;
            default:
                return;
        }
    }

    void Left_Hooking()
    {
        Enemy_Speed = 1f;
        animator.SetTrigger("Left_Hooking");
        Invoke("offSPrite", 0.4f);
        Attacking = false;  // 공격중 끄기
    }

    void Right_Hooking()
    {
        Enemy_Speed = 1f;
        animator.SetTrigger("Right_Hooking");
        Invoke("offSPrite", 0.2f);
        Attacking = false;  // 공격 중 끄기.
    }
    IEnumerator recoil()    // 공격 반동으로 잠시 멈추는 함수
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Idle", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Idle", false);
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
        }
        else if (Enemy_Left == false)
        {
            Enemy_Left = true;
        }
        yield return null; 
        Hit_Set = false;
    }


    public virtual void GolemBoss(Transform target)
    {
        weaponTag = Player.playerTag;
        Swordlevel = player.proLevel;
        selectWeapon = player.proSelectWeapon;
        bleedingDamage = player.bleedDamage;
        bloodBoomDmg = Player.bloodBoomDmg;

        if (Enemy_HP < 600 && BossPage < 1)
        {
            BossPage++;
            bossBox.enabled = false;
            CancelInvoke();
            WaringArea[0].enabled = false;
            Thunder.SetTrigger("Thunder");
            animator.SetTrigger("ChangePage2");
            Invoke("ChangePage", 4f);
            bossMoving = true;
        }

        if (Enemy_HP < 300 && BossPage < 2)
        {
            BossPage++;
            bossBox.enabled = false;
            CancelInvoke();
            WaringArea[0].enabled = false;
            Thunder.SetTrigger("Thunder");
            animator.SetTrigger("ChangePage3");
            Invoke("ChangePage", 6f);
            bossMoving = true;
        }

        GolemDie();

        if (bossMoving)
        {
            // 목표 방향 계산
            Vector3 targetDirection = (TeleportPos[0].position - transform.position).normalized;
            rigid.velocity = targetDirection * 7.5f;

            // 오브젝트 A가 목표 위치에 도달한 경우
            if (Vector3.Distance(transform.position, TeleportPos[0].position) < 0.1f)
            {
                rigid.velocity = Vector3.zero;
                bossMoving = false;
            }
        }

        if (bleedingTime >= 0)
        {
            bleedingTime -= Time.deltaTime;
        }

        if (this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            if (animator.GetBool("Idle") == true)
            {
                Enemy_Speed = 0f;
            }
            if (animator.GetBool("Idle") == false)
            {
                Enemy_Speed = 1f;
            }
            GolemMove();
            GolemAttack();
        }
        else if (this.gameObject.layer == LayerMask.NameToLayer("Dieenemy"))
        {
            animator.SetBool("Idle", true);
            Enemy_Speed = 0f;
        }
    }

    public virtual void GolemBossOneTime()
    {
        Pos = GetComponent<Transform>();
        BossSpriteBox = this.gameObject.GetComponent<BoxCollider2D>();
        bossBox = this.GetComponent<BoxCollider2D>();
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
        hit_bloodTrans = this.gameObject.transform.GetChild(1).GetComponent<Transform>();
        Enemy_HPten = Enemy_HP * 0.1f;
        bleedingTime = 0f;
        Invoke("GolemRandomAtk", 2f);
        bleeding();
    }

    void GolemRandomAtk()
    {
        if (Dying)
        {
            return;
        }
        int randNum;
        if (BossPage < 1)
        {
            Pattern = new List<int> { 1, 2, 3};
        }
        else if (BossPage == 1)
        {
            Pattern = new List<int> { 1, 2, 3, 4};
        }
        else if (BossPage > 1)
        {
            Pattern = new List<int> { 1, 2, 3, 4, 5 ,6};
        }
        Pattern.RemoveAt(LastPattern - 1);
        randNum = Random.Range(4, 5);
        atkPattern = Pattern[Random.Range(0, Pattern.Count)];     // 패턴 번호를 1 ~ 6(Max-1)까지 랜덤으로 뽑음.
        if (this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("GolemRandomAtk", randNum);
        }
    }

    void GolemMove()  // 골렘의 오른쪽으로 움직이는 함수
    {
        
    }
    void GolemDie()   // 골렘의 죽는 애니메이션
    {
        if (Enemy_HP <= 0 && !Dying)
        {
            animator.runtimeAnimatorController = PageAnimators[1];
            Dying = true;
            bossBox.enabled = false;
            animator.SetTrigger("Die");
        }
    }

    void GolemAttack()    // 보스 공격 패턴
    {
        switch (atkPattern)
        {
            case 1:
                ScaleFlip();
                float range = 11;
                if (TargetFind > 0) //왼쪽으로 감
                {
                    range *= -1;
                }
                else if (TargetFind < 0) //오른쪽으로 감
                {
                    range *= 1;
                }
                Vector3 vc = new Vector3(player.transform.position.x + range, player.transform.position.y + 1);
                this.transform.position = vc;
                animator.SetTrigger("Punch");
                WaringArea[0].enabled = true;
                TeleportEfc.SetTrigger("Teleport");
                PunchCharge.SetTrigger("Charging");
                Invoke("Punch", 1f);
                atkPattern = 0;
                LastPattern = 1;
                break;
            case 2:
                ScaleFlip();
                TeleportEfc.SetTrigger("Teleport");
                this.transform.position = TeleportPos[1].position;
                animator.SetTrigger("Attacking");
                Invoke("WingAttack", 0.6f);
                atkPattern = 0;
                LastPattern = 2;
                break;
            case 3:
                Invoke("CircleMissile", 1f);
                atkPattern = 0;
                LastPattern = 3;
                break;
            case 4:
                Invoke("ThornAttack", 0f);
                atkPattern = 0;
                LastPattern = 4;
                break;
            case 5:
                WaringArea[1].enabled = true;
                WaringArea[2].enabled = true;
                this.transform.position = TeleportPos[0].position;
                TeleportEfc.SetTrigger("Teleport");
                animator.SetTrigger("Laser");
                Invoke("LaserAttack", 0f);
                atkPattern = 0;
                LastPattern = 5;
                break;
            case 6:
                Invoke("MagicArrowAttack", 0f);
                atkPattern = 0;
                LastPattern = 6;
                break;
            default:
                return;
        }
    }

    void ScaleFlip()
    {
        TargetFind = this.transform.position.x - player.transform.position.x;
        if (TargetFind > 0) //왼쪽으로 감
        {
            Vector3 newLocalScale = new Vector3(1, 1, 1);
            this.transform.localScale = newLocalScale;
        }
        else if (TargetFind < 0) //오른쪽으로 감
        {
            Vector3 newLocalScale = new Vector3(-1, 1, 1);
            this.transform.localScale = newLocalScale;
        }
    }

    void Punch()
    {
        Attacking = false;  // 공격중 끄기
        float time = 0f;
        WaringArea[0].enabled = false;
        for (int i = 0; i < 5; i++)
        {
            Invoke("ChargingPunch", time);
            time += 0.2f;
        }
        PunchCount = 1;
    }

    void WingAttack()
    {
        Attacking = false;  // 공격중 끄기
        float time = 0f;
        for (int i = 0; i < 5; i++)
        {
            Invoke("MissileCreate", time);
            time += 0.4f;
        }
        MissileCount = 0;
    }

    void MissileCreate()
    {
        float[] angles;
        if (MissileCount % 2 == 0)
        {
            angles = angles0;
        }
        else
        {
            angles = angles1;
        }

        for (int i = 0; i < angles.Length; i++)
        {
            float angle = angles[i];
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject missile = Instantiate(MissilePB, transform.position, rotation);
        }
        MissileCount++;
    }

    void ChargingPunch()
    {
        int px;
        int py;
        px = PunchCount * 6;
        if (PunchCount % 2 == 0)
        {
            py = 1;
        }
        else
        {
            py = -1;
        }
        if (TargetFind > 0)
        {
            px *= 1;
        }
        if (TargetFind < 0)
        {
            px *= -1;
        }
        Vector3 Pc = new Vector3(this.transform.position.x + px, this.transform.position.y + py);
        GameObject punch = Instantiate(PunchEfc, transform);
        punch.transform.position = Pc;
        PunchCount++;
    }

    void CircleMissile()
    {
        animator.SetTrigger("Circle");
        CircleMissileShoot = false;
        Attacking = false;  // 공격중 끄기
        float time = 0f;
        for (int i = 0; i < CircleAngles.Length; i++)
        {
            if (BossPage >= 1)
            {
                Invoke("CircleMissileCreatePage2", time);
            }
            Invoke("CircleMissileCreate", time);
            time += 0.025f;
        }
        Invoke("Shoot", 1f);
        CircleMissileCount = 0;
    }

    void CircleMissileCreate()
    {
        float circleRadius = 2f;
        
        float angle = CircleMissileCount * (360f / CircleAngles.Length); // 각도 계산
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // 원의 둘레 위의 위치 계산

        Quaternion rotation = Quaternion.Euler(0f, 0f, CircleAngles[CircleMissileCount]);

        GameObject missile = Instantiate(CircleMissilePB, spawnPosition, rotation);
        missile.GetComponent<CircleMissile>().boss = this;
        CircleMissileCount++;
    }

    void CircleMissileCreatePage2()
    {
        float circleRadius = 3f;

        float angle = CircleMissileCount * (360f / CircleAngles.Length); // 각도 계산
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // 원의 둘레 위의 위치 계산

        Quaternion rotation = Quaternion.Euler(0f, 0f, CircleAngles[CircleMissileCount] + 15f);

        GameObject missile = Instantiate(CircleMissilePB, spawnPosition, rotation);
        missile.GetComponent<CircleMissile>().boss = this;
        missile.GetComponent<CircleMissile>().moveSpeed = 5f;
    }

    void Shoot()
    {
        CircleMissileShoot = true;
    }

    void ThornAttack()
    {
        if (BossPage < 1)
        {
            return;
        }
        animator.SetTrigger("Thorn");
        Attacking = false;  // 공격중 끄기
        float time = 0f;
        for (int i = 0; i < 2; i++)
        {
            Invoke("ThornCreate", time);
            time += 0.7f;
        }
        ThornCount = 0;
    }

    void ThornCreate()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject Thorn = Instantiate(ThornPB, ThornPos[ThornCount + (i * 2)].position, Quaternion.identity);
        }
        ThornCount++;
    }

    void LaserAttack()
    {
        Attacking = false;  // 공격중 끄기
        Invoke("LaserCreate", 2f);
    }

    void LaserCreate()
    {
        Transform CreatePos;
        Vector3 NewPos;
        GameObject newLaser;
        for (int i = 0; i < 2; i++)
        {
            WaringArea[1+i].enabled = false;
            CreatePos = LaserPos[i];
            NewPos = new Vector3(CreatePos.transform.position.x, CreatePos.transform.position.y, CreatePos.transform.position.z);
            newLaser = Instantiate(LaserPB, NewPos, Quaternion.Euler(0f, 0f, 0f));
            if (i>0)
            {
                newLaser.GetComponent<Laser>().Dir = -1;
            }
        }
    }

    void MagicArrowAttack()
    {
        animator.SetTrigger("MagicArrow");
        Attacking = false;  // 공격중 끄기
        float time = 0f;
        for (int i = 0; i < MagicArrowAngles.Length; i++)
        {
            Invoke("MagicArrowCreate", time);
            time += 0.05f;
        }
        MagicArrowCount = 0;
    }

    void MagicArrowCreate()
    {
        float circleRadius = 4f;

        float angle = MagicArrowCount * (360f / MagicArrowAngles.Length); // 각도 계산
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // 원의 둘레 위의 위치 계산

        Quaternion rotation = Quaternion.Euler(0f, 0f, MagicArrowAngles[MagicArrowCount]);

        GameObject Arrow = Instantiate(MagicArrowPB, spawnPosition, rotation);
        MagicArrowCount++;
    }

    void ChangePage()
    {
        if (BossPage == 1)
        {
            animator.runtimeAnimatorController = PageAnimators[BossPage - 1];
            Invoke("GolemRandomAtk", 1f);
            bossBox.enabled = true;
        }
        else if (BossPage == 2)
        {
            animator.runtimeAnimatorController = PageAnimators[BossPage - 1];
            Invoke("GolemRandomAtk", 1f);
            bossBox.enabled = true;
        }

    }
}