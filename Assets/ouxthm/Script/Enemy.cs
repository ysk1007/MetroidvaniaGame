using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Player player;
    public string Enemy_Name; //������ �߰���
    public bool AmIBoss = false; //������ �߰���
    public int BossHpLine; //������ �߰���
    public int Stage;

    public int Enemy_Mod;   // 1: ������, 2: �������� ���� ����, 3:�������, 4:����, 5: ����, 7: ����ü ���Ÿ�, 9: �п�, 11: �����Ͽ� �浹
    public float Enemy_HP;  // ���� ü��
    public float Enemy_HPten;   // ���� ü���� 10%
    public float Enemy_Power;   //���� ���ݷ�
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public float Enemy_Atk_Speed;    // ���� ���ݼӵ�
    public bool Attacking = false;  // ���� ������ Ȯ���ϴ� ����
    public float Gap_Distance_X;  // ���� Player X �Ÿ�����
    public float Gap_Distance_Y;  // ���� Player Y �Ÿ�����
    public float Enemy_Sensing_X;  // ���� X�� ���� ��Ÿ�
    public float Enemy_Sensing_Y;  // ���� Y�� ���� ��Ÿ�
    public float Enemy_Range_X; //���� X�� ���� ��Ÿ�
    public float Enemy_Range_Y; //���� Y�� ���� ��Ÿ�
    public float Pdamage;   // ���Ͱ� �޴� ������
    public float Bump_Power;    // �÷��̾�� �浹 �� �� ������
    public float atkDelay;  // ���� ������
    public int nextDirX;    // ���� ������ X ����
    public int nextDirY;    // ���� ������ Y ����  
    public bool Dying = false; // �״� ���� Ȯ���ϴ� ����
    public float Enemy_Dying_anim_Time;     // �״� �ִϸ��̼� �ð� ����
    public float atkX;  // ���� �ݶ��̴��� x��
    public float atkFlipx; // ���� �ݶ��̴��� x��(���� ��)
    public float atkY;  // ���� �ݶ��̴��� y��
    public bool enemyHit = false;   // ���� �������� �������� Ȯ���ϴ� ����
    public float old_Speed;     // �ӵ� �� ���ϱ� �� �ӵ� ��
    public float dTime;
    public float atkTime;   // ���� ��� �ð�
    public bool Attacker;  // ���� ���Ͱ� ���������� �ƴ��� �������� ����
    public float endTime;   // ����ü ������� �ð�

    public float scaleX; // scale X ��(hit_eff�� �������� -�Ǿ������� �ݴ�������� �߱⿡ �����ϱ� ���� ����)
    public string weaponTag;    // ���� �±� ("Sword"�� ���� ����)
    public int Swordlevel;  // �÷��̾� �� ���õ�
    public int selectWeapon;    // ���õ��� �ø� ���� ���� 0 = Į, 1 = ����, 2 = Ȱ, 4 = ���� X
    public int bleedLevel;  // ���� ����
    public int slashBleedLevel; // �˱�� ���� ��������
    public float bleedingDamage;    // ���� ������
    public float bleedingTime;  // ���� ���ӽð�
    public float bloodBoomDmg;  // �÷��̾��� ���� ���� �Ͷ߸��� ������

    public bool turning;    // ������ �ڵ� �� �ִ� ��Ȳ���� Ȯ���ϴ� ����
    public int atkPattern;  // boss�� ���� ���� ��ȣ
    public float playerLoc; // player�� X��ǥ
    public float enemyLoc;  // ������ X��ǥ
    public float bossLoc;   // boss�� X��ǥ
    public float myLocY;    // boss�� y��
    public bool bossMoving;  // boss�� �����̵��� rock ǯ

    public bool Enemy_Left; // ���� ����
    public bool Hit_Set;    // ���͸� ����� ����
    public float boarLoc;    // ������� X ������ġ 

    public GameObject hiteff;  // ��Ʈ ����Ʈ 
    public GameObject blood;   // ���� ���� ����Ʈ
    Transform hit_bloodTrans; // ��Ʈ/�������� ����Ʈ ��ġ
    
    Transform soulSpawn;    // ���� �ٴ� �Ͷ߸��� ���� ��ġ
    Transform soulSpawn1;   // ���� �ٴ� �Ͷ߸��� ���� ��ġ
    Transform soulSpawn2;   // ���� �ٴ� �Ͷ߸��� ���� ��ġ
    Transform PbSpawn;    // ������ ������ �� ���� ��ġ

    public GameObject SoulFloor; // ���� ��ȥ ����
    public GameObject Rock; // ������ ������ ��
    public GameObject CaninePb; // ������ ������ ����
    public GameObject Split_Slime;  // �п� ������
    public GameObject fire; // ������ ����ü
    public GameObject ProObject;    // Ŭ�� ����ü

    Transform spawn;    // �п��� ������ ������ ��ġ 1
    Transform spawn2;   // �п��� ������ ������ ��ġ 2
    public Transform PObject;    // ����ü ���� ��ġ
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SpriteRenderer orcWaringmark;
    SpriteRenderer sprite;  // Nec ������ ���踶ũ
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    BoxCollider2D Box;
    Transform posi;
    Transform transformPosition;
    BoxCollider2D BoxCollider2DSize;

    BoxCollider2D Boxs;
    BoxCollider2D bossBox;  // ���� ���� �ݶ��̴� �״� ���� ����
    BoxCollider2D BossSpriteBox;    // ���� �̹��� �ݶ��̴�

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
    public float TargetFind; //�¿� ����
    public GameObject BossCenter;

    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�(�߻�)

    public void Start()
    {
        player = Player.instance.GetComponent<Player>();
    }

    public virtual void Short_Monster(Transform target) 
    { 
        weaponTag = Player.playerTag;
        playerLoc = target.position.x;
        enemyLoc = this.gameObject.transform.position.x;
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x); //X�� �Ÿ� ���
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y); //Y�� �Ÿ� ���
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
        if (nextDirX != 0)   // Ư�� ���Ϳ��� Run �ִϸ��̼��� �ֱ� ������ ��������� ��
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

    public virtual void Boss(Transform target)  // boss�� Update��
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

    public virtual void Boar(Transform target)  // boar��
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
    
    public virtual void onetime()   // Awake�� ����
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

    public virtual void bossOnetime()   // boss�� Awake��
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
        Hit_Set = false;    // �÷��̾�� ���� ���� ����
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
    void switchCollider()   // ���� �ڽ� �ݶ��̴� ��ġ �Ű��ִ� �Լ�
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
            Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // ���� ������Ʈ�� ù��° �ڽ� ������Ʈ�� ���Ե� BoxCollider2D�� ������.
            spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
            posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
            Bcollider.enabled = true;   // ���� �ڽ� �ݶ��̴� ����
            if (Enemy_Mod == 2)
            {
                if (spriteRenderer.flipX == true)   // �̹��� �ø����� �� ���� ���� x�� ��ȯ ���ǹ�
                {
                    posi.localPosition = new Vector3(atkFlipx, atkY);   // ������ ���� �ݶ��̴� �ڽ��� x��ǥ�� y��ǥ
                }
                else if (spriteRenderer.flipX == false) // ������ �� ��
                {
                    posi.localPosition = new Vector3(atkX, atkY);  // ������ ���� �ݶ��̴� �ڽ��� -x��ǥ�� y��ǥ
                }
            }
            else if (Enemy_Mod == 3)
            {
                if (spriteRenderer.flipX == true)   // �̹��� �ø����� �� ���� ���� x�� ��ȯ ���ǹ�
                {
                    posi.localPosition = new Vector3(atkX, atkY);   // ������ ���� �ݶ��̴� �ڽ��� x��ǥ�� y��ǥ
                }
                else if (spriteRenderer.flipX == false) // ������ �� ��
                {
                    posi.localPosition = new Vector3(-atkX, atkY);  // ������ ���� �ݶ��̴� �ڽ��� -x��ǥ�� y��ǥ
                }
            }
        }
        

    }
    void Move() // �̵�
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

    void Think() // �ڵ����� ���� ������ ���ϴ� �ڷ�ƾ
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX = Random.Range(-1, 2);     // ���� X���� ����( -1 ~ 1)
        if(Enemy_Mod == 3)
        {
        nextDirY = Random.Range(-1, 2);     // ���� Y���� ����( -1 ~ 1)
        }
        
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true;       // nextDirX�� ���� 1�̸� x���� flip��
        }
        // ���
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn() // �̹����� ������ �ڷ�ƾ
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX *= -1;   // nextDirX�� -1�� ���� ������ȯ
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true; // nextDirX ���� 1�̸� x���� flip��
        }
        StopAllCoroutines();
        Think();
    }

    public void bleeding()  // ��Ʈ������ �ִ� �Լ�
    {
        if(bleedingTime > 0)
        {
            if (selectWeapon == 0 && Swordlevel > 0 && Enemy_HP > 0)
            {
                float damage = (bleedLevel * player.bleedDamage);
                this.GetComponentInChildren<EnemyUi>().ShowBleedText(damage); //������ �߰��� ������
                Enemy_HP -= damage; // ü����  �������� * ���� �������� ����
                player.TotalDamaged += damage;
                if (Swordlevel > 1 && Enemy_HP <= Enemy_HPten && AmIBoss == false)  // �� ���õ��� 1 �̻� ���� ü�� ������ ���Ͱ� ���� ���̸� ���(���� ����)
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
        if (selectWeapon == 0 && Swordlevel > 0)  // �÷��̾ ���� �������� ���
        {
            if (bleedLevel <= 5)     // �������� 6���� ����
            {
                bleedLevel++;
            }
            bleedingTime = Player.BleedingTime;
        }
    }
    public IEnumerator Hit(float damage) // ���� �Լ�
    {
        Player player = Player.instance.GetComponent<Player>();
        Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>(); //������ �߰���
        Proficiency_ui pro = GameManager.Instance.GetComponent<Proficiency_ui>(); // ���õ� �߰���
        damage = damage * player.DmgIncrease; //�� ���� �߰�
        if (player.UseRedCard)
        {
            float randNum = Random.Range(0.01f, 3.33f);
            damage *= randNum;
        }
        bool cc = false; // �߰�
        if (player.CCGetRandomResult()) //ġ��Ÿ ��� �߰�
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
        this.GetComponentInChildren<EnemyUi>().ShowDamgeText(damage,cc); //�� ��Ʈ
        if(Enemy_Mod == 11)
        {
            Hit_Set = true;
            StartCoroutine(Rush());
        }

        if (Enemy_HP > 0) // Enemy�� ü���� 0 �̻��� ��
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
                    old_Speed = Enemy_Speed;  // ���� �ӵ� ������ ������ ���� �ٸ� ������ �ӵ� ���� ����
                }
                animator.SetTrigger("Hit");
                Enemy_Speed = 0;
                yield return new WaitForSeconds(0.5f);
                Enemy_Speed = old_Speed;    // ���� �ӵ� ������ ����
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
    public IEnumerator Die()    // �״� �ڷ�ƾ
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (Enemy_HP <= 0 && Enemy_Mod != 3 && Enemy_Mod != 11 && Enemy_Mod != 6 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy")) // Enemy�� ü���� 0�� ���ų� ������ ��(����)
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
            if (Enemy_Mod == 9 && posi.localScale.y == 1f)   // �п� ������ ���
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
        else if (Enemy_HP <= 0 && Enemy_Mod == 3 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy")) // ���� ���� ����)
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
                    // ��������Ʈ ��ũ
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
        else if (Enemy_HP <= 0 && Enemy_Mod == 6 && this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))  // Orc���� ����
        {
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;
            for (int i = 0; i < 10; i++)
            {
                // ��������Ʈ ��ũ
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
                // ��������Ʈ ��ũ
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
                Debug.Log("��¦��¦");
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            enemyDestroy();
        }
        enemyHit = false;
    }

    void Sensing(Transform target, RaycastHit2D rayHit)  // �÷��̾� ����
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        if (Gap_Distance_X <= Enemy_Sensing_X && Gap_Distance_Y <= Enemy_Sensing_Y)      // Enemy�� X�� ��Ÿ��� ���� ��, Y�� ��Ÿ��� ���� ��
        {
            if (transform.position.x < target.position.x)            // ������ ����
            {
                nextDirX = 1;
                if (Enemy_Mod != 3)  // ���Ͱ� ����Ÿ���� �ƴ� ��
                {
                    if (nextDirX == 1 && rayHit.collider != null)  // nextDirX�� 1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                    {
                        spriteRenderer.flipX = true;
                        if(Enemy_Mod == 5)  // ���� ���Ͱ� ������ �� ���ڸ��� �ֱ� ���� �ڵ�
                        {
                            Enemy_Speed = 5f;
                            if (Attacking == true)
                            {
                                Enemy_Speed = 0f;
                            }
                        }
                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if(Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                if (Enemy_Mod == 5) // �����̶� ������ ���� �ٷ� �����ؾ� ��.
                                {
                                    Attack();
                                }
                                else
                                {
                                    Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                                }

                            }
                        }
                    }
                    else if (nextDirX == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (0,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if(Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                            }
                        }
                    }
                }
                else if (nextDirX == 1 && Enemy_Mod == 3)  // ���� ������ �÷��̾� ����
                {
                    spriteRenderer.flipX = true;
                    if (Attacker)
                    {
                        Vector2 resHeight = new Vector2(-1.5f, 1f);
                        Vector2 playerPoint = (Vector2)target.transform.position + resHeight;   // �÷��̾�� ���ļ� �����ϴ� ���� �����ϱ� ���� ���ο� ������ ������
                        transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);   // resHeight�� �����־� �÷��̾��� �Ʒ����� �������� �ʵ��� ����
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                        }
                    }
                    else if (!Attacker) 
                    { 
                        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Enemy_Speed * Time.deltaTime);
                    }
                }
            }
            else if (transform.position.x > target.position.x)      // ���� ����
            {
                nextDirX = -1;
                if (Enemy_Mod != 3) // ���Ͱ� ����Ÿ���� �ƴ� ��
                {
                    if (nextDirX == -1 && rayHit.collider != null) // nextDirX�� -1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
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
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
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
                                    Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                                }
                            }
                        }
                    }
                    else if (nextDirX == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if (Enemy_Mod != 1)
                        {
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false)
                            {
                                Attacking = true;
                                Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                            }
                        }
                    }
                }
                else if (nextDirX == -1 && Enemy_Mod == 3)  // ���� ������ �÷��̾� ����
                {
                    spriteRenderer.flipX = false;
                    if (Attacker)
                    {
                        Vector2 resHeight = new Vector2(1.5f, 1f);
                        Vector2 playerPoint = (Vector2)target.transform.position + resHeight;       // �÷��̾�� ���ļ� �����ϴ� ���� �����ϱ� ���� ���ο� ������ ������(Vector2�� �����Ͽ� +�� �� �� Vector2���� 3���� ��ȣ���� �ʰ� ��)
                        transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y) // Ÿ���� ��ġ�� 2.5f�� ���ؼ� Bee�� �÷��̾��� �Ʒ��ʿ��� �����ϴ� ���� ����
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
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
                Move();     // Move �Լ� ����
            }
        }
    }

    public void Sensor()    // �÷��� ���� �Լ�
    {
        rigid = this.GetComponent<Rigidbody2D>();
        if (Enemy_Mod != 3)
        {
            // Enemy�� �� ĭ ���� ���� ��� ���� �ڱ� �ڽ��� ��ġ ���� (x)�� + nextDirX���� ���ϰ� 1.2f�� ���Ѵ�.
            Vector2 frontVec = new Vector2(rigid.position.x + nextDirX * 1.2f, rigid.position.y);

            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));

            // �������� �Ʒ��� ��Ƽ� �������� ������ ����(�������), LayMask.GetMask("")�� �ش��ϴ� ���̾ ��ĵ��
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Tilemap", "Pad", "wall"));
            if (rayHit.collider == null)
            {
                Turn();
            }
        }
    }

    public void Attack() //���� �Լ�
    {
        animator = this.GetComponentInChildren<Animator>();
        if (!Dying && Enemy_Mod != 5)
        {
            if (Enemy_Mod == 3)
            {
                switchCollider();
                GiveDamage();       // �÷��̾�� ������ �ִ� �Լ� ����    
                animator.SetTrigger("Attack");  // �� ���ݿ�
                animator.SetBool("Attacking", true);    // �� ���� Ȯ���ϴ� ���� ��(�ʹ� �����Ǽ� ��� �� ��)
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
    public void offAttkack() // ���� ���� �Լ�
    {
        if(Enemy_Mod == 3)
        {
            Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // ���� ������Ʈ�� ù��° �ڽ� ������Ʈ�� ���Ե� BoxCollider2D�� ������. 
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

    public void GiveDamage()    // �÷��̾�� �������� �ִ� �Լ�
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
    public void Bump()      // �浹 ������ �Լ�
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

    private void OnDrawGizmos() // Bump()�� ����Ǵ� ��ġ�� �׸��� �Լ�
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

    public IEnumerator Boom()  // ���� �Լ�
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

    public IEnumerator Split()  // ������ �п� �Լ�
    {
        spawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        spawn2 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        GameObject splitSlime = Instantiate(Split_Slime, spawn.position, spawn.rotation);
        GameObject splitSlime2 = Instantiate(Split_Slime, spawn2.position, spawn2.rotation);
        yield return null;
    }


    public void slimeJump() //������ ��������
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("Attacking");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void enemyDestroy()
    {
        Destroy(this.gameObject); 
    }

    public void ProjectiveBody()    // ����ü ���� (��ġ ����)
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
        Pb.Dir = nextDirX;   // Projective_Body ��ũ��Ʈ�� �ִ� Dir ������ ���� ��ũ��Ʈ�� ���� nextDirX�� ����
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

    public void canineSpawning()    // ������ ���͸� ������ �Լ�
    {
        GameObject canine = Instantiate(CaninePb, PbSpawn.position, PbSpawn.rotation);
        canine.transform.eulerAngles = new Vector3(0, 0, 10); // �߻簢 ���ϱ�

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
    public void hitEff()    // �ǰ� ����Ʈ 
    {
        GameObject hitEff = Instantiate(hiteff, hit_bloodTrans.position, hit_bloodTrans.rotation, hit_bloodTrans);
        scaleX = this.gameObject.transform.localScale.x;    // ������Ʈ�� scale.x ���� �޾ƿͼ� -�� ��쿡�� ����Ʈ�� ���������� �� �� �ֵ����ϴ� ����
        hitEFF hitEFF = hitEff.GetComponent<hitEFF>();
        hitEFF.dir = nextDirX;
        hitEFF.scalX = scaleX;
    }

    public void bleedEff()  // ���� ����Ʈ �Ͷ߸��� �Լ�
    {
        GameObject bloodEff = Instantiate(blood, hit_bloodTrans.position, hit_bloodTrans.rotation, hit_bloodTrans);
        bloodEFF bloodEFF = bloodEff.GetComponent<bloodEFF>();
        bloodEFF.dir = nextDirX;    // ������ ���Ⱚ
        bloodEFF.scalX = scaleX;    // ������ scale.x ��(-�� �Ǿ��ִ� ��찡 ����)
        float Damage = bloodBoomDmg * bleedLevel;
        Damage = Damage * player.DmgIncrease; //�� ���� �߰�
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
        if (turning == true)    // ���� ������ ����
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
 
    public void bossMove()  // boss�� �����̵��� �ϴ� �Լ�
    {
        if (bossMoving)
        {
            gameObject.transform.Translate(new Vector2(nextDirX, 0) * Time.deltaTime * Enemy_Speed);   
        }
    }
    public void randomAtk() // ���� ���� �������� ���ϱ�
    {
        int nextNum;

        atkPattern = Random.Range(1, 4);
        nextNum = Random.Range(4, 7);
        Invoke("randomAtk", nextNum);
    }

    public void bossSoul()      // ��ȥ �߻�
    {
        animator.SetTrigger("Attacking");
    }

    public IEnumerator bossJump()       // �����ġ��
    {
        animator.SetTrigger("Jump");
        Enemy_Speed = 12f;
        bossMoving = true;
        yield return new WaitForSeconds(0.5f);
        bossMoving = false;
    }

    public void bossFloor()     // �ٴ� �Ͷ߸��� ���
    {
        Enemy_Speed = 1f;
        animator.SetTrigger("Run");
        bossMoving = true;
        Invoke("offFloor", 2f);
        
    }

    public void offFloor()  // ���� �ٴ� �Ͷ߸��� ���� ������ �Լ�
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
    public void locBox() // ���� ���� �ݶ��̴� ��ġ�Լ�
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
    public void onBox() // ���� ���� �ݶ��̴� on �Լ�
    {
        bossBox.enabled = false;
        GiveDamage();
    }
    public void offBox()
    {
        bossBox.enabled = true;
    }
    void onSpriteNec()  // Nec��
    {
        sprite.enabled = true;
    }
    void offSpriteNec() // Nec��
    {
        sprite.enabled = false;
    }
    void onSprite() // orc��
    {
        if(atkPattern < 5)
        {
            orcWaringmark.enabled = true;
        }
    }
    void offSPrite() // orc��
    {
        orcWaringmark.enabled = false;
    }

    void OrcRandomAtk()
    {
        int randNum;
        randNum = Random.Range(4, 5); 
        atkPattern = Random.Range(1, 7);     // ���� ��ȣ�� 1 ~ 6���� �������� ����.
        if(this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("OrcRandomAtk", randNum);
        }
    }

    void orcMove()  // Orc ������ ���������� �����̴� �Լ�
    {
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * Enemy_Speed);
    }
    void orcDie()   // Orc ������ �״� �ִϸ��̼�
    {
        if (Dying)
        {
            BossSpriteBox.enabled = false;
            rigid.isKinematic = true;
            gameObject.transform.Translate(Vector2.down * Time.deltaTime * 5);
        }
    }

    void OrcAttack()    // orc ������ ���� ����
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
        Attacking = false;  // ������ ����
    }

    void Right_Hooking()
    {
        Enemy_Speed = 1f;
        animator.SetTrigger("Right_Hooking");
        Invoke("offSPrite", 0.2f);
        Attacking = false;  // ���� �� ����.
    }
    IEnumerator recoil()    // ���� �ݵ����� ��� ���ߴ� �Լ�
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Idle", true);
        yield return new WaitForSeconds(4f);
        animator.SetBool("Idle", false);
    }


    IEnumerator boarMove()
    {
        if (Hit_Set == true)    // �÷��̾�� �¾Ҵٸ�
        {
            if (animator.GetBool("Rush") && Enemy_Left == true)       // �ٴ� �ִϸ��̼��� ���� ���̰�, Fat_Left�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (animator.GetBool("Rush") && Enemy_Left == false)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
        yield return null;
    }

    IEnumerator Rush()   // ���� �ڷ�ƾ.
    {
        if (playerLoc < boarLoc) // �÷��̾ ���ʿ� �ִٸ�.
        {
            Enemy_Left = true;
        }
        else if (playerLoc > boarLoc)    // �÷��̾ �����ʿ� �ִٸ�.
        {
            Enemy_Left = false;
        }
        yield return new WaitForSeconds(1f);    // �÷��̾� ���� �� �޷����� ���� �� ���� 1��
        Attacking = true;

        animator.SetBool("Rush", true);

        Enemy_Speed = 10f;        //  �ӵ� 10 ����.
    }

    IEnumerator StopRush()
    {
        animator.SetBool("Rush", false);
        animator.SetTrigger("Hit");
        Attacking = false;  // ���� �� ����
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
            // ��ǥ ���� ���
            Vector3 targetDirection = (TeleportPos[0].position - transform.position).normalized;
            rigid.velocity = targetDirection * 7.5f;

            // ������Ʈ A�� ��ǥ ��ġ�� ������ ���
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
        atkPattern = Pattern[Random.Range(0, Pattern.Count)];     // ���� ��ȣ�� 1 ~ 6(Max-1)���� �������� ����.
        if (this.gameObject.layer != LayerMask.NameToLayer("Dieenemy"))
        {
            Invoke("GolemRandomAtk", randNum);
        }
    }

    void GolemMove()  // ���� ���������� �����̴� �Լ�
    {
        
    }
    void GolemDie()   // ���� �״� �ִϸ��̼�
    {
        if (Enemy_HP <= 0 && !Dying)
        {
            animator.runtimeAnimatorController = PageAnimators[1];
            Dying = true;
            bossBox.enabled = false;
            animator.SetTrigger("Die");
        }
    }

    void GolemAttack()    // ���� ���� ����
    {
        switch (atkPattern)
        {
            case 1:
                ScaleFlip();
                float range = 11;
                if (TargetFind > 0) //�������� ��
                {
                    range *= -1;
                }
                else if (TargetFind < 0) //���������� ��
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
        if (TargetFind > 0) //�������� ��
        {
            Vector3 newLocalScale = new Vector3(1, 1, 1);
            this.transform.localScale = newLocalScale;
        }
        else if (TargetFind < 0) //���������� ��
        {
            Vector3 newLocalScale = new Vector3(-1, 1, 1);
            this.transform.localScale = newLocalScale;
        }
    }

    void Punch()
    {
        Attacking = false;  // ������ ����
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
        Attacking = false;  // ������ ����
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
        Attacking = false;  // ������ ����
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
        
        float angle = CircleMissileCount * (360f / CircleAngles.Length); // ���� ���
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // ���� �ѷ� ���� ��ġ ���

        Quaternion rotation = Quaternion.Euler(0f, 0f, CircleAngles[CircleMissileCount]);

        GameObject missile = Instantiate(CircleMissilePB, spawnPosition, rotation);
        missile.GetComponent<CircleMissile>().boss = this;
        CircleMissileCount++;
    }

    void CircleMissileCreatePage2()
    {
        float circleRadius = 3f;

        float angle = CircleMissileCount * (360f / CircleAngles.Length); // ���� ���
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // ���� �ѷ� ���� ��ġ ���

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
        Attacking = false;  // ������ ����
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
        Attacking = false;  // ������ ����
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
        Attacking = false;  // ������ ����
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

        float angle = MagicArrowCount * (360f / MagicArrowAngles.Length); // ���� ���
        Vector3 spawnPosition = BossCenter.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * circleRadius; // ���� �ѷ� ���� ��ġ ���

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