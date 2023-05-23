using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump ���� ���� ����
    public float Speed; //Move �ӵ� ���� ����
    public float curTime, coolTime = 2;  // ���Ӱ����� ������ �ð�
    public float skcurTime, skcoolTime = 5;  // ��ų ��Ÿ��
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public bool isjump = false;     //���������� üũ
    public bool isSkill = false;    //��ų Ȯ��
    public bool isAttacking = false; //���ݻ��� Ȯ��
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
    public float Dmg = 7;
    public GameObject GameManager;
    public GameObject attackRange;

    private Enemy enemyarrow;
    private Arrow arrow;

    public BoxCollider2D box; //���� ����
    public Transform pos;   //���ݹڽ� ��ġ
    public GameObject Arrow; //ȭ�� ������Ʈ
    public Transform Arrowpos;
    public SpriteRenderer spriteRenderer;
    private SpriteRenderer AttackRennderer;
    Rigidbody2D rigid;
    Animator anim;
    public Enemy enemy;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //���۽� ���� ���� Ƚ�� ����
        Speed = 4;  //���۽� �⺻ �̵��ӵ�
        jumpPower = 17; //�⺻ ��������
        pos = transform.GetChild(1).GetComponentInChildren<Transform>(); //attackRange�� ��ġ���� pos�� ����
        Arrowpos = transform.GetChild(0).GetComponentInChildren<Transform>(); //attackRange�� ��ġ���� pos�� ����
        arrow = GetComponent<Arrow>();
    }
    void Update()
    {
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�

    }

    void Player_Move() //Player �̵�, ����
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill)    //���� ���������Ͻ� �̵� �Ұ���
        {
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
                StartCoroutine(DownJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
            //gameObject.layer = LayerMask.NameToLayer("Jump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpCnt--;
            isGround = false;
        }

        /*if (transform.position.y < -10)
         {
            PlayerReposition();
         }*/

    }

    public void Player_Attack() //Player ���ݸ���
    {
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)
        {
            WeaponChage += 1;      
            // Sword Dmg = 7, Axe Dmg = 10, Arrow Dmg = 5
            if(WeaponChage == 2)
            {
                attackRange.tag = "Axe";
                Dmg = 10;
            }
            else if(WeaponChage == 3)
            {
                attackRange.tag = "Arrow";
                this.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                Dmg = 7;
                attackRange.tag = "Sword";
                WeaponChage = 1;
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && skcurTime <= 0 && !anim.GetBool("Sliding"))
        {
            skcurTime = skcoolTime;
            isSkill = true;
            StartCoroutine(Skill());
        }
        else
            skcurTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill)
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
            curTime -= Time.deltaTime;
    }

    public void AttackDamage()// Player ���ݽ� ������ ������� �Ѱ��ֱ�
    {
        box = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
        if (box != null)    //���� ���� �ȿ� null���� �ƴҶ���
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, box.size, 0); //���� ���� �ȿ� �ݶ��̴��� 
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy")
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
        yield return null;
        if (WeaponChage == 1) //sword ��ų
        {

        }
        if (WeaponChage == 2) //Axe ��ų
        {

        }
        if (WeaponChage == 3) //Arrow ��ų
        {
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
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
            //gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)   //Player�� ���� ���� ������
    {
        if (collision.gameObject.tag == "Wall") //���ʺ��� �پ ��������
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

    private IEnumerator Sliding() //�����̵� ����
    {
        GameManager.GetComponent<Ui_Controller>().Sliding();
        Speed = 0;
        isSlide = true;
        gameObject.tag = "Sliding";
        gameObject.layer = LayerMask.NameToLayer("Sliding");
        anim.SetBool("Sliding", true);
        if (slideDir == 1) //���������� �����̵�
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if (slideDir == -1) //�������� �����̵�
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //���� �ð�
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = 4;
        yield return new WaitForSeconds(2f); //�����̵� ��Ÿ��
        isSlide = false;

    }

    public void Playerhurt(float Damage, Vector2 pos) // Player�� ���ݹ��� ��
    {
        if (!ishurt)
        {
            ishurt = true;
            CurrentHp = CurrentHp - Damage;

            if (CurrentHp <= 0)
            {
                Invoke("Die", 3f);
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

    }

    IEnumerator Attack_delay() //���Ӱ��� ������
    {
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
    }

    void Sword_attack() //Sword ���� ���� ����
    {
        if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
        {
            SwdCnt++;
            Dmg = 10;
        }
        else
        {
            SwdCnt = 1;
            Dmg = 7;
        }

        if (SwdCnt == 1)
        {

            box.size = new Vector2(3.5f, 2.5f);
            box.offset = new Vector2(1.5f, 0);
        }
        else
        {
            box.size = new Vector2(4f, 2.5f);
            if (slideDir == 1)  //���� ���⺰ box.offset���� �ٸ��� ����
                box.offset = new Vector2(2, 0);
            else
                box.offset = new Vector2(1, 0);

        }

        //ù��° ���� ����� �Լ� ������ �ִϸ��̼� �κп� �������
        isdelay = true;
        anim.SetFloat("Sword", SwdCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
        anim.SetTrigger("sword_atk");

        if (SwdCnt > 1)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
            SwdCnt = 0;
        curTime = coolTime; // �޺� ���� ���ѽð�
    }   

    void Axe_attack()   //Axe ���� ���� ����
    {
        if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
            AxeCnt++;
        else
            AxeCnt = 1;


        if(AxeCnt == 1) // ���ۺ� ����� ����
        {
            Dmg = 10;
            box.size = new Vector2(5f, 2.5f);
            if(slideDir == 1)   //���� ���⺰ box.offset���� �ٸ��� ����
                box.offset = new Vector2(2, 0);
            else
                box.offset = new Vector2(1, 0);
        }
        else if (AxeCnt == 2)    
        {
            Dmg = 15;
            box.size = new Vector2(4.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(2.5f, 0);
            else
                box.offset = new Vector2(0.5f, 0);
            
        }
        else if (AxeCnt == 3)
        {
            Dmg = 20;
            attackDash = 6;
            box.size = new Vector2(5.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(3.5f, 0);
            else
                box.offset = new Vector2(-0.5f, 0); 
        }
            

        isdelay = true;
        anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
        anim.SetTrigger("axe_atk");

        if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
            AxeCnt = 0;

        curTime = coolTime + 0.5f;  // �޺� ���� ���ѽð�
    }       

    IEnumerator Arrow_attack() //ȭ�� �Ϲݰ��� �� ��ų - �ִϸ��̼� Ư�� �κп��� ����ǰ� ����Ƽ���� ������
    {
        yield return null;
        Transform ArrowposTransform = transform.GetChild(0);
        if (slideDir == 1)   //���� ���⺰ Arrowpos ��ġ�� ����
            ArrowposTransform.localPosition = new Vector3(1, 0.2f); 
        else
            ArrowposTransform.localPosition = new Vector3(-1, 0.2f); 

        Instantiate(Arrow, Arrowpos.position, transform.rotation);
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
        yield return new WaitForSeconds(1.3f);
        isSkill = false;
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
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator Blink() // �����ð����� ���� ȿ��
    {
        while (ishurt)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    IEnumerator DownJump()
    {
        anim.SetBool("Player_Jump", true);
        gameObject.layer = LayerMask.NameToLayer("Jump");
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    } //��� �Ʒ��� ��������

    void Die() //Player ����� ��������Ʈ ����
    {
        Destroy(gameObject);
    }

    void PlayerReposition()
    {
        transform.position = new Vector3(-30, -7.5f, 0);
    }
}