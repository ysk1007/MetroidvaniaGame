using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;   // �÷��̾� ����
    public float jumpPower; //Jump ���� ���� ����
    public float Speed; //Move �ӵ� ���� ����
    public float SpeedChange; // Move �ӵ����� ���� ����
    public float curTime, coolTime = 2;  // ���Ӱ����� ������ �ð�
    public float skcoolTime;  // ��ų ��Ÿ��
    public float Sword_SkTime, Axe_SkTime, Bow_SkTime;   // ���⺰ ��ų ��Ÿ��
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
    public float attackDash = 4f; //ū ���ݽ� ������ �̵��ϴ� ��
    public float slideSpeed = 13;   //�����̵� �ӵ�
    public int slideDir = 1;    //�����̵� ���Ⱚ
    public float MaxHp;    //�÷��̾� �ִ� HP
    public float CurrentHp;    //�÷��̾� ���� HP
    public bool ishurt = false; //�ǰ� Ȯ��
    public bool isknockback = false;    //�˹� Ȯ��
    public float Dmg;  // ����� ���� ����
    public float DmgChange; // ����� ���� ���� ����
    public float ShieldTime; // ���� ��ų �� ���ӽð�
    public float chargingTime = 2f; // ��¡ �ð�
    public bool isCharging = false; // ��¡ ���� ����
    public float chargeTimer = 0f; // ��¡ �ð��� �����ϴ� Ÿ�̸�

    public static Player instance; //�߰���
    public int gold;  //�߰���
    public int AtkPower; //�߰���
    public int Def; //�߰���
    public float CriticalChance; //�߰��� , �̼�,���� �����Ǵ°� ���� �ʿ�
    public float DmgIncrease; //�߰���
    public float enemyPower;

    public static int swordLevel = 3;       // 2023-07-31 �߰�(Į ���õ�)
    public static float BleedingTime = 8f;  // 2023-07-31 �߰�(���� ���� �ð�)
    public static float bleedDamage = 0.5f; // 2023-08-01 ���� ������
    //public float enemyBleedingTime; // 2023-08-01(������ ���� ���� �ð�)

    //���ôɷ�ġ �߰���
    public float[] selectAtkLevel = { 10f, 20f, 30f };
    public float[] selectATSLevel = { 15f, 30f, 45f };
    public float[] selectCCLevel = { 5f, 10f, 20f };
    public float[] selectDefLevel = { 10f, 20f, 30f };
    public float[] selectHpLevel = { 30f, 60f, 90f };
    public float[] selectGoldLevel = { 130f, 160f, 200f };
    public float[] selectExpLevel = { 130f, 160f, 200f };
    public float[] selectCoolTimeLevel = { 5f, 10f, 20f };

    public GameObject GameManager;  //���� �Ŵ���
    public GameObject attackRange;  //�������� ��ġ
    public GameObject Arrow; //ȭ�� ������Ʈ
    public GameObject Arrow2; //ȭ�� ���� ������Ʈ
    public GameObject Slash;  // �� �⺻��ų ������Ʈ
    public GameObject BowSkill;  // Ȱ �⺻��ų ������Ʈ
    public GameObject BowMaster; // Ȱ ���õ� ȭ�� ������Ʈ

    public Transform Arrowpos; //ȭ�� ���� ������Ʈ
    public Transform Arrowpos2; //������ ȭ��  ������Ʈ
    public Transform Attackpos;   //���ݹڽ� ��ġ
    public Transform Skillpos;  // ��ų ���� ������Ʈ

    public AudioClip SwordSkillSound;
    public AudioClip SwordAtkSound;
    public AudioClip AxeAtk1Sound;
    public AudioClip AxeAtk2Sound;
    public AudioClip BowAtkSound;
    public AudioClip BowSkillSound;
    public AudioClip DamagedSound;
    public AudioClip AxeSkillSound;
    public AudioClip JumpSound;
    public AudioClip SlideingSound;


    public BoxCollider2D box; //���� ���� ����
    public SpriteRenderer spriteRenderer;
    public Enemy enemy;
    Projective_Body PBody;

    Rigidbody2D rigid;
    Animator anim;
    new AudioSource audio;
    void Awake()
    {
        instance = this; //�߰���
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
    }

    void Start() //�߰���
    {
        DataManager.instance.JsonLoad("PlayerData");
    }

    void Update()
    {
        //enemyBleedingTime = Enemy.bleedingTime; // 2023-08-01 �߰� (���� ���� ���� �ð� ��� ���)

        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�
    }

    void Player_Move() //Player �̵�, ����
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill)    //���� ���������Ͻ� �̵� �Ұ���
        {
            Speed = SpeedChange;
           Transform AtkRangeTransform = transform.GetChild(1);   // AttackRange ��ġ�� ������ ���� �ڽĿ�����Ʈ ��ġ�� �ҷ���
            anim.SetBool("Player_Walk", true);
            if (Direction < 0) //���� �ٶ󺸱�
            {
                spriteRenderer.flipX = false;
                transform.Translate(new Vector2(-1, 0) * Speed * Time.deltaTime);
                slideDir = -1;
                AtkRangeTransform.localPosition = new Vector3(-3, 0); // AttackRange ��ġ�� ����
            }
            else if (Direction > 0) //������ �ٶ󺸱�
            {
                spriteRenderer.flipX = true;
                transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
                slideDir = 1;
                AtkRangeTransform.localPosition = new Vector3(0, 0);
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
            if(Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(PadJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            StartCoroutine(PadJump());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
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

        if (Input.GetKeyDown(KeyCode.D) && !anim.GetBool("Sliding"))    //�����ͽ�ų ����
        {
            isMasterSkill = true;
            if (WeaponChage == 1)   //��
            {
                //Debug.Log(BleedingTime);
                enemy.bleedEff();
            }
            if (WeaponChage == 2)   //����
            {

            }
            if (WeaponChage == 3)   //Ȱ
            {
                StartCoroutine(MasterSkill());
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !anim.GetBool("Sliding"))  //��ų ����
        {
            if (WeaponChage == 1 && Sword_SkTime <= 0)    //Sword ��ų ����
            {
                skcoolTime = 10f;
                Sword_SkTime = skcoolTime;
                isSkill = true;
                SwdCnt = 2;
                anim.SetTrigger("sword_atk");
                anim.SetFloat("Sword", SwdCnt); // �ִϸ��̼ǿ� ��ų �����Լ��� �־����
            }
            if (WeaponChage == 2 && Axe_SkTime <= 0)    //Axe ��ų ����
            {
                StartCoroutine(Skill());
                skcoolTime = 20f;
                Axe_SkTime = skcoolTime;
            }
            if (WeaponChage == 3 && Bow_SkTime <= 0)    //Bow ��ų ����
            {
                StartCoroutine(Skill());
                skcoolTime = 10f;
                Bow_SkTime = skcoolTime;
                isSkill = true;
            }
        }
        else
        {
            if(Sword_SkTime >= 0)   // �׳� 0�� �Ʒ��� ��� ���ҵǴ� �۾� ���ַ��� �߰�
                Sword_SkTime -= Time.deltaTime;
            if (Axe_SkTime >= 0)
                Axe_SkTime -= Time.deltaTime;
            if (Bow_SkTime >= 0)
                Bow_SkTime -= Time.deltaTime;
        }
            
        if(Input.GetKey(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill) // Axe ��¡ ����
        {
            if(WeaponChage == 2)
            {
                if (!isCharging)
                {
                    isCharging = true;
                    print("��¡ ����");
                    chargeTimer = 0f;
                }
                else // �̹� ��¡ ���� ���
                {
                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= chargingTime)
                    {
                        chargeTimer = chargingTime; // ��¡ �ð��� �ִ� �ð��� �Ѿ�� �ʵ��� ����
                        print("��¡�Ϸ�");
                    }

                }
            }
        }
        else
        {
            isCharging = false;
            if (chargeTimer >= chargingTime && chargeTimer != 0)
            {
                Axe_chargeing();
                StartCoroutine(Attack_delay());
            }
           
        }


        if (Input.GetKeyUp(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill)    //�⺻ ����
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
            {
                StartCoroutine(Blink());
                gameObject.layer = LayerMask.NameToLayer("Player");
                isShield = false;
            }
        }
    }

    public void AttackDamage()// Player ���ݽ� ������ ������� �Ѱ��ֱ�
    {
        Dmg = DmgChange;
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
                        //StopCoroutine(enemy.Hit(Dmg));
                        StartCoroutine(enemy.Hit(Dmg));
                        //enemy.Hit(Dmg);
                        Debug.Log(Dmg + "Player");
                    }
                }
            }
        }
    } 
    IEnumerator Skill()//��ų �۵��� ����(���� ������)
    {
        //yield return null;
        if (WeaponChage == 1) //sword ��ų
        {
            StartCoroutine(SkillTime());
            Transform SkillTransform = transform.GetChild(3);   //�� ��ų ������Ʈ ��ġ�� ����
            if (slideDir == 1)   //���� ���⺰ Arrowpos ��ġ�� ����
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
        }
        if (WeaponChage == 2) //Axe ��ų
        {
            gameObject.layer = LayerMask.NameToLayer("Shield"); //�� Ȱ��ȭ �� 10�ʰ� ����
            this.transform.GetChild(6).gameObject.SetActive(true);  //�� ����Ʈ �ѱ�
            PlaySound("AxeSkill");
            isShield = true;
            ShieldTime = 10f;
        }
        if (WeaponChage == 3) //Arrow ��ų
        {
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
        }
    }

    IEnumerator MasterSkill()
    {
        if(WeaponChage == 3)
        {
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
        }
        yield return null;
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
        if (ShieldTime >= 0)
            gameObject.layer = LayerMask.NameToLayer("Shield");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = SpeedChange;
        yield return new WaitForSeconds(2f); //�����̵� ��Ÿ��
        isSlide = false;

    }

    public void Playerhurt(float Damage, Vector2 pos) // Player�� ���ݹ��� ��
    {
        if (!ishurt)
        {
            if (gameObject.layer != LayerMask.NameToLayer("Shield"))    // ���� ������ �ǰݵ�
            {

                ishurt = true;
                CurrentHp = CurrentHp - Damage;
                PlaySound("Damaged");

                if (CurrentHp <= 0)
                {
                    //Invoke("Die", 3f);
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
                StartCoroutine(Blink());
                gameObject.layer = LayerMask.NameToLayer("Player");
                isShield = false;
                ShieldTime = 0;
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
            DmgChange = 10;
            box.size = new Vector2(5f, 2.5f);
            if(slideDir == 1)   //���� ���⺰ box.offset���� �ٸ��� ����
                box.offset = new Vector2(2, 0);
            else
                box.offset = new Vector2(1, 0);
            //PlaySound("AxeAtk1");     ����Ƽ �ִϸ��̼ǿ� ����ǰ� �߰��ص���
        }
        else if (AxeCnt == 2)    
        {
            DmgChange = 15;
            box.size = new Vector2(4.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(2.5f, 0);
            else
                box.offset = new Vector2(0.5f, 0);
            //PlaySound("AxeAtk1");     ����Ƽ �ִϸ��̼ǿ� ����ǰ� �߰��ص���
        }
        else if (AxeCnt == 3)
        {
            DmgChange = 20;
            attackDash = 6;
            box.size = new Vector2(5.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(3.5f, 0);
            else
                box.offset = new Vector2(-0.5f, 0);
            //PlaySound("AxeAtk3");     ����Ƽ �ִϸ��̼ǿ� ����ǰ� �߰��ص���
        }
        anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
        anim.SetTrigger("axe_atk");

        if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
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
        anim.SetFloat("Axe", AxeCnt); //��¡ ������ 3�� �ִϸ��̼����� ����
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
        else if(isMasterSkill)
        {
            Instantiate(BowMaster, Arrowpos.position, transform.rotation);  //������ ��ų ����Ʈ ȭ�� ����
        }
        else
        {
            Instantiate(Arrow, Arrowpos.position, transform.rotation); // �⺻ ȭ�� ���� ����
            if(level == 2)
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

    IEnumerator SkillTime() //��ų ���� �ð�
    {
        if(WeaponChage == 1)
            yield return new WaitForSeconds(0.6f);
        if(WeaponChage == 3)
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
        if(ShieldTime > 0)  // �ǰ� ������ �� ��ų�� �����ϸ� �÷��̾� ���̾�� �ٲ����ʰ� �ٷ� ������ ����
            gameObject.layer = LayerMask.NameToLayer("Shield");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator Blink() // �����ð����� ���� ȿ��
    {
        while (ishurt)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        if(isShield)
        {
            Transform childTransform = this.transform.GetChild(6);
            SpriteRenderer childSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();

            childSpriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            childSpriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            childSpriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            childSpriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
            this.transform.GetChild(6).gameObject.SetActive(false);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    IEnumerator PadJump()
    {
        PlaySound("Jump");
        if (JumpCnt == 1)
            this.transform.GetChild(5).gameObject.SetActive(true);
        isjump = true;
        anim.SetBool("Player_Jump", true);
        gameObject.layer = LayerMask.NameToLayer("Jump");
        
        yield return new WaitForSeconds(0.3f);
        if (ShieldTime >= 0 && !ishurt && !isSlide) //�� ���ӽð��� �����ٸ� �� ���̾� ����
            gameObject.layer = LayerMask.NameToLayer("Shield");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
        this.transform.GetChild(5).gameObject.SetActive(false);
    } //���� ���� ����

    void Die() //Player ����� ��������Ʈ ����
    {
        Destroy(gameObject);
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
                //audio.clip =
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
            case "AxeSkill":
                audio.clip = AxeSkillSound;
                break;
            case "BowSkill":
                audio.clip = BowSkillSound;
                break;
        }
        audio.Play();
    }
}