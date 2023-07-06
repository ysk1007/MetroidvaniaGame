using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: ������, 2: �������� ���� ����, 3:�������, 4:���Ÿ� ����
    public float Enemy_HP;  // ���� ü��
    public float Enemy_Power;   //���� ���ݷ�
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public float Enemy_Atk_Speed;    // ���� ���ݼӵ�
    public bool Attacking = false;  // ���� ������ Ȯ���ϴ� ����
    public bool Hit_Set;    // ���ظ� �Ծ����� Ȯ���ϴ� ����
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
    public float atkY;  // ���� �ݶ��̴��� y��
    public bool enemyHit = false;   // ���� �������� �������� Ȯ���ϴ� ����
    public float old_Speed;     // �ӵ� �� ���ϱ� �� �ӵ� ��
    public float dTime;

    public GameObject Split_Slime;

    Transform spawn;    // �п��� ������ ������ ��ġ 1
    Transform spawn2;   // �п��� ������ ������ ��ġ 2
    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    BoxCollider2D Box;
    Transform posi;
    BoxCollider2D Boxs;
   
    public Transform Pos;
    public Arrow arrow;

    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�(�߻�)

    public virtual void Short_Monster(Transform target) 
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        Sensing(target, rayHit);
        Sensor();
    }
    public virtual void onetime()   // Awake�� ����
    {
        Pos = GetComponent<Transform>();
        StartCoroutine(Think());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Boxs = GetComponent<BoxCollider2D>();
            Bump();
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
                else
                    Debug.Log("������");               // ���� �Ͼ �ذ��ؾ� �� ����ó�� �����ϸ� ��
            }
        }
    }

    void Move() // �̵�
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        gameObject.transform.Translate(new Vector2(nextDirX, 0) * Time.deltaTime * Enemy_Speed);

        if(Enemy_Mod == 9)
        {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        }

        if (nextDirX == -1)
        {
            spriteRenderer.flipX = false;
        }
        else if (nextDirX == 1)
        {
            spriteRenderer.flipX = true;
        }
        if (Enemy_Mod == 5 && (nextDirX == 1 || nextDirX == -1))
        {
            animator.SetBool("Run", true);
        }
        else if (Enemy_Mod == 5 && (nextDirX == 0))
        {
            animator.SetBool("Run", false);
        }
    }

    IEnumerator Think() // �ڵ����� ���� ������ ���ϴ� �ڷ�ƾ
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
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
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
        StartCoroutine(Think());
    }

    void delayTime()
    {
        dTime -= Time.deltaTime;
        if (dTime > 0)
        {
            delayTime();
        }
        else if (dTime <= 0)
        {
            return;
        }
    }

    public IEnumerator Hit(float damage) // ���� �Լ�
    {
        posi = this.gameObject.GetComponent<Transform>();
        enemyHit = true;
        animator = this.GetComponentInChildren<Animator>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();

        Enemy_HP -= damage;
        Debug.Log("������ ����");
        if (Enemy_HP > 0) // Enemy�� ü���� 0 �̻��� ��
        {
            if (!animator.GetBool("Hit"))
            {
                /*�� ����Ŭ ���� 
                  0 ����� �� ��° ���� �� ó�� ��Ʈ �ִϸ��̼� ����
                  �ִϸ��̼� ������ 1.5 �õ� ���ǵ� ����׿� 1.5 ����� ����*/

                if(Enemy_Speed > 0)
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
        else if (Enemy_HP <= 0 && Enemy_Mod != 3) // Enemy�� ü���� 0�� ���ų� ������ ��(����)
        {
            Dying = true;
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            animator.SetTrigger("Die");
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            yield return new WaitForSeconds(Enemy_Dying_anim_Time );
            enemyHit = false;
            if(Enemy_Mod == 9 && posi.localScale.y > 1f)   // �п� ������ ���
            {
                Debug.Log("�п� ����");
                StartCoroutine(Split());
                this.gameObject.SetActive(false);
            }       
            else if (Enemy_Mod == 9 && posi.localScale.y <= 1f)
            {
                Debug.Log("���� ���ٰ�");
                this.gameObject.SetActive(false);   // clone slime ����
                //Destroy();
            }
            else if(Enemy_Mod != 9)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 3) // ���� ���� ����)
        {
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
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.gameObject.SetActive(false);
        }
        
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
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Debug.Log("dddddd");
                            Attacking = true;
                            if (Enemy_Mod == 5)
                            {
                                Attack();
                            }
                            else if (Enemy_Mod == 9)
                            {
                                Debug.Log("���� ���� ����");
                                StartCoroutine(slimeJump());
                            }
                            else
                            {
                                Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                            }
                            
                        }
                    }
                    else if (nextDirX == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (0,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                        }
                    }
                }
                else if (nextDirX == 1 && Enemy_Mod == 3)  // ���� ������ �÷��̾� ����
                {
                    spriteRenderer.flipX = true;
                    Vector2 resHeight = new Vector2(-1.5f, 1f);
                    Vector2 playerPoint = (Vector2)target.transform.position + resHeight;   // �÷��̾�� ���ļ� �����ϴ� ���� �����ϱ� ���� ���ο� ������ ������
                    transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);   // resHeight�� �����־� �÷��̾��� �Ʒ����� �������� �ʵ��� ����
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y)
                    {
                        Attacking = true;
                        Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����

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
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
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
                    else if (nextDirX == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����
                        }
                    }
                }
                else if (nextDirX == -1 && Enemy_Mod == 3)  // ���� ������ �÷��̾� ����
                {
                    spriteRenderer.flipX = false;
                    Vector2 resHeight = new Vector2(1.5f, 1f);
                    Vector2 playerPoint = (Vector2)target.transform.position + resHeight;       // �÷��̾�� ���ļ� �����ϴ� ���� �����ϱ� ���� ���ο� ������ ������(Vector2�� �����Ͽ� +�� �� �� Vector2���� 3���� ��ȣ���� �ʰ� ��)
                    transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y) // Ÿ���� ��ġ�� 2.5f�� ���ؼ� Bee�� �÷��̾��� �Ʒ��ʿ��� �����ϴ� ���� ����
                    {
                        Attacking = true;
                        Invoke("Attack", atkDelay); // ���� ��Ÿ�� ����

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
        Transform AtkTransform = transform.GetChild(0);
        animator = this.GetComponentInChildren<Animator>();
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // ���� ������Ʈ�� ù��° �ڽ� ������Ʈ�� ���Ե� BoxCollider2D�� ������.
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();

        if (!Dying && Enemy_Mod == 3)
        {
            Bcollider.enabled = true;   // ���� �ڽ� �ݶ��̴� ����

            if (spriteRenderer.flipX == true)   // �̹��� �ø����� �� ���� ���� x�� ��ȯ ���ǹ�
            {
                AtkTransform.localPosition = new Vector3(atkX, atkY);   // ������ ���� �ݶ��̴� �ڽ��� x��ǥ�� y��ǥ
            }
            else if (spriteRenderer.flipX == false) // ������ �� ��
            {
                AtkTransform.localPosition = new Vector3(-atkX, atkY);  // ������ ���� �ݶ��̴� �ڽ��� -x��ǥ�� y��ǥ
            }

            GiveDamage();       // �÷��̾�� ������ �ִ� �Լ� ����
            animator.SetTrigger("Attack");  // �� ���ݿ�
            animator.SetBool("Attacking", true);    // �� ���� Ȯ���ϴ� ���� ��(�ʹ� �����Ǽ� ��� �� ��)
            Enemy_Speed = 0;

            if (Attacking == true)
            {
                Invoke("offAttkack", 0.5f);
            }

        }
        else if(!Dying && Enemy_Mod == 5)
        {
            StartCoroutine(Boom());
        }

    }
    public void offAttkack() // ���� ���� �Լ�
    {
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // ���� ������Ʈ�� ù��° �ڽ� ������Ʈ�� ���Ե� BoxCollider2D�� ������. 
        Attacking = false;
        animator.SetBool("Attacking", false);
        Enemy_Speed = 3f;
        Bcollider.enabled = false;
    }
    
    public void GiveDamage()    // �÷��̾�� �������� �ִ� �Լ�
    {
        posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        Box = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(posi.position, Box.size, 0);
        
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
            {
                collider.GetComponent<Player>().Playerhurt(Enemy_Power, Pos.position);
                Debug.Log("������ ��");

            }
            Debug.Log("������ �� ��");
        }

    }

    public void Bump()      // �浹 ������ �Լ�
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(Pos.position, Boxs.size, 0);
        Player player = GetComponent<Player>();
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
                collider.GetComponent<Player>().Playerhurt(Bump_Power, Pos.position);
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
        this.gameObject.SetActive(false);
    }

    public IEnumerator Split()  // ������ �п� �Լ�
    {
        //posi = this.gameObject.GetComponent<Transform>();
        spawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        spawn2 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        Debug.Log("�п� �Ұ�");
        GameObject splitSlime = Instantiate(Split_Slime, spawn.position, spawn.rotation);
        GameObject splitSlime2 = Instantiate(Split_Slime, spawn2.position, spawn2.rotation);
        yield return null;
    }

    public IEnumerator slimeJump() //������ ��������
    {
        yield return null;
        Debug.Log("���� ����");

    }

    /*public void Destroy()
    {
        Debug.Log("�ƿ� ����");
        Destroy(Split_Slime); 

    }*/

}