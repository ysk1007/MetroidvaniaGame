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
    public float attackDash = 5f; //ū ���ݽ� ������ �̵��ϴ� ��
    public float slideSpeed = 13;   //�����̵� �ӵ�
    public int slideDir;    //�����̵� ���Ⱚ
    public float MaxHp;    //�÷��̾� �ִ� HP
    public float CurrentHp;    //�÷��̾� ���� HP
    public bool ishurt = false; //�ǰ� Ȯ��
    public bool isknockback = false;    //�˹� Ȯ��
    public float Dmg = 7;
    public GameObject GameManager;
    public GameObject attackRange;

    public BoxCollider2D box; //���� ����
    public GameObject Arrow; //ȭ�� ������Ʈ
    public Transform pos;   //���ݹڽ� ��ġ
    public SpriteRenderer spriteRenderer;
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
        pos = GetComponent<Transform>(); // pos ������ Transform ������Ʈ �Ҵ�
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
            transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
            anim.SetBool("Player_Walk", true);

            if (Direction < 0) //���� �ٶ󺸱�
            {
                transform.eulerAngles = new Vector2(0, 180);
                slideDir = -1;
            }
            else if (Direction > 0) //������ �ٶ󺸱�
            {
                transform.eulerAngles = new Vector2(0, 0);
                slideDir = 1;
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
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(DownJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
            gameObject.layer = LayerMask.NameToLayer("Jump");
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
            if (WeaponChage == 2)
            {
                attackRange.tag = "Axe";
                Dmg = 10;
            }
            else if (WeaponChage == 3)
            {
                attackRange.tag = "Arrow";
            }
            else
            {
                Dmg = 7;
                attackRange.tag = "Sword";
                WeaponChage = 1;
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
                //this.attackRange.gameObject.SetActive(true);
                if (WeaponChage == 1)    //Sword ����
                {

                    if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                        SwdCnt++;
                    else
                        SwdCnt = 1;

                    isdelay = true;
                    anim.SetFloat("Sword", SwdCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                    anim.SetTrigger("sword_atk");

                    if (SwdCnt > 1)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                    {
                        StartCoroutine(Dash_delay());
                        SwdCnt = 0;
                    }
                    else
                        AttackDamage();

                    curTime = coolTime;
                }
                if (WeaponChage == 2)    //Axe ����
                {
                    if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                        AxeCnt++;
                    else
                        AxeCnt = 1;

                    if (AxeCnt == 2)    //���ۺ� ������Ÿ�� �߰�
                    {
                        delayTime += 0.2f;
                        StartCoroutine(Dash_delay());
                    }
                    else if (AxeCnt == 3)
                    {
                        delayTime += 0.55f;
                        StartCoroutine(Dash_delay());
                    }
                    else
                        AttackDamage();

                    isdelay = true;
                    anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                    anim.SetTrigger("axe_atk");

                    if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                        AxeCnt = 0;

                    curTime = coolTime + 0.5f;
                }
                if (WeaponChage == 3)    //Bow ����
                {
                    isdelay = true;
                    StartCoroutine(arrow_delay());
                }
                StartCoroutine(attack_delay());    //������ ���� ���ݱ��� ������
            }
        }
        else
            curTime -= Time.deltaTime;
    }

    IEnumerator Skill()
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
            StartCoroutine(arrow_delay());
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
        }

    }

    public void AttackDamage()
    {
        box = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
        if (box != null)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, box.size, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy")
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


    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // �� �ݶ������� Player�� ��� ������ ����, �������� �� �ݶ����� ���� �� ���� ����
    {
        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Tilemap", "Pad"));
        Debug.DrawRay(rigid.position, Vector3.down * 1f, Color.red);

        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
        if (collision.gameObject.tag == "Pad" || collision.gameObject.tag == "Tilemap" && !isGround)
        {
            anim.SetBool("Player_Jump", false);
            isjump = false;
            isGround = true;
        }
        if (rayHitDown.collider != null)
        {
            JumpCnt = JumpCount;
            gameObject.layer = LayerMask.NameToLayer("Player");
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
        anim.SetBool("Sliding", true);
        if (slideDir == 1) //���������� �����̵�
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if (slideDir == -1) //�������� �����̵�
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //���� �ð�
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        Speed = 4;
        yield return new WaitForSeconds(2f); //�����̵� ��Ÿ��
        isSlide = false;

    }


    public void Playerhurt(float Damage) // Player�� ���ݹ��� ��
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
                StartCoroutine(Knockback());
                StartCoroutine(Routine());
                StartCoroutine(Blink());
            }
        }

    }

    IEnumerator attack_delay() //���Ӱ��� ������
    {
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
        //this.attackRange.gameObject.SetActive(false);
    }

    IEnumerator arrow_delay() //ȭ����ݽ� ������ �ð� ���� - �ִϸ��̼ǰ� �����ֱ� ����
    {
        anim.SetTrigger("arrow_atk");
        if (isSkill)
            yield return new WaitForSeconds(0.7f);
        else
            yield return new WaitForSeconds(0.5f);
        if (slideDir == 1)  //�÷��̾ �ٶ󺸴� ���� ����
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime); // Ȱ ���ݽ� �ణ�� �ڷ� �и�
            else
                rigid.velocity = new Vector2(transform.localScale.x - 10f, Time.deltaTime);
        }
        else
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 10f, Time.deltaTime);
        }
        Instantiate(Arrow, pos.position, transform.rotation);

    }

    IEnumerator SkillTime() //��ų ���� �ð�
    {
        yield return new WaitForSeconds(1.3f);
        isSkill = false;
    }

    IEnumerator Dash_delay() //���ݽ� �ణ�� �������� ������ ���� �̵�
    {
        if (SwdCnt == 2)
        {
            yield return new WaitForSeconds(0.5f);
            box.size = new Vector2(3, 2);
        }
        else if (AxeCnt == 2)
        {
            yield return new WaitForSeconds(0.8f);
            box.size = new Vector2(3, 2);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            box.size = new Vector2(4, 2);
        }

        if (slideDir == -1)
        {
            rigid.velocity = new Vector2(transform.localScale.x - attackDash, Time.deltaTime);
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x + attackDash, Time.deltaTime);
        }

        AttackDamage();
        box.size = new Vector2(2, 2);
    }

    IEnumerator Knockback() //���������� �˹�
    {
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.2f) //�˹� ���ӽð�
        {
            if (slideDir == -1)
                transform.Translate(Vector2.left * Speed * 2 * Time.deltaTime);
            else
                transform.Translate(Vector2.right * Speed * 2 * Time.deltaTime * -1);

            ctime += Time.deltaTime;
            yield return null;
        }
        isknockback = false;
    }

    IEnumerator Routine() // ���������� ��񵿾� ����
    {
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