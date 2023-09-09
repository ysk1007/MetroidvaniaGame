using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;


public class Player : MonoBehaviour
{
    public float[] ExpBarValue = {
        100f, 150f, 200f, 250f, 300f,
        350f, 400f, 450f, 500f, 550f,
        600f, 650f, 700f, 750f, 800f,
        850f, 900f, 950f, 1000f, 1050f,
        1100f, 1150f, 1200f, 1250f, 1300f,
        1350f, 1400f};   // 레벨업 하는데 필요한 경험치 추가함
    public float Speed; //Move 속도 저장 변수
    public float jumpPower; //Jump 높이 저장 변수
    public float SpeedChange; // Move 속도변경 저장 변수
    public float curTime, coolTime = 2;  // 연속공격이 가능한 시간
    public float[] MasterSkillTime = { 40, 90, 45 };   //무기별 숙련도 스킬 쿨타임
    public float Sword_MsTime, Axe_MsTime, Bow_MsTime;  // 무기별 숙련도 스킬 쿨타임 적용
    public float[] SkillTime = { 12, 20, 10 }; // 무기별 기본스킬 쿨타임
    public float Sword_SkTime, Axe_SkTime, Bow_SkTime;  // 무기별 기본스킬 쿨타임 적용
    public float[] WeaponsDmg = { 0.65f, 1.2f, 0.8f }; //무기별 공격력 비례 칼, 도끼, 활
    public bool isdelay = false;    //공격 딜레이 체크
    public bool isSlide = false;     //슬라이딩 체크
    public bool isGround = true;    //Player가 땅인지 아닌지 체크
    public bool isjump = false;     //점프중인지 체크
    public bool isWallJump = false;
    public bool isSkill = false;    //스킬 확인
    public bool isMasterSkill = false;  //숙련도 스킬 확인
    public bool isAttacking = false; //공격상태 확인
    public bool isShield = false;   //방어막 상태 확인
    public bool isWall = false;     // 벽에 붙어 있는지 확인
    public bool isDie = false;  // 플레이어가 죽었는지 판단    2023-09-03 추가
    public float delayTime = 1f;    //공격 딜레이 기본 시간
    public int WeaponChage = 1;     //무기 변경 저장 변수
    public int JumpCnt, JumpCount = 2;  //2단점프의 수를 카운터 해주는 변수
    public int SwdCnt, AxeCnt;  //공격모션의 순서
    public float Direction; //방향값
    public float attackDash = 4f; //큰 공격시 앞으로 이동하는 값
    public float slideSpeed = 13;   //슬라이딩 속도
    public int slideDir = 1;    //슬라이딩 방향값
    public float MaxHp = 100;    //플레이어 최대 HP
    public float BaseHp = 100;    //플레이어 최대 HP
    public float CurrentHp;    //플레이어 현재 HP
    public bool ishurt = false; //피격 확인
    public bool isknockback = false;    //넉백 확인
    public float Dmg;  // 최종 대미지 변수
    public float ShieldTime; // 도끼 스킬 방어막 지속시간
    public float chargingTime = 5f; // 차징 시간
    public bool isCharging = false; // 차징 상태 여부
    public float chargeTimer = 0f; // 차징 시간을 측정하는 타이머
    public float ArrowDistance = 0.75f;
    public bool Axepro = true;
    public PlayerCanvas playerCanvas; //추가함
    public Vector3 spawnPoint;  // 2023-09-02 추가(플레이어 리스폰 위치)

    public static Player instance; //추가함
    public float ATP; // 플레이어 대미지
    public int level;   // 플레이어 레벨
    public float gold;  //추가함
    public float AtkPower; //추가함
    public float Def = 5; //추가함
    public float CriticalChance; //추가함 , 이속,공속 복구되는거 수정 필요
    public float DmgIncrease = 1f; //추가함
    public float CriDmgIncrease = 1.5f; //추가함
    public float ATS = 1; //추가함
    public float GoldGet = 1f; //추가함
    public float EXPGet = 1f; //추가함
    public bool CanlifeStill = true; //추가함
    public float lifeStill; //추가함
    public float DecreaseCool = 0f; //추가함
    public float LifeRegen = 0f; //추가함
    public float SlidingCool = 2f;
    public int proSelectWeapon = 4; //4는 숙련도를 고르지 않은 상태 0,1,2 => 칼,도끼,활
    public int proLevel = 0;
    public int EnemyKillCount = 0;
    public float TotalGetGold = 0f;
    public float TotalDamaged = 0f;
    public bool FirstMaterial = false;
    public bool SecondMaterial = false;
    public bool ThirdMaterial = false;
    public float enemyPower;
    public int stackbleed;  // 몬스터에 쌓인 출혈 스택
    public int slashBleedStack; 
    public static float BleedingTime = 8f;  // 2023-07-31 추가(출혈 지속 시간)
    public float bleedDamage = 10f; // 2023-08-01 출혈 데미지
    public static float bloodBoomDmg = 66f;  // 출혈스택 터뜨리는 데미지
    public static string playerTag;    // 2023-08-11 추가 (플레이어 무기 태그)
    public List<Collider2D> enemyColliders = new List<Collider2D>();   //공격 몬스터 체크
    public List<Enemy> enemyCheck = new List<Enemy>(); // Enemy 타입 리스트로 변경

    //선택능력치 밸류
    public float[] selectAtkValue = { 0.1f, 0.2f, 0.3f };
    public float[] selectATSValue = { 0.15f, 0.3f, 0.4f };
    public float[] selectCCValue = { 0.05f, 0.1f, 0.2f };
    public float[] selectLifeStillValue = { 0.005f, 0.007f, 0.01f };
    public float[] selectDefValue = { 10f, 20f, 30f };
    public float[] selectHpValue = { 30f, 60f, 90f };
    public float[] selectGoldValue = { 0.3f, 0.6f, 1.0f };
    public float[] selectExpValue = { 0.3f, 0.6f, 1.0f };
    public float[] selectCoolTimeValue = { 0.05f, 0.1f, 0.2f };

