using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;   // �÷��̾� ����
    public float[] ExpBarValue = {
        100f, 150f, 200f, 250f, 300f, 
        350f, 400f, 450f, 500f, 550f, 
        600f, 650f, 700f, 750f, 800f,
        850f, 900f, 950f, 1000f, 1050f,
        1100f, 1150f, 1200f, 1250f, 1300f,
        1350f, 1400f};   // ������ �ϴµ� �ʿ��� ����ġ �߰���
    public float jumpPower; //Jump ���� ���� ����
    public float Speed; //Move �ӵ� ���� ����
    public float SpeedChange; // Move �ӵ����� ���� ����
    public float curTime, coolTime = 2;  // ���Ӱ����� ������ �ð�
    public float skcoolTime;  // ��ų ��Ÿ��
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public bool isjump = false;     //���������� üũ
    public bool isSkill = false;    //��ų Ȯ��
    public bool isMasterSkill = false;  //���õ� ��ų Ȯ��
    public bool isAttacking = false; //���ݻ��� Ȯ��
    public bool isShield = false;   //�� ���� Ȯ��
    public float delayTime = 1f;    //���� ������ �⺻ �ð�
    public int WeaponChage = 1;     //���� ���� ���� ����
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt, AxeCnt;  //���ݸ���� ����
    public float Direction; //���Ⱚ
    public float slideSpeed = 13;   //�����̵� �ӵ�
    public int slideDir = 1;    //�����̵� ���Ⱚ
    public float MaxHp = 100;    //�÷��̾� �ִ� HP
    public float BaseHp = 100;    //�÷��̾� �ִ� HP
    public float CurrentHp;    //�÷��̾� ���� HP
    public bool ishurt = false; //�ǰ� Ȯ��
    public bool isknockback = false;    //�˹� Ȯ��
    public float Dmg;  // ����� ���� ����
    public float DmgChange; // ����� ���� ���� ����
    public float ShieldTime; // ���� ��ų �� ���ӽð�
    public float chargingTime = 2f; // ��¡ �ð�
    public bool isCharging = false; // ��¡ ���� ����
    public float chargeTimer = 0f; // ��¡ �ð��� �����ϴ� Ÿ�̸�
    public PlayerCanvas playerCanvas; //�߰���
    public float[] MasterSkillTime = { 10, 10, 10 };   //���⺰ ���õ� ��ų ��Ÿ��
    public float Sword_MsTime, Axe_MsTime, Bow_MsTime;  // ���⺰ ���õ� ��ų ��Ÿ�� ����
    public float[] SkillTime = { 10, 20, 10 }; // ���⺰ �⺻��ų ��Ÿ��
    public float Sword_SkTime, Axe_SkTime, Bow_SkTime;  // ���⺰ �⺻��ų ��Ÿ�� ����
    public float attackDash = 4f; //ū ���ݽ� ������ �̵��ϴ� ��

    public static Player instance; //�߰���
    public float gold;  //�߰���
    public float AtkPower; //�߰���
    public float Def = 5; //�߰���
    public float CriticalChance; //�߰��� , �̼�,���� �����Ǵ°� ���� �ʿ�
    public float DmgIncrease = 1f; //�߰���
    public float CriDmgIncrease = 1.5f; //�߰���
    public float ATS = 1; //�߰���
    public float GoldGet = 1f; //�߰���
    public float EXPGet = 1f; //�߰���
    public bool CanlifeStill = false; //�߰���
    public float lifeStill; //�߰���
    public float DecreaseCool = 0f; //�߰���
    public float enemyPower;
    public int proSelectWeapon = 4; //4�� ���õ��� ������ ���� ���� 0,1,2 => Į,����,Ȱ
    public int proLevel = 0;
    public int stackbleed; // ���Ϳ� ���� ���� ����

    public int proSelectWeapon = 0;       // 2023-07-31 �߰�(������ ���õ� ����) 0Į,1����,2Ȱ,4���� X
    public int proLevel = 3; // ���õ� ����
    public int stackbleed;  // ���Ϳ� ���� ���� ����
    public static float BleedingTime = 8f;  // 2023-07-31 �߰�(���� ���� �ð�)
    public static float bleedDamage = 0.5f; // 2023-08-01 ���� ������
    public static float bloodBoomDmg = 7f;  // �������� �Ͷ߸��� ������
    public static string playerTag;    // 2023-08-11 �߰� (�÷��̾� ���� �±�)


    //���ôɷ�ġ ���
    public float[] selectAtkValue = { 0.1f, 0.2f, 0.3f };
    public float[] selectATSValue = { 0.15f, 0.3f, 0.4f };
    public float[] selectCCValue = { 0.05f, 0.1f, 0.2f };
    public float[] selectLifeStillValue = {0.01f, 0.03f, 0.09f };
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
    public GameObject attackRange;  //�������� ��ġ
    public GameObject Arrow; //ȭ�� ������Ʈ
    public GameObject Arrow2; //ȭ�� ���� ������Ʈ
    public GameObject Slash;  // �� �⺻��ų ������Ʈ
    public GameObject AxeSkill;  //���� �����ͽ�ų ������Ʈ
    public GameObject BowSkill;  // Ȱ �⺻��ų ������Ʈ
    public GameObject BowMaster; // Ȱ ���õ� ȭ�� ������Ʈ

    public Transform Arrowpos; //ȭ�� ���� ������Ʈ
    public Transform Arrowpos2; //������ ȭ��  ������Ʈ
    public Transform Attackpos;   //���ݹڽ� ��ġ
    public Transform Skillpos;  // ��ų ���� ������Ʈ
    public Transform Axepos;   //��ų ���� ��ġ

    public AudioClip SwordAtkSound;
    public AudioClip SwordSkillSound;
    //public AudioClip SwordMasterSound;
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
    Projective_Body PBody;

    Rigidbody2D rigid;
    public Animator anim; //������ �ۺ������� ����
    new AudioSource audio;

    public bool UseGridSword = false;
    public float GridPower = 0f;
    void Awake()
    {
        instance = this; //�߰���
        attackRange = transform.GetChild(0).gameObject; // �÷��̾��� 0��° ������Ʈ�� attackRange�� ����
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //���۽� ���� ���� Ƚ�� ����
        SpeedChange = 4;  //���۽� �⺻ �̵��ӵ�
        jumpPower = 15; //�⺻ ��������
        DmgChange = 7; // �⺻ ���� �����
        audio = GetComponent<AudioSource>();
        Attackpos = transform.GetChild(0).GetComponentInChildren<Transform>(); //attackRange�� ��ġ���� pos�� ����
        Arrowpos = transform.GetChild(1).GetComponentInChildren<Transform>(); //Arrowpos�� ��ġ���� pos�� ����
        Arrowpos2 = transform.GetChild(2).GetComponentInChildren<Transform>(); //Arrowpos2�� ��ġ���� pos�� ����
        Skillpos = transform.GetChild(3).GetComponentInChildren<Transform>(); //Skillpos�� ��ġ���� pos�� ����
        Axepos = transform.GetChild(7).GetComponentInChildren<Transform>(); //Axepos�� ��ġ���� pos�� ����
    }

    void Start() //�߰���
    {
        DataManager.instance.JsonLoad("PlayerData");
        anim.SetFloat("AttackSpeed", ATS);
        OptionManager.instance.Playing = true;
    }

    void Update()
    {
        playerTag = this.gameObject.transform.GetChild(0).tag;
        stackbleed = Enemy.bleedLevel; // 2023-08-09 �߰� 
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�
        if (UseGridSword) //�߰���
        {
            GridsSword();
        }
    }

    void Player_Move() //Player �̵�, ����
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill)    //���� ���������Ͻ� �̵� �Ұ���
        {
            Speed = SpeedChange;
           Transform AtkRangeTransform = transform.GetChild(0);   // AttackRange ��ġ�� ������ ���� �ڽĿ�����Ʈ ��ġ�� �ҷ���
           Transform AxeposTransform = transform.GetChild(7);   //Axepos ��ġ�� ���� ���� �ڽĿ�����Ʈ ��ġ�� ������
            anim.SetBool("Player_Walk", true);
            if (Direction < 0) //���� �ٶ󺸱�
            {
                spriteRenderer.flipX = false;
                transform.Translate(new Vector2(-1, 0) * Speed * Time.deltaTime);
                slideDir = -1;
                AtkRangeTransform.localPosition = new Vector3(-3, 0); // AttackRange ��ġ�� ����
                AxeposTransform.localPosition = new Vector3(-3, -(0.8f));   //Axepos ��ġ�� ����
            }
            else if (Direction > 0) //������ �ٶ󺸱�
            {
                spriteRenderer.flipX = true;
                transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
                slideDir = 1;
                AtkRangeTransform.localPosition = new Vector3(0, 0);
                AxeposTransform.localPosition = new Vector3(3, -(0.8f));
            }
        }
        else
            anim.SetBool("Player_Walk", false);

        //Sliding
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && !isjump && !isdelay && !isSkill)
        {
            StartCoroutine(Sliding());
        }

        //Jump
        if (Input.GetKey(KeyCode.DownArrow) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide")) //���ǿ��� ������ ������ ��������
        {
            RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1.5f, LayerMask.GetMask("Pad"));
            //Debug.DrawRay(rigid.position, Vector3.down * 1.5f, Color.red);
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(PadJump(0));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            StartCoroutine(PadJump(1));
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(!Input.GetKey(KeyCode.DownArrow))
                JumpCnt--;
            isGround = false;
        }
    }

    public void Player_Attack() //Player ���ݸ���
    {

        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)    // ���� ����
        {
            WeaponChage += 1;      
            if(WeaponChage == 2)
            {
                attackRange.tag = "Axe";
            }
            else if(WeaponChage == 3)
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
        }

        if (Input.GetKeyDown(KeyCode.D) && proSelectWeapon != 4 && proLevel == 3)    //���õ� ��ų ����
        {
            if (!anim.GetBool("Sliding") && !isMasterSkill && !isSkill && !isCharging)
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

        if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("Sliding") && !isSkill && !isMasterSkill)  //�⺻ ��ų ����
        {
            if (WeaponChage == 1 && Sword_SkTime <= 0)    //Sword
            {
                skcoolTime = DeCoolTimeCarcul(Skill_Cools[0]); //��ų�� ������
                sSkill = true;
                SwdCnt = 2;
                anim.SetTrigger("sword_atk");
                anim.SetFloat("Sword", SwdCnt); // �ִϸ��̼ǿ� ��ų �����Լ��� �־����
            }
            if (WeaponChage == 2 && Axe_SkTime <= 0)    //Axe
            {
                isSkill = true;
                skcoolTime = DeCoolTimeCarcul(Skill_Cools[1]); //��ų�� ������
                StartCoroutine(Skill());
            }
            if (WeaponChage == 3 && Bow_SkTime <= 0)    //Bow
            {
                isSkill = true;
                skcoolTime = DeCoolTimeCarcul(Skill_Cools[2]); //��ų�� ������
                StartCoroutine(Skill());
            }
        }
        if(Sword_SkTime >= 0)   //Sword ��ų ��Ÿ�� 
        {
            Sword_SkTime -= Time.deltaTime;
        }
        if(Axe_SkTime >= 0)     //Axe ��ų ��Ÿ��
        {
            Axe_SkTime -= Time.deltaTime;
        }
        if(Bow_SkTime >= 0)     //Bow ��ų ��Ÿ��
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
            if(WeaponChage == 2)
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
                    playerCanvas.GuageIncrease(chargeTimer/chargingTime); //���� ������ �߰���
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
            if(curTime >= 0)
                curTime -= Time.deltaTime;
            if(ShieldTime >= 0)
                ShieldTime -= Time.deltaTime;
            if (ShieldTime <= 0 && !ishurt && !isSlide && !isjump) //�� ���ӽð��� �����ٸ� �� ���̾� ����
        }

        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)    // ���� ����
        {
            if(!isCharging && !isSkill && !isMasterSkill)
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
            }
        }
    }

    public void AttackDamage()// Player ���ݽ� ������ ������� �Ѱ��ֱ�
    {
        Dmg = DmgChange + AtkPower + GridPower;//������
        box = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
        if (box != null)    //���� ���� �ȿ� null���� �ƴҶ���
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Attackpos.position, box.size, 0); //���� ���� �ȿ� �ݶ��̴��� 
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy" || collider.tag == "Boss")
                {
                    enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        StartCoroutine(enemy.Hit(Dmg));
                        Debug.Log(Dmg + "Player");
                    }
                }
            }
        }
    } 
    IEnumerator Skill()//��ų �۵��� ����
    {
        if (WeaponChage == 1) //sword ��ų
        {
            StartCoroutine(SkillDelay());
            Transform SkillTransform = transform.GetChild(3);   //�� ��ų ������Ʈ ��ġ�� ����
            if (slideDir == 1)
            {
                SkillTransform.localPosition = new Vector3(2, 0.2f);
            }
            else
            {
                SkillTransform.localPosition = new Vector3(-2, 0.2f);
            }
            Instantiate(Slash, Skillpos.position, transform.rotation); // �˱� ���� ����
            PlaySound("SwordSkill");
            yield return new WaitForSeconds(0.2f);
            SwdCnt = 1;
            Sword_SkTime = SkillTime[0];
        }
        if (WeaponChage == 2) //Axe ��ų
        {

            //gameObject.layer = LayerMask.NameToLayer("Shield"); //�� Ȱ��ȭ �� 10�ʰ� ����
            this.transform.GetChild(6).gameObject.SetActive(true);  //�� ����Ʈ �ѱ�
            PlaySound("AxeSkill");
            isShield = true;
            ShieldTime = 10f;
            Axe_SkTime = SkillTime[1];
            isSkill = false;
        }
        if (WeaponChage == 3) //Arrow ��ų
        {
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillDelay());
            Bow_SkTime = SkillTime[2];
        }
    }

    IEnumerator MasterSkill()
    {
        if (WeaponChage == 1 && proSelectWeapon == 0 && Sword_MsTime <= 0) //Sword ���õ� ��ų
        {
            if (stackbleed > 0)
            {
                isMasterSkill = true;
                //enemy.bleedEff();
                Sword_MsTime = MasterSkillTime[0];
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
            AxeCnt = 4;
            anim.SetFloat("Axe", AxeCnt); //���õ� ��ų�� Axe_atk3 ��� �ִϸ��̼����� ����
            anim.SetTrigger("axe_atk");
            yield return new WaitForSeconds(1.5f);
            //PlaySound("AxeMasterSkill"); // �ִϸ��̼ǿ� ���� ����
            Instantiate(AxeSkill, Axepos.position, transform.rotation);
            Axe_MsTime = MasterSkillTime[1];
            yield return new WaitForSeconds(1f);
            isMasterSkill = false;
        }
        if(WeaponChage == 3 && proSelectWeapon == 2 && Bow_MsTime <= 0) //Bow ���õ� ��ų
        {
            isMasterSkill = true;
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillDelay());
            yield return new WaitForSeconds(0.5f);
            PlaySound("BowMasterSkill");
            Bow_MsTime = MasterSkillTime[2];
        }
    }

    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // �� �ݶ������� Player�� ��� ������ ����, �������� �� �ݶ����� ���� �� ���� ����
    {
        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Tilemap", "Pad"));
        //Debug.DrawRay(rigid.position, Vector3.down * 1f, Color.red);

        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
        if(collision.gameObject.tag == "Pad" || collision.gameObject.tag == "Tilemap" && !isGround)
        {
            anim.SetBool("Player_Jump", false);
            isjump = false;
            isGround = true;
        }
        if (rayHitDown.collider != null && !anim.GetBool("Sliding"))
        {
            JumpCnt = JumpCount;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)   //Player�� ���� ���� ������
    {
        if (collision.gameObject.tag == "Wall") //���� �پ ������ ��
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
       
        if (collision.gameObject.tag == "Wall" && Input.GetKey(KeyCode.Space)) //������ ������ �ݴ�� �ðܰ�
        {
            if (Direction > 0)
                rigid.velocity = new Vector2(1, 1) * 10f;
            if (Direction < 0)
                rigid.velocity = new Vector2(-1, 1) * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  // ���� ���ۿ� �������� ��� �ٽ� ������(�ӽ�)
    {
        if(collision.gameObject.tag == "Respawn")
        {
            Playerhurt(10, Attackpos.position);
            PlayerReposition();
        }
    }

    private IEnumerator Sliding() //�����̵� ����
    {
        GameManager.GetComponent<Ui_Controller>().Sliding();
        Transform SlideTransform = transform.GetChild(4);
        Speed = 0;
        isSlide = true;
        gameObject.tag = "Sliding";
        gameObject.layer = LayerMask.NameToLayer("Sliding");
        anim.SetBool("Sliding", true);
        PlaySound("Slideing");
        if (slideDir == 1) //���������� �����̵�
        {
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)-1.1, (float)-0.4); // Smoke ��ġ�� ����
            SlideTransform.eulerAngles = new Vector3(0, 0, 0);
        }    
        if (slideDir == -1) //�������� �����̵�
        {
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
            SlideTransform.localPosition = new Vector3((float)1.1, (float)-0.4); // Smoke ��ġ�� ����
            SlideTransform.eulerAngles = new Vector3(0, 180, 0);
        }
        this.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f); //���� �ð�
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        this.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = SpeedChange;
        yield return new WaitForSeconds(2f); //�����̵� ��Ÿ��
        isSlide = false;

    }

    public void Playerhurt(float Damage, Vector2 pos) // Player�� ���ݹ��� ��
    {
        if (!ishurt)
        {
            if (!isShield)    // ���� ������ �ǰݵ�
            {
                Damage = DefDamgeCarculation(Damage); //���� ���� �߰�
                ishurt = true;
                CurrentHp = CurrentHp - Damage;
                PlaySound("Damaged");

                if (CurrentHp <= 0)
                {
                    StartCoroutine(Die(pos));
                }
                else
                {
                    anim.SetTrigger("hurt");
                    GameManager.GetComponent<Ui_Controller>().Damage(Damage);
                    float x = transform.position.x - pos.x;
                    if (transform.position.x > pos.x)
                        x = 1;
                    else
                        x = -1;
                    StartCoroutine(Knockback(x));
                    StartCoroutine(Routine());
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

    IEnumerator Attack_delay() //���Ӱ��� ������
    {
        yield return new WaitForSeconds(delayTime);
        isdelay = false;
    }

    void Sword_attack() //Sword ���� ���� ����
    {
        isdelay = true;
        DmgChange = 7;
        box.size = new Vector2(3.5f, 2.5f);
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

        if(AxeCnt == 1) // ���ۺ� ����� ����
        {
           DmgChange = 15;
        }
        else if (AxeCnt == 2)    
        {
            DmgChange = 20;
        }
        box.size = new Vector2(4f, 2.5f);
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
        DmgChange = 30;
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
            ArrowposTransform.localPosition = new Vector3(1, 0.2f);
            Arrowpos2Transform.localPosition = new Vector3(1, -0.5f);
        }
        else
        {
            ArrowposTransform.localPosition = new Vector3(-1, 0.2f);
            Arrowpos2Transform.localPosition = new Vector3(-1, -0.5f);
        }

        if(isSkill)
        {
            Instantiate(BowSkill, Arrowpos.position, transform.rotation);   //��ų ����Ʈ ȭ�� ����
            PlaySound("BowSkill");
        }
        else if(isMasterSkill && proLevel >= 3 && proSelectWeapon == 2)
        {
            Instantiate(BowMaster, Arrowpos.position, transform.rotation);  //������ ��ų ����Ʈ ȭ�� ����
        }
        else
        {
            Instantiate(Arrow, Arrowpos.position, transform.rotation); // �⺻ ȭ�� ���� ����
            if(proLevel >= 2 && proSelectWeapon == 2)
                Instantiate(Arrow2, Arrowpos2.position, transform.rotation);  // ������ ȭ�� ���� ����
            PlaySound("BowAtk");
        }


        if (slideDir == 1)  //�÷��̾ �ٶ󺸴� ���� ����
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime); // Ȱ ���ݽ� �ణ�� �ڷ� �и�
            else
                rigid.velocity = new Vector2(transform.localScale.x - 10f, Time.deltaTime);
        }
        else  //�÷��̾ �ٶ󺸴� ���� ������
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 10f, Time.deltaTime);
        }
    }

    IEnumerator SkillDelay() //��ų ���� �ð� - �ִϸ��̼� �ӵ��� �����ַ��� �߰���
    {
        if(WeaponChage == 1)
            yield return new WaitForSeconds(0.6f);
        else if(WeaponChage == 2)
            yield return new WaitForSeconds(0.5f);
        else
            yield return new WaitForSeconds(1.3f);

        isSkill = false;
        isMasterSkill = false;
    }

    IEnumerator Dash() //�Ϻ� ���ݽ� ������ �뽬 �̵� - �ִϸ��̼� Ư�� �κп��� ����ǰ� ����Ƽ���� ������
    {
        yield return null;
        if (slideDir == -1)
        {
            rigid.velocity = new Vector2(transform.localScale.x - attackDash, Time.deltaTime);
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x + attackDash, Time.deltaTime);
        }
    }

    IEnumerator Knockback(float dir) //���������� �˹�
    {
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.4f) //�˹� ���ӽð�
        {
            Vector2 vector2 = new Vector2(dir, 1);
            transform.Translate(vector2.normalized * Speed * 3 * Time.deltaTime);
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
        if(isShield && ShieldTime <= 0)
        {
            Transform childTransform = this.transform.GetChild(6);
            SpriteRenderer childSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();

            for(int i = 0; i < 2; i++)
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
    }

    IEnumerator PadJump(int up)
    {
        PlaySound("Jump");
        if (JumpCnt >= 0 && up == 1)
            this.transform.GetChild(5).gameObject.SetActive(true);
        isjump = true;
        anim.SetBool("Player_Jump", true);
        if(up == 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Jump");
            yield return new WaitForSeconds(0.5f);
        }
        else if(up == 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Jump");
            yield return new WaitForSeconds(0.3f);
        }
        gameObject.layer = LayerMask.NameToLayer("Player");

        this.transform.GetChild(5).gameObject.SetActive(false);
    } //���� ���� ����

    IEnumerator Die(Vector2 pos) //Player ����� ��������Ʈ ����
    {
        PlaySound("Die");
        float x = transform.position.x - pos.x;
        if (transform.position.x > pos.x)
            x = 1;
        else
            x = -1;
        StartCoroutine(Knockback(x));
        anim.SetTrigger("Die");
        gameObject.tag = "DiePlayer";
        //gameObject.layer = LayerMask.NameToLayer("DiePlayer");
        yield return new WaitForSeconds(2.2f);
        this.transform.gameObject.SetActive(false);
    }

    void PlayerReposition() // ������ ��ġ ����(�ӽ�)
    {
        transform.position = new Vector3(-30, -7.5f, 0);
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
                //audio.clip = SwordMasterSound;
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
            NewDmg = damage + (damage / (-0.05f * Def));
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
        Debug.Log("GetSelectValue: " + selectName);
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
        GridPower = (gold / 777) * 7;
        if (!UseGridSword)
        {
            GameManager.GetComponent<Ui_Controller>().UiUpdate();
            UseGridSword = true;
        }
    }
}