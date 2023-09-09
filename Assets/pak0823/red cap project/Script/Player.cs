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
        1350f, 1400f};   // ������ �ϴµ� �ʿ��� ����ġ �߰���
    public float Speed; //Move �ӵ� ���� ����
    public float jumpPower; //Jump ���� ���� ����
    public float SpeedChange; // Move �ӵ����� ���� ����
    public float curTime, coolTime = 2;  // ���Ӱ����� ������ �ð�
    public float[] MasterSkillTime = { 40, 90, 45 };   //���⺰ ���õ� ��ų ��Ÿ��
    public float Sword_MsTime, Axe_MsTime, Bow_MsTime;  // ���⺰ ���õ� ��ų ��Ÿ�� ����
    public float[] SkillTime = { 12, 20, 10 }; // ���⺰ �⺻��ų ��Ÿ��
    public float Sword_SkTime, Axe_SkTime, Bow_SkTime;  // ���⺰ �⺻��ų ��Ÿ�� ����
    public float[] WeaponsDmg = { 0.65f, 1.2f, 0.8f }; //���⺰ ���ݷ� ��� Į, ����, Ȱ
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public bool isjump = false;     //���������� üũ
    public bool isWallJump = false;
    public bool isSkill = false;    //��ų Ȯ��
    public bool isMasterSkill = false;  //���õ� ��ų Ȯ��
    public bool isAttacking = false; //���ݻ��� Ȯ��
    public bool isShield = false;   //�� ���� Ȯ��
    public bool isWall = false;     // ���� �پ� �ִ��� Ȯ��
    public bool isDie = false;  // �÷��̾ �׾����� �Ǵ�    2023-09-03 �߰�
    public float delayTime = 1f;    //���� ������ �⺻ �ð�
    public int WeaponChage = 1;     //���� ���� ���� ����
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt, AxeCnt;  //���ݸ���� ����
    public float Direction; //���Ⱚ
    public float attackDash = 4f; //ū ���ݽ� ������ �̵��ϴ� ��
    public float slideSpeed = 13;   //�����̵� �ӵ�
    public int slideDir = 1;    //�����̵� ���Ⱚ
    public float MaxHp = 100;    //�÷��̾� �ִ� HP
    public float BaseHp = 100;    //�÷��̾� �ִ� HP
    public float CurrentHp;    //�÷��̾� ���� HP
    public bool ishurt = false; //�ǰ� Ȯ��
    public bool isknockback = false;    //�˹� Ȯ��
    public float Dmg;  // ���� ����� ����
    public float ShieldTime; // ���� ��ų �� ���ӽð�
    public float chargingTime = 5f; // ��¡ �ð�
    public bool isCharging = false; // ��¡ ���� ����
    public float chargeTimer = 0f; // ��¡ �ð��� �����ϴ� Ÿ�̸�
    public float ArrowDistance = 0.75f;
    public bool Axepro = true;
    public PlayerCanvas playerCanvas; //�߰���
    public Vector3 spawnPoint;  // 2023-09-02 �߰�(�÷��̾� ������ ��ġ)

    public static Player instance; //�߰���
    public float ATP; // �÷��̾� �����
    public int level;   // �÷��̾� ����
    public float gold;  //�߰���
    public float AtkPower; //�߰���
    public float Def = 5; //�߰���
    public float CriticalChance; //�߰��� , �̼�,���� �����Ǵ°� ���� �ʿ�
    public float DmgIncrease = 1f; //�߰���
    public float CriDmgIncrease = 1.5f; //�߰���
    public float ATS = 1; //�߰���
    public float GoldGet = 1f; //�߰���
    public float EXPGet = 1f; //�߰���
    public bool CanlifeStill = true; //�߰���
    public float lifeStill; //�߰���
    public float DecreaseCool = 0f; //�߰���
    public float LifeRegen = 0f; //�߰���
    public float SlidingCool = 2f;
    public int proSelectWeapon = 4; //4�� ���õ��� ���� ���� ���� 0,1,2 => Į,����,Ȱ
    public int proLevel = 0;
    public int EnemyKillCount = 0;
    public float TotalGetGold = 0f;
    public float TotalDamaged = 0f;
    public bool FirstMaterial = false;
    public bool SecondMaterial = false;
    public bool ThirdMaterial = false;
    public float enemyPower;
    public int stackbleed;  // ���Ϳ� ���� ���� ����
    public int slashBleedStack; 
    public static float BleedingTime = 8f;  // 2023-07-31 �߰�(���� ���� �ð�)
    public float bleedDamage = 10f; // 2023-08-01 ���� ������
    public static float bloodBoomDmg = 66f;  // �������� �Ͷ߸��� ������
    public static string playerTag;    // 2023-08-11 �߰� (�÷��̾� ���� �±�)
    public List<Collider2D> enemyColliders = new List<Collider2D>();   //���� ���� üũ
    public List<Enemy> enemyCheck = new List<Enemy>(); // Enemy Ÿ�� ����Ʈ�� ����

    //���ôɷ�ġ ���
    public float[] selectAtkValue = { 0.1f, 0.2f, 0.3f };
    public float[] selectATSValue = { 0.15f, 0.3f, 0.4f };
    public float[] selectCCValue = { 0.05f, 0.1f, 0.2f };
    public float[] selectLifeStillValue = { 0.005f, 0.007f, 0.01f };
    public float[] selectDefValue = { 10f, 20f, 30f };
    public float[] selectHpValue = { 30f, 60f, 90f };
    public float[] selectGoldValue = { 0.3f, 0.6f, 1.0f };
    public float[] selectExpValue = { 0.3f, 0.6f, 1.0f };
    public float[] selectCoolTimeValue = { 0.05f, 0.1f, 0.2f };

    //���ôɷ�ġ ����
    public int selectAtkLevel = 0;
    public int selectATSLevel = 0;
    public int selectCCLevel = 0;
    public int selectLifeStillLevel = 0;
    public int selectDefLevel = 0;
    public int selectHpLevel = 0;
    public int selectGoldLevel = 0;
    public int selectExpLevel = 0;
    public int selectCoolTimeLevel = 0;

    public GameObject GameManager;  //���� �Ŵ���
    MapManager map;
    public Ui_Controller Ui;  //ui ��Ʈ�ѷ�
    public GameObject attackRange;  //�������� ��ġ
    public GameObject Arrow; //ȭ�� ������Ʈ
    public GameObject Arrow2; //ȭ�� ���� ������Ʈ
    public GameObject Slash;  // �� �⺻��ų ������Ʈ
    public GameObject AxeSkill;  //���� �����ͽ�ų ������Ʈ
    public GameObject BowSkill;  // Ȱ �⺻��ų ������Ʈ
    public GameObject BowMaster; // Ȱ ���õ� ȭ�� ������Ʈ
    public GameObject HolyArrow; // Ȱ ���õ� ȭ�� ������Ʈ
    public GameObject Axebuf; // ���� ���õ� 0�� �нú� ����

    public Transform Arrowpos; //ȭ�� ���� ������Ʈ
    public Transform Arrowpos2; //������ ȭ��  ������Ʈ
    public Transform Attackpos;   //���ݹڽ� ��ġ
    public Transform Skillpos;  // ��ų ���� ������Ʈ

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

    public BoxCollider2D box; //���� ���� ����
    public BoxCollider2D Axebox; //���� ���õ� ����
    public SpriteRenderer spriteRenderer;
    public Enemy enemy;
    public MoveCamera movecamera;
    public Loading loading;
    Projective_Body PBody;
    Rigidbody2D rigid;
    public Animator anim;
    new AudioSource audio;

    //Ư�� ������ ����
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
        instance = this; //�߰���
        
        attackRange = transform.GetChild(0).gameObject; // �÷��̾��� 0��° ������Ʈ�� attackRange�� ����
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //���۽� ���� ���� Ƚ�� ����
        SpeedChange = 5;  //���۽� �⺻ �̵��ӵ�
        jumpPower = 15; //�⺻ ��������
        ATP = 10; // �⺻ ���� �����
        audio = GetComponent<AudioSource>();
        Attackpos = transform.GetChild(0).GetComponentInChildren<Transform>(); //attackRange�� ��ġ���� pos�� ����
        Arrowpos = transform.GetChild(1).GetComponentInChildren<Transform>(); //Arrowpos�� ��ġ���� pos�� ����
        Arrowpos2 = transform.GetChild(2).GetComponentInChildren<Transform>(); //Arrowpos2�� ��ġ���� pos�� ����
        Skillpos = transform.GetChild(3).GetComponentInChildren<Transform>(); //Skillpos�� ��ġ���� pos�� ����
        Ui = GameManager.GetComponent<Ui_Controller>();
    }

    void Start() //�߰���
    {
        movecamera = MoveCamera.instance.GetComponent<MoveCamera>();
        DataManager dm = DataManager.instance;
        loading = Loading.instance; // 09-04 �߰���
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
            Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
            Player_Attack();    //Player�� ���� �Լ�
        }
        if (UseGridSword) //�߰���
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
    void Player_Move() //Player �̵�, ����
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill && !isMasterSkill && movecamera.startFightBoss == false)    //���� ���������Ͻ� �̵� �Ұ���
        {
            Speed = SpeedChange;
            Transform AtkRangeTransform = transform.GetChild(0);   // AttackRange ��ġ�� ������ ���� �ڽĿ�����Ʈ ��ġ�� �ҷ���
            anim.SetBool("Player_Walk", true);
            if (Direction < 0) //���� �ٶ󺸱�
            {
                spriteRenderer.flipX = false;
                transform.Translate(new Vector2(-1, 0) * Speed * Time.deltaTime);
                slideDir = -1;
                AtkRangeTransform.localPosition = new Vector3(-1, 0); // AttackRange ��ġ�� ����
            }
            else if (Direction > 0) //������ �ٶ󺸱�
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
                Instantiate(HolyArrow, Arrowpos.position, transform.rotation);  //�ż�ȭ�� �߻�
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
        if (Input.GetKey(KeyCode.DownArrow) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide")) //���ǿ��� ������ ������ ��������
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
        if (isWall) // ������
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
    public void Player_Attack() //Player ���ݸ���
    {
        if (map.pause)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) && proSelectWeapon != 4 && proLevel == 3)    //���õ� ��ų ����
        {
            if (!anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && !isMasterSkill && !isSkill && !isCharging && !isdelay && !isWall)
            {
                StartCoroutine(MasterSkill());
            }
        }
        if (Sword_MsTime >= 0)   //Sword ���õ� ��ų ��Ÿ�� 
        {
            Sword_MsTime -= Time.deltaTime;
        }
        if (Axe_MsTime >= 0)     //Axe ���õ� ��ų ��Ÿ��
        {
            Axe_MsTime -= Time.deltaTime;
        }
        if (Bow_MsTime >= 0)     //Bow ���õ� ��ų ��Ÿ��
        {
            Bow_MsTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill && !isdelay && !isWall)  //�⺻ ��ų ����
        {
            if (WeaponChage == 1 && Sword_SkTime <= 0 && !anim.GetBool("sword_atk"))    //Sword
            {
                Sword_SkTime = DeCoolTimeCarcul(SkillTime[0]); //��ų�� ������
                isSkill = true;
                SwdCnt = 2;
                anim.SetTrigger("sword_atk");
                anim.SetFloat("Sword", SwdCnt); // �ִϸ��̼ǿ� ��ų �����Լ��� �־����
            }
            if (WeaponChage == 2 && Axe_SkTime <= 0)    //Axe
            {
                isSkill = true;
                StartCoroutine(Skill());
                Axe_SkTime = DeCoolTimeCarcul(SkillTime[1]); //��ų�� ������
            }
            if (WeaponChage == 3 && Bow_SkTime <= 0)    //Bow
            {
                isSkill = true;
                StartCoroutine(Skill());
                Bow_SkTime = DeCoolTimeCarcul(SkillTime[2]); //��ų�� ������
            }
        }
        if (Sword_SkTime >= 0)   //Sword ��ų ��Ÿ�� 
        {
            Sword_SkTime -= Time.deltaTime;
        }
        if (Axe_SkTime >= 0)     //Axe ��ų ��Ÿ��
        {
            Axe_SkTime -= Time.deltaTime;
        }
        if (Bow_SkTime >= 0)     //Bow ��ų ��Ÿ��
        {
            Bow_SkTime -= Time.deltaTime;
        }
        if (ShieldTime >= 0) //�� ���ӽð��� ������ �� ����
        {
            ShieldTime -= Time.deltaTime;
            if (ShieldTime <= 0)
            {
                StartCoroutine(Blink());
            }
        }

        if (Input.GetKey(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill) // Axe ��¡ ����
        {
            if (WeaponChage == 2 && proSelectWeapon == 1 && proLevel >= 1)
            {
                if (!isCharging)
                {
                    isCharging = true;
                    chargeTimer = 0f;
                    playerCanvas.ChargeStart(); //���� ������ �߰���
                }
                else // �̹� ��¡ ���� ���
                {
                    chargeTimer += Time.deltaTime;
                    playerCanvas.GuageIncrease(chargeTimer / chargingTime); //���� ������ �߰���
                    if (chargeTimer >= chargingTime)
                    {
                        chargeTimer = chargingTime; // ��¡ �ð��� �ִ� �ð��� �Ѿ�� �ʵ��� ����
                    }

                }
            }
        }
        else
        {
            isCharging = false;
            playerCanvas.ChargeEnd(); //���� ������ �߰���
            if (chargeTimer >= chargingTime && chargeTimer != 0)
            {
                Axe_chargeing();
                StartCoroutine(Attack_delay());
            }

        }

        if (Input.GetKeyUp(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill)    //�⺻ ����
        {
            if (!isdelay)   //�����̰� false�϶� ���� ����
            {
                if (WeaponChage == 1)    //Sword ����
                {
                    Sword_attack();
                }
                if (WeaponChage == 2)    //Axe ����
                {
                    Axe_attack();
                }
                if (WeaponChage == 3)    //Bow ����
                {
                    isdelay = true;
                    anim.SetTrigger("arrow_atk");
                }
                StartCoroutine(Attack_delay());    //������ ���� ���ݱ��� ������
            }
        }
        else
        {
            if (curTime >= 0)
                curTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)    // ���� ����
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

    public void AttackDamage()// Player ���ݽ� ������ ������� �Ѱ��ֱ�
    {
        box = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
        if (box != null)    //���� ���� �ȿ� null���� �ƴҶ���
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Attackpos.position, box.size, 0); //���� ���� �ȿ� �ݶ��̴��� 
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && (collider.tag == "Enemy" || collider.tag == "Boss"))
                {
                    enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        StartCoroutine(enemy.Hit(Dmg));
                        Debug.Log(Dmg + "Player");
                        if (proSelectWeapon == 0 && proLevel >= 1)  // ������ �ִ� ���͸� ����Ʈ�� ����
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
    IEnumerator Skill()//��ų �۵��� ����
    {
        GameManager.GetComponent<WeaponSwap>().Skill();
        if (WeaponChage == 1) //sword ��ų
        {
            Transform SkillTransform = transform.GetChild(3);   //�� ��ų ������Ʈ ��ġ�� ����
            if (slideDir == 1)
                SkillTransform.localPosition = new Vector3(1, 0.2f);
            else
                SkillTransform.localPosition = new Vector3(-1, 0.2f);

            PlaySound("SwordSkill");
            Instantiate(Slash, Skillpos.position, transform.rotation); // �˱� ���� ����
            yield return new WaitForSeconds(0.2f);
            SwdCnt = 1;
            isSkill = false;
        }
        if (WeaponChage == 2) //Axe ��ų
        {
            this.transform.GetChild(6).gameObject.SetActive(true);  //�� ����Ʈ �ѱ�
            PlaySound("AxeSkill");
            isShield = true;
            ShieldTime = 10f;
            isSkill = false;
        }
        if (WeaponChage == 3) //Arrow ��ų
        {
            anim.SetTrigger("arrow_atk");   //�ִϸ��̼ǿ� Bow_Attack �Լ� ������ ������
        }
    }

    void AxePro2()
    {
        if (proLevel > 1 && proSelectWeapon == 1 && Axepro == true) // ���� ���õ� 1�� �� �ߵ�
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
        Vector3 scaleVector = new Vector3(1, 1, 1); // ������ �� ��ȭ�� ������ Vector3 ����
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
        if (WeaponChage == 1 && proSelectWeapon == 0 && Sword_MsTime <= 0) //Sword ���õ� ��ų
        {
            if (enemyColliders != null)
            {
                isMasterSkill = true;
                GameManager.GetComponent<WeaponSwap>().Ult();
                PlaySound("SwordMasterSkill");
                yield return new WaitForSeconds(0.7f);

                enemyCheck = enemyColliders
                    .Where(x => x != null && (x.tag == "Enemy" || x.tag == "Boss") && x.GetComponent<Enemy>() != null && x.GetComponent<BoxCollider2D>() != null) // �����ϸ鼭 "Enemy" �Ǵ� "Boss" �±��� ���ӿ�����Ʈ�� ����
                    .Select(x => x.GetComponent<Enemy>()) // ����� ���ӿ�����Ʈ���� Enemy ��ũ��Ʈ ������Ʈ�� ������ ����Ʈ�� ����
                    .Where(enemy => enemy != null) // Enemy ������Ʈ�� null�� �ƴ� ��� ���͸�
                    .Distinct() // �ߺ��� Enemy ������Ʈ ����
                    .ToList();

                foreach (Enemy enemy in enemyCheck) // ������ ��� ������ ������ ����
                {
                    stackbleed = enemy.bleedLevel;  //2023 - 08 - 09 �߰�
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
                Debug.Log("�� �� ����");
            }
            isMasterSkill = false;
        }
        if (WeaponChage == 2 && proSelectWeapon == 1 && Axe_MsTime <= 0)    //Axe ���õ� ��ų
        {
            isMasterSkill = true;
            GameManager.GetComponent<WeaponSwap>().Ult();
            AxeCnt = 4;
            anim.SetFloat("Axe", AxeCnt); //���õ� ��ų�� Axe_atk3 ��� �ִϸ��̼����� ����
            anim.SetTrigger("axe_atk");
            yield return new WaitForSeconds(1.5f * delayTime);
            //PlaySound("AxeMasterSkill"); // �ִϸ��̼ǿ� ���� ����
            AxeMasterSkill();
            Axe_MsTime = DeCoolTimeCarcul(MasterSkillTime[1]);
            yield return new WaitForSeconds(1f);
            isMasterSkill = false;
        }
        if (WeaponChage == 3 && proSelectWeapon == 2 && Bow_MsTime <= 0) //Bow ���õ� ��ų
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
    void OnCollisionStay2D(Collision2D collision)   // �� �ݶ������� Player�� ��� ������ ����, �������� �� �ݶ����� ���� �� ���� ����
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
    void OnCollisionExit2D(Collision2D collision)   //Player�� ���� ���� ������
    {
        if (collision.gameObject.tag == "Wall") //���� �پ ������ ��
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)  // ���� ���ۿ� �������� ��� �ٽ� ������
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            spawnPoint = collision.transform.position;
        }
    }
    IEnumerator Sliding() //�����̵� ����
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
        if (slideDir == 1) //���������� �����̵�
        {
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)-1.3, (float)-1.12); // Smoke ��ġ�� ����
            SlideTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (slideDir == -1) //�������� �����̵�
        {
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)1.3, (float)-1.12); // Smoke ��ġ�� ����
            SlideTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        this.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f); //���� �ð�
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        this.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = SpeedChange;
        yield return new WaitForSeconds(SlidingCool); //�����̵� ��Ÿ��
        isSlide = false;
    }

    public void Playerhurt(float Damage, Vector2 pos) // Player�� ���ݹ��� ��
    {
        if (!ishurt)
        {
            if (!isShield)    // ���� ������ �ǰݵ�
            {
                float x;
                if (transform.position.x < pos.x)
                    x = -1;
                else
                    x = 1;

                Damage = DefDamgeCarculation(Damage); //���� ���� �߰�
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

    IEnumerator Attack_delay() //�⺻���� ������
    {
        yield return new WaitForSeconds(delayTime);
        isdelay = false;
    }

    void Sword_attack() //Sword ���� ���� ����
    {
        isdelay = true;
        Dmg = (ATP + AtkPower + GridPower + VulcanPower) * WeaponsDmg[0];//������
        box.size = new Vector2(2.5f, 2.5f);
        box.offset = new Vector2(1.5f, 0);
        anim.SetFloat("Sword", SwdCnt); //Blend�� �̿��� �Ϲݰ��ݰ� ��ų �ִϸ��̼� ���� ����
        anim.SetTrigger("sword_atk"); //���� ����� �Լ� ������ �ִϸ��̼� �κп� �������
    }

    void Axe_attack()   //Axe ���� ���� ����
    {
        isdelay = true;
        if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
            AxeCnt++;
        else
            AxeCnt = 1;

        if (AxeCnt == 1) // ���ۺ� ����� ����
        {
            Dmg = (ATP + AtkPower + GridPower + VulcanPower) * WeaponsDmg[1];
        }
        else if (AxeCnt == 2)
        {
            Dmg = (ATP + AtkPower + GridPower + VulcanPower + 5) * WeaponsDmg[1];
        }
        box.size = new Vector2(2.5f, 2.5f);
        if (slideDir == 1)   //���� ���⺰ box.offset���� �ٸ��� ����
            box.offset = new Vector2(2, 0);
        else
            box.offset = new Vector2(1, 0);
        //PlaySound("AxeAtk1");     ����Ƽ �ִϸ��̼ǿ� ����ǰ� �߰��ص���

        anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
        anim.SetTrigger("axe_atk");

        if (AxeCnt > 1)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
            AxeCnt = 0;

        curTime = coolTime + 0.5f;  // �޺� ���� ���ѽð�
    }

    void Axe_chargeing()  //Axe ��¡ ��ų ����
    {
        isdelay = true;
        Speed = 0;
        AxeCnt = 3;
        Dmg = (ATP + AtkPower + GridPower + VulcanPower) * 3 * WeaponsDmg[1];
        isCharging = false;
        chargeTimer = 0f;
        anim.SetFloat("Axe", AxeCnt); //��¡ ������ Axe_atk3 ª�� �ִϸ��̼����� ����
        anim.SetTrigger("axe_atk");
    }
    IEnumerator Bow_attack() //ȭ�� �Ϲݰ��� �� ��ų - �ִϸ��̼� Ư�� �κп��� ����ǰ� ����Ƽ���� ������
    {
        yield return null;
        Transform ArrowposTransform = transform.GetChild(1);  // �⺻ ȭ��
        Transform Arrowpos2Transform = transform.GetChild(2); // ���� ȭ��

        if (slideDir == 1)   //���� ���⺰ Arrowpos ��ġ�� ����
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
            Instantiate(BowSkill, Arrowpos.position, transform.rotation);   //��ų ����Ʈ ȭ�� ����
            PlaySound("BowSkill");
            isSkill = false;
        }
        else if (isMasterSkill && proLevel >= 3 && proSelectWeapon == 2)
        {
            Instantiate(BowMaster, Arrowpos.position, transform.rotation);  //������ ��ų ����Ʈ ȭ�� ����
        }
        else
        {
            Instantiate(Arrow, Arrowpos.position, transform.rotation); // �⺻ ȭ�� ���� ����
            if (proLevel >= 2 && proSelectWeapon == 2)
                Instantiate(Arrow2, Arrowpos2.position, transform.rotation);  // ������ ȭ�� ���� ����
            PlaySound("BowAtk");
        }

        if (slideDir == 1)  //�÷��̾ �ٶ󺸴� ���� ����
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 3f, Time.deltaTime); // Ȱ ���ݽ� �ణ�� �ڷ� �и�
            else
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime);
        }
        else  //�÷��̾ �ٶ󺸴� ���� ������
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 3f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
        }
        yield return new WaitForSeconds(0.1f);
        isMasterSkill = false;
    }

    IEnumerator Knockback(float dir) //���������� �˹�
    {
        if (NoNockback)
        {
            yield break;
        }
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.4f) //�˹� ���ӽð�
        {
            Vector2 vector2 = new Vector2(dir, 1);
            transform.Translate(vector2.normalized * Speed * 2 * Time.deltaTime);
            ctime += Time.deltaTime;
            yield return null;
        }
        isknockback = false;
    }

    IEnumerator Routine() // ���������� ��񵿾� ����
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(2f);
        ishurt = false;
    }

    IEnumerator Blink() // �����ð����� ���� ȿ��
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
    } //���� ���� ����

    IEnumerator Die() //Player ����� ��������Ʈ ����
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

    void PlayerReposition() // ������ ��ġ ���� 2023-09-02 �߰�
    {
        Playerhurt(10, transform.position);
        transform.position = spawnPoint;
    }

    void PlaySound(string action) // ���� ���� �Լ�
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

    public bool CCGetRandomResult() //ġ��Ÿ ��� �Լ� �߰�
    {
        // 0�� 1 ������ ������ ���� ����
        float randomValue = Random.Range(0f, 1f);

        // ���� ���� Ȯ������ �۰ų� ������ true�� ��ȯ, �׷��� ������ false�� ��ȯ
        return randomValue <= CriticalChance;
    }

    public float DefDamgeCarculation(float damage) //���� ��� �Լ� �߰�
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

    public float DeCoolTimeCarcul(float cooltime) //��Ÿ�� ���� �� �߰�
    {
        float newCooltime;
        newCooltime = cooltime - (cooltime * DecreaseCool);
        return newCooltime;
    }

    public void GetSelectValue(string selectName) //���� �ɷ�ġ ��� �Լ�
    {
        switch (selectName)
        {
            case "selectAtkLevel":
                if (selectAtkLevel > 1) // ���� 1 �̻�
                {
                    DmgIncrease += selectAtkValue[selectAtkLevel - 1] - selectAtkValue[selectAtkLevel - 2];
                }
                else if (selectAtkLevel == 1) // ó�� ���� ��
                {
                    DmgIncrease += selectAtkValue[selectAtkLevel - 1];
                }
                break;
            case "selectATSLevel":
                if (selectATSLevel > 1) // ���� 1 �̻�
                {
                    ATS += selectATSValue[selectATSLevel - 1] - selectATSValue[selectATSLevel - 2];
                    delayTime = -0.4f * ATS + 1.4f;
                    anim.SetFloat("AttackSpeed", ATS);
                }
                else if (selectATSLevel == 1) // ó�� ���� ��
                {
                    ATS += selectATSValue[selectATSLevel - 1];
                    delayTime = -0.4f * ATS + 1.4f;
                    anim.SetFloat("AttackSpeed", ATS);
                }
                break;
            case "selectCCLevel":
                if (selectCCLevel > 1) // ���� 1 �̻�
                {
                    CriticalChance += selectCCValue[selectCCLevel - 1] - selectCCValue[selectCCLevel - 2];
                }
                else if (selectCCLevel == 1) // ó�� ���� ��
                {
                    CriticalChance += selectCCValue[selectCCLevel - 1];
                }
                break;
            case "selectLifeStillLevel":
                if (selectLifeStillLevel > 1) // ���� 1 �̻�
                {
                    lifeStill += selectLifeStillValue[selectLifeStillLevel - 1] - selectLifeStillValue[selectLifeStillLevel - 2];
                }
                else if (selectLifeStillLevel == 1) // ó�� ���� ��
                {
                    lifeStill += selectLifeStillValue[selectLifeStillLevel - 1];
                }
                break;
            case "selectDefLevel":
                if (selectDefLevel > 1) // ���� 1 �̻�
                {
                    Def += selectDefValue[selectDefLevel - 1] - selectDefValue[selectDefLevel - 2];
                }
                else if (selectDefLevel == 1) // ó�� ���� ��
                {
                    Def += selectDefValue[selectDefLevel - 1];
                }
                break;
            case "selectHpLevel":
                if (selectHpLevel > 1) // ���� 1 �̻�
                {
                    MaxHp += BaseHp * selectHpValue[selectHpLevel - 1] / 100 - BaseHp * selectHpValue[selectHpLevel - 2] / 100;
                }
                else if (selectHpLevel == 1) // ó�� ���� ��
                {
                    MaxHp += BaseHp * selectHpValue[selectHpLevel - 1] / 100;
                }
                break;
            case "selectGoldLevel":
                if (selectGoldLevel > 1) // ���� 1 �̻�
                {
                    GoldGet += selectGoldValue[selectGoldLevel - 1] - selectGoldValue[selectGoldLevel - 2];
                }
                else if (selectGoldLevel == 1) // ó�� ���� ��
                {
                    GoldGet += selectGoldValue[selectGoldLevel - 1];
                }
                break;
            case "selectExpLevel":
                if (selectExpLevel > 1) // ���� 1 �̻�
                {
                    EXPGet += selectExpValue[selectExpLevel - 1] - selectExpValue[selectExpLevel - 2];
                }
                else if (selectExpLevel == 1) // ó�� ���� ��
                {
                    EXPGet += selectExpValue[selectExpLevel - 1];
                }
                break;
            case "selectCoolTimeLevel":
                if (selectCoolTimeLevel > 1) // ���� 1 �̻�
                {
                    DecreaseCool += selectCoolTimeValue[selectCoolTimeLevel - 1] - selectCoolTimeValue[selectCoolTimeLevel - 2];
                }
                else if (selectCoolTimeLevel == 1) // ó�� ���� ��
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

    public int[] returnPlayerSelectLevel() //�߰���
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

    //������ Ư��ȿ�� �Լ�
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