    //선택능력치 레벨
    public int selectAtkLevel = 0;
    public int selectATSLevel = 0;
    public int selectCCLevel = 0;
    public int selectLifeStillLevel = 0;
    public int selectDefLevel = 0;
    public int selectHpLevel = 0;
    public int selectGoldLevel = 0;
    public int selectExpLevel = 0;
    public int selectCoolTimeLevel = 0;

    public GameObject GameManager;  //게임 매니저
    MapManager map;
    public Ui_Controller Ui;  //ui 컨트롤러
    public GameObject attackRange;  //근접공격 위치
    public GameObject Arrow; //화살 오브젝트
    public GameObject Arrow2; //화살 증가 오브젝트
    public GameObject Slash;  // 검 기본스킬 오브젝트
    public GameObject AxeSkill;  //도끼 마스터스킬 오브젝트
    public GameObject BowSkill;  // 활 기본스킬 오브젝트
    public GameObject BowMaster; // 활 숙련도 화살 오브젝트
    public GameObject HolyArrow; // 활 숙련도 화살 오브젝트
    public GameObject Axebuf; // 도끼 숙련도 0렙 패시브 버프

    public Transform Arrowpos; //화살 생성 오브젝트
    public Transform Arrowpos2; //증가된 화살  오브젝트
    public Transform Attackpos;   //공격박스 위치
    public Transform Skillpos;  // 스킬 생성 오브젝트

    public AudioClip SwordAtkSound;
    public AudioClip SwordSkillSound;
    public AudioClip SwordMasterSound;
    public AudioClip AxeAtk1Sound;
    public AudioClip AxeAtk2Sound;
    public AudioClip AxeSkillSound;
    public AudioClip AxeMasterSound;
    public AudioClip BowAtkSound;
    public AudioClip BowSkillSound;
    public AudioClip BowMasterSound;
    public AudioClip DamagedSound;
    public AudioClip JumpSound;
    public AudioClip SlideingSound;
    public AudioClip DieSound;

    public BoxCollider2D box; //근접 공격 범위
    public BoxCollider2D Axebox; //도끼 숙련도 범위
    public SpriteRenderer spriteRenderer;
    public Enemy enemy;
    public MoveCamera movecamera;
    public Loading loading;
    Projective_Body PBody;
    Rigidbody2D rigid;
    public Animator anim;
    new AudioSource audio;

    //특수 아이템 변수
    public bool UseGridSword = false;
    public bool DivinePower = false;
    public bool UsePastErase = false;
    public bool UseRedCard = false;
    public bool NoNockback = false;
    public bool UseMirror = false;
    public bool UseVulcanArmor = false;
    public bool UsePickGloves = false;
    public Camera cam;
    public Animator TimeLoopAnim;
    public AudioSource TimeLoopSound;
    public GameObject PastErase;
    public float GridPower = 0f;
    public float VulcanPower = 0f;

    public GameObject AxeMasterEfc;
    public int SkillCount = 1;
    void Awake()
    {
        instance = this; //추가함
        
        attackRange = transform.GetChild(0).gameObject; // 플레이어의 0번째 오브젝트인 attackRange를 저장
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //시작시 점프 가능 횟수 적용
        SpeedChange = 5;  //시작시 기본 이동속도
        jumpPower = 15; //기본 점프높이
        ATP = 10; // 기본 공격 대미지
        audio = GetComponent<AudioSource>();
        Attackpos = transform.GetChild(0).GetComponentInChildren<Transform>(); //attackRange의 위치값을 pos에 저장
        Arrowpos = transform.GetChild(1).GetComponentInChildren<Transform>(); //Arrowpos의 위치값을 pos에 저장
        Arrowpos2 = transform.GetChild(2).GetComponentInChildren<Transform>(); //Arrowpos2의 위치값을 pos에 저장
        Skillpos = transform.GetChild(3).GetComponentInChildren<Transform>(); //Skillpos의 위치값을 pos에 저장
        Ui = GameManager.GetComponent<Ui_Controller>();
    }

    void Start() //추가함
    {
        movecamera = MoveCamera.instance.GetComponent<MoveCamera>();
        DataManager dm = DataManager.instance;
        loading = Loading.instance; // 09-04 추가함
        map = MapManager.instance;
        dm.JsonLoad("PlayerData");
        dm.JsonLoad("ItemData");
        anim.SetFloat("AttackSpeed", ATS);
        OptionManager.instance.Playing = true;
        Invoke("HpRegen", 1f);
        AxePro2();
    }

    void Update()
    {
        playerTag = this.gameObject.transform.GetChild(0).tag;
        if (map.pause || isDie || loading.DoLoading)
        {
            return;
        }
        else
        {
            Player_Move();  //Player의 이동, 점프, 속도 함수
            Player_Attack();    //Player의 공격 함수
        }
        if (UseGridSword) //추가함
        {
            GridsSword();
        }
        if(this.gameObject.transform.position.y < -30)
        {
            PlayerReposition();
        }
        AxePro2();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Attackpos.position, box.size);
    }
    void Player_Move() //Player 이동, 점프
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // 좌우 방향값을 정수로 가져오기
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill && !isMasterSkill && movecamera.startFightBoss == false)    //공격 딜레이중일시 이동 불가능
        {
            Speed = SpeedChange;
            Transform AtkRangeTransform = transform.GetChild(0);   // AttackRange 위치값 변경을 위해 자식오브젝트 위치값 불러옴
            anim.SetBool("Player_Walk", true);
            if (Direction < 0) //왼쪽 바라보기
            {
                spriteRenderer.flipX = false;
                transform.Translate(new Vector2(-1, 0) * Speed * Time.deltaTime);
                slideDir = -1;
                AtkRangeTransform.localPosition = new Vector3(-1, 0); // AttackRange 위치값 변경
            }
            else if (Direction > 0) //오른쪽 바라보기
            {
                spriteRenderer.flipX = true;
                transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
                slideDir = 1;
                AtkRangeTransform.localPosition = new Vector3(1, 0);
            }
        }
        else
            anim.SetBool("Player_Walk", false);

        //Sliding
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && !isjump && !isdelay && !isSkill && !isWall)
        {
            StartCoroutine(Sliding());
            if (DivinePower)
            {
                Instantiate(HolyArrow, Arrowpos.position, transform.rotation);  //신성화살 발사
            }
        }

        //Jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!Input.GetKey(KeyCode.DownArrow))
                JumpCnt--;

            if (JumpCnt < (JumpCount - 1))
            {
                StopCoroutine("PadJump");
            }
            isGround = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide")) //발판에서 밑으로 점프시 내려가기
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(PadJump(0));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            StartCoroutine(PadJump(1));
        }

        RaycastHit2D rayHitRight = Physics2D.Raycast(rigid.position, Vector3.right, 1.2f, LayerMask.GetMask("Tilemap"));
        RaycastHit2D rayHitLeft = Physics2D.Raycast(rigid.position, Vector3.left, 1.2f, LayerMask.GetMask("Tilemap"));
        if (isWall) // 벽점프
        {
            if (Input.GetKeyDown(KeyCode.Space) && rayHitLeft.collider != null && !isGround && !isWallJump)
            {
                StartCoroutine(PadJump(1));
                isWallJump = true;
                rigid.velocity = new Vector2(1, 2) * 9f;
                spriteRenderer.flipX = true;
                isWall = false;
                slideDir = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && rayHitRight.collider != null && !isGround && !isWallJump)
            {
                StartCoroutine(PadJump(1));
                isWallJump = true;
                rigid.velocity = new Vector2(-1, 2) * 9f;
                spriteRenderer.flipX = false;
                isWall = false;
                slideDir = -1;
            }
        }
    }
    public void Player_Attack() //Player 공격모음
    {
        if (map.pause)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) && proSelectWeapon != 4 && proLevel == 3)    //숙련도 스킬 실행
        {
            if (!anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && !isMasterSkill && !isSkill && !isCharging && !isdelay && !isWall)
            {
                StartCoroutine(MasterSkill());
            }
        }
        if (Sword_MsTime >= 0)   //Sword 숙련도 스킬 쿨타임 
        {
            Sword_MsTime -= Time.deltaTime;
        }
        if (Axe_MsTime >= 0)     //Axe 숙련도 스킬 쿨타임
        {
            Axe_MsTime -= Time.deltaTime;
        }
        if (Bow_MsTime >= 0)     //Bow 숙련도 스킬 쿨타임
        {
            Bow_MsTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill && !isdelay && !isWall)  //기본 스킬 실행
        {
            if (WeaponChage == 1 && Sword_SkTime <= 0 && !anim.GetBool("sword_atk"))    //Sword
            {
                Sword_SkTime = DeCoolTimeCarcul(SkillTime[0]); //스킬쿨 수정함
                isSkill = true;
                SwdCnt = 2;
                anim.SetTrigger("sword_atk");
                anim.SetFloat("Sword", SwdCnt); // 애니메이션에 스킬 실행함수를 넣어뒀음
            }
            if (WeaponChage == 2 && Axe_SkTime <= 0)    //Axe
            {
                isSkill = true;
                StartCoroutine(Skill());
                Axe_SkTime = DeCoolTimeCarcul(SkillTime[1]); //스킬쿨 수정함
            }
            if (WeaponChage == 3 && Bow_SkTime <= 0)    //Bow
            {
                isSkill = true;
                StartCoroutine(Skill());
                Bow_SkTime = DeCoolTimeCarcul(SkillTime[2]); //스킬쿨 수정함
            }
        }
        if (Sword_SkTime >= 0)   //Sword 스킬 쿨타임 
        {
            Sword_SkTime -= Time.deltaTime;
        }
        if (Axe_SkTime >= 0)     //Axe 스킬 쿨타임
        {
            Axe_SkTime -= Time.deltaTime;
        }
        if (Bow_SkTime >= 0)     //Bow 스킬 쿨타임
        {
            Bow_SkTime -= Time.deltaTime;
        }
        if (ShieldTime >= 0) //방어막 지속시간이 끝나면 방어막 해제
        {
            ShieldTime -= Time.deltaTime;
            if (ShieldTime <= 0)
            {
                StartCoroutine(Blink());
            }
        }

        if (Input.GetKey(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill) // Axe 차징 공격
        {
            if (WeaponChage == 2 && proSelectWeapon == 1 && proLevel >= 1)
            {
                if (!isCharging)
                {
                    isCharging = true;
                    chargeTimer = 0f;
                    playerCanvas.ChargeStart(); //도끼 게이지 추가함
                }
                else // 이미 차징 중인 경우
                {
                    chargeTimer += Time.deltaTime;
                    playerCanvas.GuageIncrease(chargeTimer / chargingTime); //도끼 게이지 추가함
                    if (chargeTimer >= chargingTime)
                    {
                        chargeTimer = chargingTime; // 차징 시간이 최대 시간을 넘어가지 않도록 제한
                    }

                }
            }
        }
        else
        {
            isCharging = false;
            playerCanvas.ChargeEnd(); //도끼 게이지 추가함
            if (chargeTimer >= chargingTime && chargeTimer != 0)
            {
                Axe_chargeing();
                StartCoroutine(Attack_delay());
            }

        }

        if (Input.GetKeyUp(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill)    //기본 공격
        {
            if (!isdelay)   //딜레이가 false일때 공격 가능
            {
                if (WeaponChage == 1)    //Sword 공격
                {
                    Sword_attack();
                }
                if (WeaponChage == 2)    //Axe 공격
                {
                    Axe_attack();
                }
                if (WeaponChage == 3)    //Bow 공격
                {
                    isdelay = true;
                    anim.SetTrigger("arrow_atk");
                }
                StartCoroutine(Attack_delay());    //공격후 다음 공격까지 딜레이
            }
        }
        else
        {
            if (curTime >= 0)
                curTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)    // 무기 변경
        {
            if (!isCharging && !isSkill && !isMasterSkill)
            {
                WeaponChage += 1;
                if (WeaponChage == 2)
                {
                    attackRange.tag = "Axe";
                }
                else if (WeaponChage == 3)
                {
                    attackRange.tag = "Arrow";
                    this.transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    attackRange.tag = "Sword";
                    WeaponChage = 1;
                    this.transform.GetChild(0).gameObject.SetActive(true);
                }
                WeaponSwap.instance.DoSwap = true;
            }
        }
    }

    public void AttackDamage()// Player 공격시 적에게 대미지값 넘겨주기
    {
        box = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
        if (box != null)    //공격 범위 안에 null값이 아닐때만
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Attackpos.position, box.size, 0); //공격 범위 안에 콜라이더를 
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && (collider.tag == "Enemy" || collider.tag == "Boss"))
                {
                    enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        StartCoroutine(enemy.Hit(Dmg));
                        Debug.Log(Dmg + "Player");
                        if (proSelectWeapon == 0 && proLevel >= 1)  // 출혈이 있는 몬스터를 리스트에 저장
                        {
                            BoxCollider2D boxCollider = collider.GetComponent<BoxCollider2D>();
                            if (boxCollider != null && (collider.tag == "Enemy" || collider.tag == "Boss"))
                            {
                                enemyColliders.Add(boxCollider);
                                Debug.Log(boxCollider);
                            }
                        }
                    }
                }
            }
        }
    }
    IEnumerator Skill()//스킬 작동시 실행
    {
        GameManager.GetComponent<WeaponSwap>().Skill();
        if (WeaponChage == 1) //sword 스킬
        {
            Transform SkillTransform = transform.GetChild(3);   //검 스킬 오브젝트 위치값 저장
            if (slideDir == 1)
                SkillTransform.localPosition = new Vector3(1, 0.2f);
            else
                SkillTransform.localPosition = new Vector3(-1, 0.2f);

            PlaySound("SwordSkill");
            Instantiate(Slash, Skillpos.position, transform.rotation); // 검기 복사 생성
            yield return new WaitForSeconds(0.2f);
            SwdCnt = 1;
            isSkill = false;
        }
        if (WeaponChage == 2) //Axe 스킬
        {
            this.transform.GetChild(6).gameObject.SetActive(true);  //방어막 이펙트 켜기
            PlaySound("AxeSkill");
            isShield = true;
            ShieldTime = 10f;
            isSkill = false;
        }
        if (WeaponChage == 3) //Arrow 스킬
        {
            anim.SetTrigger("arrow_atk");   //애니메이션에 Bow_Attack 함수 실행이 들어가있음
        }
    }

    void AxePro2()
    {
        if (proLevel > 1 && proSelectWeapon == 1 && Axepro == true) // 도끼 숙련도 1일 때 발동
        {
            StartCoroutine(proSkill());
            SpeedChange -= 0.25f;
            Axepro = false;
        }
    }

    IEnumerator proSkill()
    {
        Vector3 Axepro = new Vector3(this.transform.position.x, this.transform.position.y);
        GameObject Axe = Instantiate(Axebuf, transform.parent);
        Axe.transform.position = Axepro;
        yield return new WaitForSeconds(0.5f);
        Transform trans;
        Vector3 scaleVector = new Vector3(1, 1, 1); // 스케일 값 변화를 저장할 Vector3 변수
        trans = GetComponent<Transform>();
        scaleVector.x = 1.2f;
        scaleVector.y = 1.2f;
        trans.localScale = scaleVector;
        MaxHp += 50;
        CurrentHp += 50;
        Def += 20;
        slideSpeed = 10;
        Destroy(Axe);
    }
    IEnumerator MasterSkill()
    {
        if (WeaponChage == 1 && proSelectWeapon == 0 && Sword_MsTime <= 0) //Sword 숙련도 스킬
        {
            if (enemyColliders != null)
            {
                isMasterSkill = true;
                GameManager.GetComponent<WeaponSwap>().Ult();
                PlaySound("SwordMasterSkill");
                yield return new WaitForSeconds(0.7f);

                enemyCheck = enemyColliders
                    .Where(x => x != null && (x.tag == "Enemy" || x.tag == "Boss") && x.GetComponent<Enemy>() != null && x.GetComponent<BoxCollider2D>() != null) // 존재하면서 "Enemy" 또는 "Boss" 태그인 게임오브젝트를 추출
                    .Select(x => x.GetComponent<Enemy>()) // 추출된 게임오브젝트에서 Enemy 스크립트 컴포넌트를 가져와 리스트에 저장
                    .Where(enemy => enemy != null) // Enemy 컴포넌트가 null이 아닐 경우 필터링
                    .Distinct() // 중복된 Enemy 컴포넌트 제거
                    .ToList();

                foreach (Enemy enemy in enemyCheck) // 감지된 모든 적에게 데미지 입힘
                {
                    stackbleed = enemy.bleedLevel;  //2023 - 08 - 09 추가
                    if (stackbleed > 0)
                    {
                        enemy.bleedEff();
                        BoxCollider2D boxCollider = enemy.GetComponent<BoxCollider2D>();
                        if (boxCollider != null)
                        {
                            enemyColliders.Remove(boxCollider);
                        }
                    }
                }
                Sword_MsTime = DeCoolTimeCarcul(MasterSkillTime[0]);
            }
            else
            {
                Debug.Log("쓸 수 없음");
            }
            isMasterSkill = false;
        }
        if (WeaponChage == 2 && proSelectWeapon == 1 && Axe_MsTime <= 0)    //Axe 숙련도 스킬
        {
            isMasterSkill = true;
            GameManager.GetComponent<WeaponSwap>().Ult();
            AxeCnt = 4;
            anim.SetFloat("Axe", AxeCnt); //숙련도 스킬은 Axe_atk3 길게 애니메이션으로 실행
            anim.SetTrigger("axe_atk");
            yield return new WaitForSeconds(1.5f * delayTime);
            //PlaySound("AxeMasterSkill"); // 애니메이션에 실행 있음
            AxeMasterSkill();
            Axe_MsTime = DeCoolTimeCarcul(MasterSkillTime[1]);
            yield return new WaitForSeconds(1f);
            isMasterSkill = false;
        }
        if (WeaponChage == 3 && proSelectWeapon == 2 && Bow_MsTime <= 0) //Bow 숙련도 스킬
        {
            isMasterSkill = true;
            GameManager.GetComponent<WeaponSwap>().Ult();
            anim.SetTrigger("arrow_atk");
            yield return new WaitForSeconds(0.5f);
            PlaySound("BowMasterSkill");
            Bow_MsTime = DeCoolTimeCarcul(MasterSkillTime[2]);
        }
    }

    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // 벽 콜라이젼이 Player에 닿고 있으면 실행, 점프착지 시 콜라이젼 닿을 시 점프 해제
    {
        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("Tilemap", "Pad", "Water"));
        if (collision.gameObject.tag == "Wall" && rayHitDown.collider != null)
        {
            isWall = false;
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
            Debug.Log("1");
            Debug.Log(rayHitDown.collider);
        }
        else if (collision.gameObject.tag == "Wall" && rayHitDown.collider == null)
        {
            isWall = true;
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
            Debug.Log("2");
            Debug.Log(rayHitDown.collider);
        }


        if ((collision.gameObject.tag == "Pad" || collision.gameObject.tag == "Tilemap") && !isGround)
        {
            if (rayHitDown.collider != null)
            {
                anim.SetBool("Player_Jump", false);
                isWall = false;
                isjump = false;
                isWallJump = false;
                isGround = true;
            }
        }
        if (rayHitDown.collider != null && !anim.GetBool("Sliding"))
        {
            JumpCnt = JumpCount;
        }
    }
    void OnCollisionExit2D(Collision2D collision)   //Player가 벽에 닿지 않을때
    {
        if (collision.gameObject.tag == "Wall") //벽에 붙어서 내려갈 때
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)  // 함정 구멍에 떨어졌을 경우 다시 리스폰
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform.position;
        }
    }
    IEnumerator Sliding() //슬라이딩 실행
    {
        GameManager.GetComponent<Ui_Controller>().Sliding();
        Transform SlideTransform = transform.GetChild(4);
        Speed = 0;
        chargeTimer = 0;
        isSlide = true;
        gameObject.tag = "Sliding";
        gameObject.layer = LayerMask.NameToLayer("Sliding");
        anim.SetBool("Sliding", true);
        PlaySound("Slideing");
        if (slideDir == 1) //오른쪽으로 슬라이딩
        {
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)-1.3, (float)-1.12); // Smoke 위치값 변경
            SlideTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (slideDir == -1) //왼쪽으로 슬라이딩
        {
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)1.3, (float)-1.12); // Smoke 위치값 변경
            SlideTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        this.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f); //무적 시간
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        this.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = SpeedChange;
        yield return new WaitForSeconds(SlidingCool); //슬라이딩 쿨타임
        isSlide = false;
    }

    public void Playerhurt(float Damage, Vector2 pos) // Player가 공격받을 시
    {
        if (!ishurt)
        {
            if (!isShield)    // 방어막이 없으면 피격됨
            {
                float x;
                if (transform.position.x < pos.x)
                    x = -1;
                else
                    x = 1;

                Damage = DefDamgeCarculation(Damage); //방어력 계산식 추가
                CurrentHp = CurrentHp - Damage;
                ishurt = true;
                PlaySound("Damaged");

                if (CurrentHp <= 0)
                {
                    if (!UsePastErase)
                    {
                        isDie = true;
                        CurrentHp = 0;
                        GameManager.GetComponent<Ui_Controller>().Damage(Damage);
                        StartCoroutine(Die());
                        GameObject GO = GameManager.GetComponent<Ui_Controller>().StatisticsUi;
                        GO.SetActive(true);
                        GO.GetComponent<StatisticsUi>().isFalling = true;
                    }
                    else
                    {
                        PastEraseFunction();
                    }
                }
                else
                {
                    GameManager.GetComponent<Ui_Controller>().Damage(Damage);
                    StartCoroutine(Routine());
                    StartCoroutine(Knockback(x));
                    StartCoroutine(Blink());
                }

            }
            else
            {
                ShieldTime = 0;
                StartCoroutine(Blink());
            }
        }

    }

    IEnumerator Attack_delay() //기본공격 딜레이
    {
        yield return new WaitForSeconds(delayTime);
        isdelay = false;
    }

    void Sword_attack() //Sword 공격 관련 정보
    {
        isdelay = true;
        Dmg = (ATP + AtkPower + GridPower + VulcanPower) * WeaponsDmg[0];//변경함
        box.size = new Vector2(2.5f, 2.5f);
        box.offset = new Vector2(1.5f, 0);
        anim.SetFloat("Sword", SwdCnt); //Blend를 이용해 일반공격과 스킬 애니메이션 구분 실행
        anim.SetTrigger("sword_atk"); //공격 대미지 함수 실행은 애니메이션 부분에 들어있음
    }

    void Axe_attack()   //Axe 공격 관련 정보
    {
        isdelay = true;
        if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
            AxeCnt++;
        else
            AxeCnt = 1;

        if (AxeCnt == 1) // 동작별 대미지 변경
        {
            Dmg = (ATP + AtkPower + GridPower + VulcanPower) * WeaponsDmg[1];
        }
        else if (AxeCnt == 2)
        {
            Dmg = (ATP + AtkPower + GridPower + VulcanPower + 5) * WeaponsDmg[1];
        }
        box.size = new Vector2(2.5f, 2.5f);
        if (slideDir == 1)   //공격 방향별 box.offset값을 다르게 적용
            box.offset = new Vector2(2, 0);
        else
            box.offset = new Vector2(1, 0);
        //PlaySound("AxeAtk1");     유니티 애니메이션에 실행되게 추가해뒀음

        anim.SetFloat("Axe", AxeCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
        anim.SetTrigger("axe_atk");

        if (AxeCnt > 1)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
            AxeCnt = 0;

        curTime = coolTime + 0.5f;  // 콤보 공격 제한시간
    }

    void Axe_chargeing()  //Axe 차징 스킬 관련
    {
        isdelay = true;
        Speed = 0;
        AxeCnt = 3;
        Dmg = (ATP + AtkPower + GridPower + VulcanPower) * 3 * WeaponsDmg[1];
        isCharging = false;
        chargeTimer = 0f;
        anim.SetFloat("Axe", AxeCnt); //차징 공격은 Axe_atk3 짧은 애니메이션으로 실행
        anim.SetTrigger("axe_atk");
    }
    IEnumerator Bow_attack() //화살 일반공격 및 스킬 - 애니메이션 특정 부분에서 실행되게 유니티에서 설정함
    {
        yield return null;
        Transform ArrowposTransform = transform.GetChild(1);  // 기본 화살
        Transform Arrowpos2Transform = transform.GetChild(2); // 증가 화살

        if (slideDir == 1)   //공격 방향별 Arrowpos 위치값 변경
        {
            ArrowposTransform.localPosition = new Vector3(0, -0.2f);
            Arrowpos2Transform.localPosition = new Vector3(-0.3f, -0.5f);
        }
        else
        {
            ArrowposTransform.localPosition = new Vector3(-0.3f, -0.2f);
            Arrowpos2Transform.localPosition = new Vector3(-0.1f, -0.5f);
        }

        if (isSkill)
        {
            Instantiate(BowSkill, Arrowpos.position, transform.rotation);   //스킬 이펙트 화살 생성
            PlaySound("BowSkill");
            isSkill = false;
        }
        else if (isMasterSkill && proLevel >= 3 && proSelectWeapon == 2)
        {
            Instantiate(BowMaster, Arrowpos.position, transform.rotation);  //마스터 스킬 이펙트 화살 생성
        }
        else
        {
            Instantiate(Arrow, Arrowpos.position, transform.rotation); // 기본 화살 복사 생성
            if (proLevel >= 2 && proSelectWeapon == 2)
                Instantiate(Arrow2, Arrowpos2.position, transform.rotation);  // 증가된 화살 복사 생성
            PlaySound("BowAtk");
        }

        if (slideDir == 1)  //플레이어가 바라보는 방향 왼쪽
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 3f, Time.deltaTime); // 활 공격시 약간의 뒤로 밀림
            else
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime);
        }
        else  //플레이어가 바라보는 방향 오른쪽
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 3f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
        }
        yield return new WaitForSeconds(0.1f);
        isMasterSkill = false;
    }

    IEnumerator Knockback(float dir) //피해입을시 넉백
    {
        if (NoNockback)
        {
            yield break;
        }
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.4f) //넉백 지속시간
        {
            Vector2 vector2 = new Vector2(dir, 1);
            transform.Translate(vector2.normalized * Speed * 2 * Time.deltaTime);
            ctime += Time.deltaTime;
            yield return null;
        }
        isknockback = false;
    }

    IEnumerator Routine() // 피해입을시 잠깐동안 무적
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(2f);
        ishurt = false;
    }

    IEnumerator Blink() // 무적시간동안 투명 효과
    {
        while (ishurt)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        if (isShield && ShieldTime <= 0)
        {
            Transform childTransform = this.transform.GetChild(6);
            SpriteRenderer childSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();

            for (int i = 0; i < 2; i++)
            {
                childSpriteRenderer.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.1f);
                childSpriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }
            this.transform.GetChild(6).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            isShield = false;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator PadJump(int up)
    {
         PlaySound("Jump");
        if (JumpCnt >= 0 && up == 1)
            this.transform.GetChild(5).gameObject.SetActive(true);
        isjump = true;
        anim.SetBool("Player_Jump", true);
        if (up == 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Jump");
            yield return new WaitForSeconds(0.4f);
        }
        else if (up == 0)
        {
          
            gameObject.layer = LayerMask.NameToLayer("Jump");
            JumpCnt = JumpCount;
            yield return new WaitForSeconds(0.3f);
        }
        if (ishurt)
        {
            gameObject.layer = LayerMask.NameToLayer("Invincible");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
            

        this.transform.GetChild(5).gameObject.SetActive(false);
    } //발판 무시 관련

    IEnumerator Die() //Player 사망시 스프라이트 삭제
    {
        ishurt = false;
        if (EnemyAudioSource.instance != null)
        {
            EnemyAudioSource.instance.SoundOff();
        }
        SoundManager.instance.bgSound.clip = null;
        PlaySound("Die");
        anim.SetTrigger("Die");
        yield return null;
        this.transform.gameObject.SetActive(false);
    }

    void PlayerReposition() // 리스폰 위치 지정 2023-09-02 추가
    {
        Playerhurt(10, transform.position);
        transform.position = spawnPoint;
    }

    void PlaySound(string action) // 사운드 관련 함수
    {
        switch (action)
        {
            case "Jump":
                audio.clip = JumpSound;
                break;
            case "Slideing":
                audio.clip = SlideingSound;
                break;
            case "Damaged":
                audio.clip = DamagedSound;
                break;
            case "Die":
                audio.clip = DieSound;
                break;
            case "SwordAtk":
                audio.clip = SwordAtkSound;
                break;
            case "AxeAtk1":
                audio.clip = AxeAtk1Sound;
                break;
            case "AxeAtk2":
                audio.clip = AxeAtk2Sound;
                break;
            case "BowAtk":
                audio.clip = BowAtkSound;
                break;
            case "SwordSkill":
                audio.clip = SwordSkillSound;
                break;
            case "SwordMasterSkill":
                audio.clip = SwordMasterSound;
                break;
            case "AxeSkill":
                audio.clip = AxeSkillSound;
                break;
            case "AxeMasterSkill":
                audio.clip = AxeMasterSound;
                break;
            case "BowSkill":
                audio.clip = BowSkillSound;
                break;
            case "BowMasterSkill":
                audio.clip = BowMasterSound;
                break;
        }
        audio.Play();
    }

    public bool CCGetRandomResult() //치명타 계산 함수 추가
    {
        // 0과 1 사이의 랜덤한 값을 생성
        float randomValue = Random.Range(0f, 1f);

        // 랜덤 값이 확률보다 작거나 같으면 true를 반환, 그렇지 않으면 false를 반환
        return randomValue <= CriticalChance;
    }

    public float DefDamgeCarculation(float damage) //방어력 계산 함수 추가
    {
        float NewDmg;
        if (Def > 0)
        {
            NewDmg = damage - (damage * (0.007f * Def));
            return NewDmg;
        }
        else if (Def == 0)
        {
            return damage;
        }
        else
        {
            NewDmg = damage + (damage * (-0.05f * Def));
            return NewDmg;
        }
    }

    public float DeCoolTimeCarcul(float cooltime) //쿨타임 감소 식 추가
    {
        float newCooltime;
        newCooltime = cooltime - (cooltime * DecreaseCool);
        return newCooltime;
    }

    public void GetSelectValue(string selectName) //선택 능력치 얻는 함수
    {
        switch (selectName)
        {
            case "selectAtkLevel":
                if (selectAtkLevel > 1) // 레벨 1 이상
                {
                    DmgIncrease += selectAtkValue[selectAtkLevel - 1] - selectAtkValue[selectAtkLevel - 2];
                }
                else if (selectAtkLevel == 1) // 처음 찍을 때
                {
                    DmgIncrease += selectAtkValue[selectAtkLevel - 1];
                }
                break;
            case "selectATSLevel":
                if (selectATSLevel > 1) // 레벨 1 이상
                {
                    ATS += selectATSValue[selectATSLevel - 1] - selectATSValue[selectATSLevel - 2];
                    delayTime = -0.4f * ATS + 1.4f;
                    anim.SetFloat("AttackSpeed", ATS);
                }
                else if (selectATSLevel == 1) // 처음 찍을 때
                {
                    ATS += selectATSValue[selectATSLevel - 1];
                    delayTime = -0.4f * ATS + 1.4f;
                    anim.SetFloat("AttackSpeed", ATS);
                }
                break;
            case "selectCCLevel":
                if (selectCCLevel > 1) // 레벨 1 이상
                {
                    CriticalChance += selectCCValue[selectCCLevel - 1] - selectCCValue[selectCCLevel - 2];
                }
                else if (selectCCLevel == 1) // 처음 찍을 때
                {
                    CriticalChance += selectCCValue[selectCCLevel - 1];
                }
                break;
            case "selectLifeStillLevel":
                if (selectLifeStillLevel > 1) // 레벨 1 이상
                {
                    lifeStill += selectLifeStillValue[selectLifeStillLevel - 1] - selectLifeStillValue[selectLifeStillLevel - 2];
                }
                else if (selectLifeStillLevel == 1) // 처음 찍을 때
                {
                    lifeStill += selectLifeStillValue[selectLifeStillLevel - 1];
                }
                break;
            case "selectDefLevel":
                if (selectDefLevel > 1) // 레벨 1 이상
                {
                    Def += selectDefValue[selectDefLevel - 1] - selectDefValue[selectDefLevel - 2];
                }
                else if (selectDefLevel == 1) // 처음 찍을 때
                {
                    Def += selectDefValue[selectDefLevel - 1];
                }
                break;
            case "selectHpLevel":
                if (selectHpLevel > 1) // 레벨 1 이상
                {
                    MaxHp += BaseHp * selectHpValue[selectHpLevel - 1] / 100 - BaseHp * selectHpValue[selectHpLevel - 2] / 100;
                }
                else if (selectHpLevel == 1) // 처음 찍을 때
                {
                    MaxHp += BaseHp * selectHpValue[selectHpLevel - 1] / 100;
                }
                break;
            case "selectGoldLevel":
                if (selectGoldLevel > 1) // 레벨 1 이상
                {
                    GoldGet += selectGoldValue[selectGoldLevel - 1] - selectGoldValue[selectGoldLevel - 2];
                }
                else if (selectGoldLevel == 1) // 처음 찍을 때
                {
                    GoldGet += selectGoldValue[selectGoldLevel - 1];
                }
                break;
            case "selectExpLevel":
                if (selectExpLevel > 1) // 레벨 1 이상
                {
                    EXPGet += selectExpValue[selectExpLevel - 1] - selectExpValue[selectExpLevel - 2];
                }
                else if (selectExpLevel == 1) // 처음 찍을 때
                {
                    EXPGet += selectExpValue[selectExpLevel - 1];
                }
                break;
            case "selectCoolTimeLevel":
                if (selectCoolTimeLevel > 1) // 레벨 1 이상
                {
                    DecreaseCool += selectCoolTimeValue[selectCoolTimeLevel - 1] - selectCoolTimeValue[selectCoolTimeLevel - 2];
                }
                else if (selectCoolTimeLevel == 1) // 처음 찍을 때
                {
                    DecreaseCool += selectCoolTimeValue[selectCoolTimeLevel - 1];
                }
                break;
            case "Start":
                if (selectAtkLevel - 1 >= 0)
                {
                    DmgIncrease += selectAtkValue[selectAtkLevel - 1];
                }
                if (selectATSLevel - 1 >= 0)
                {
                    ATS += selectATSValue[selectATSLevel - 1];
                    delayTime = -0.4f * ATS + 1.4f;
                    anim.SetFloat("AttackSpeed", ATS);
                }
                if (selectCCLevel - 1 >= 0)
                {
                    CriticalChance += selectCCValue[selectCCLevel - 1];
                }
                if (selectLifeStillLevel - 1 >= 0)
                {
                    lifeStill += selectLifeStillValue[selectLifeStillLevel - 1];
                }
                if (selectDefLevel - 1 >= 0)
                {
                    Def += selectDefValue[selectDefLevel - 1];
                }
                if (selectHpLevel - 1 >= 0)
                {
                    MaxHp += MaxHp * selectHpValue[selectHpLevel - 1] / 100;
                }
                if (selectGoldLevel - 1 >= 0)
                {
                    GoldGet += selectGoldValue[selectGoldLevel - 1];
                }
                if (selectExpLevel - 1 >= 0)
                {
                    EXPGet += selectExpValue[selectExpLevel - 1];
                }
                if (selectCoolTimeLevel - 1 >= 0)
                {
                    DecreaseCool += selectCoolTimeValue[selectCoolTimeLevel - 1];
                }
                break;
        }
    }

    public int[] returnPlayerSelectLevel() //추가함
    {
        int[] SL = new int[9];
        SL[0] = selectAtkLevel;
        SL[1] = selectATSLevel;
        SL[2] = selectCCLevel;
        SL[3] = selectLifeStillLevel;
        SL[4] = selectDefLevel;
        SL[5] = selectHpLevel;
        SL[6] = selectGoldLevel;
        SL[7] = selectExpLevel;
        SL[8] = selectCoolTimeLevel;
        return SL;
    }

    //아이템 특수효과 함수
    public void GridsSword()
    {
        GridPower = (gold / 777) * 0.77f;
        if (!UseGridSword)
        {
            GameManager.GetComponent<Ui_Controller>().UiUpdate();
            UseGridSword = true;
        }
    }

    public void VulcanArmor()
    {
        VulcanPower = (Def / 10) * 5;
        if (!UseVulcanArmor)
        {
            GameManager.GetComponent<Ui_Controller>().UiUpdate();
            UseVulcanArmor = true;
        }
    }

    public void HpRegen()
    {
        Ui.Heal(LifeRegen);
        Invoke("HpRegen", 1.5f);
    }

    public void PastEraseFunction()
    {
        GameManager.GetComponent<inven>().updateUi();
        GameObject prefab = Resources.Load<GameObject>("item/BrokenWatch");
        Instantiate(prefab, PastErase.transform.parent);
        itemStatus item = prefab.GetComponent<itemStatus>();
        item.InitSetting();
        item.StatusGet(this);
        item.data.SpecialPower = false;
        item.SpecialPower();
        UsePastErase = false;
        Destroy(PastErase);
        TimeLoopAnim.SetTrigger("TimeLooping");
        Time.timeScale = 0.3f;
        TimeLoopSound.Play();
        Ui.Heal(MaxHp);
        Invoke("RealTime", 1.25f);
    }

    public void Mirror()
    {
        if (UseMirror)
        {
            cam.orthographicSize = 7f;
        }
        else
        {
            cam.orthographicSize = 9f;
        }
    }

    public void RealTime()
    {
        TimeLoopSound.Pause();
        Time.timeScale = 1f;
        ishurt = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void AxeMasterSkill()
    {
        float time = 0f;
        for (int i = 0; i < 5; i++)
        {
            Invoke("SkillCreate", time);
            time += 0.2f;
        }
        SkillCount = 1;
    }

    void SkillCreate()
    {
        int px;
        px = SkillCount * 6;
        if (slideDir > 0)
        {
            px *= 1;
        }
        if (slideDir < 0)
        {
            px *= -1;
        }
        Vector3 Pc = new Vector3(this.transform.position.x + px, this.transform.position.y + 3f);
        GameObject punch = Instantiate(AxeMasterEfc, transform.parent);
        punch.transform.position = Pc;
        SkillCount++;
    }
}