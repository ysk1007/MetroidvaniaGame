using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    public float Gap_Distance_X ;  // ���� Player X �Ÿ�����
    public float Gap_Distance_Y ;  // ���� Player Y �Ÿ�����
    public int NextMove;  // ������ ���ڷ� ǥ��
    public float Enemy_Sensing_X;  // ���� X�� ���� ��Ÿ�
    public float Enemy_Sensing_Y;  // ���� Y�� ���� ��Ÿ�
    public float Enemy_Range_X; //���� X�� ���� ��Ÿ�
    public float Enemy_Range_Y; //���� Y�� ���� ��Ÿ�
    //public float Cooltime;  // ���� ��Ÿ��
 
    public bool Dying = false; // �״� ���� Ȯ���ϴ� ����
    public float Enemy_Dying_anim_Time;     // �״� �ִϸ��̼� �ð� ����

    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�(�߻� Ŭ����)
    
    public virtual void Short_Monster(Transform target) // �ٰŸ� ���� ����
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        Sensing(target, rayHit);
        Sensor();
    }
    public virtual void onetime()   // Awake�� ����
    {
        StartCoroutine(Think());
    }
    public virtual void Long_Monster()  // ���Ÿ� ���� ����
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()   // ���� ����
    {
        Debug.Log("Fly_Monster");
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        yield return null;

        animator = this.GetComponentInChildren<Animator>();
        if (collision.gameObject.tag == "Wall")    
        {
            Debug.Log(collision.gameObject.tag);
        }
        else if (collision.gameObject.tag == "Sword")
        {
            Debug.Log(collision.gameObject.tag);
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Axe")
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            StartCoroutine(Hit());
        }
    }
    void Move() // �÷��̾� ���� �� move
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Enemy_Sensing_X < Gap_Distance_X)    // �����Ÿ� �ۿ� ���� �� 
        {
            if (NextMove == -1)       // Enemy�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
        else if(Enemy_Sensing_X > Gap_Distance_X && Enemy_Sensing_Y < Gap_Distance_Y)
        {
            if (NextMove == -1)       // Enemy�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
    }

    

    IEnumerator Think() // �ڵ����� ���� ������ ���ϴ� �ڷ�ƾ
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1������ ������ �� ����
        if (NextMove != 0 && NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true;       // NextMove�� ���� 1�̸� x���� flip��
        }
        // ���
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    IEnumerator Turn() // �̹����� ������ �ڷ�ƾ
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        NextMove *= -1;   // NextMove�� -1�� ���� ������ȯ
        if(NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true; // NextMove ���� 1�̸� x���� flip��
        }    
        StopAllCoroutines();
        StartCoroutine(Think());
        yield return null;
    }

    IEnumerator Hit() // ���� �Լ�
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();
        Enemy_HP -= 5;
        if (Enemy_HP > 0) // Enemy�� ü���� 0 �̻��� ��
        {
            float old_Speed = Enemy_Speed;  // ���� �ӵ� ������ ������ ���� �ٸ� ������ �ӵ� ���� ����
            animator.SetTrigger("Hit");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(0.5f);
            Enemy_Speed = old_Speed;    // ���� �ӵ� ������ ����
            
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 1) // Enemy�� ü���� 0�� ���ų� ������ ��(����)
        {
            Dying = true;
            animator.SetTrigger("Die");
            Enemy_Speed = 0;            
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.SetActive(false);   // ������Ʈ ������� ��
        }
        else if(Enemy_HP <= 0 && Enemy_Mod == 3)// ���� ���� ����)
        {
            Dying = true;
            Enemy_Speed = 0;
            NextMove = 0;
            for(int i = 0; i < 4; i++)  // 3�� �ݺ�
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


    void Sensing(Transform target, RaycastHit2D rayHit)  // �÷��̾� ���� /* ���� Ÿ�� ������ �÷��̾� ������� �����ؾ� �� (Y���� ������ ���·� �̵��ϱ� ������ �÷��̾��� ��ǥ�� �޾Ƽ� �̵��ϸ� �ɵ�)*/
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X)      // Enemy�� X�� ��Ÿ��� ���� ��
        {
            if (Gap_Distance_Y < Enemy_Sensing_Y)
            {
                if (transform.position.x < target.position.x)            // ������ ����
                {
                    NextMove = 1;
                    if(Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4)  // ���� �߰���
                    {
                        if (NextMove == 1 && rayHit.collider != null)  // NextMove�� 1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                        {
                            spriteRenderer.flipX = true;

                            transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 4))
                            {
                                Attacking = true;
                                Attack();
                            }
                        }
                        else if (NextMove == 1 && rayHit.collider == null)
                        {
                            transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (0,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 4))
                            {
                                Attacking = true;
                                Attack();
                            }
                        }
                    }
                    else if( NextMove == 1 && Enemy_Mod == 3 )  // ���� ������ �÷��̾� ����
                    {
                        spriteRenderer.flipX = true;
                        Vector2 resHeight = new Vector2(0f, 2.5f);
                        transform.position = Vector2.MoveTowards(transform.position, (Vector2)target.position + resHeight, Enemy_Speed * Time.deltaTime);   // resHeight�� �����־� �÷��̾��� �Ʒ����� �������� �ʵ��� ����(������ �Ǵ��� ����� Ȯ���ؾ� ��)
                        if(Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 2f <= transform.position.y)
                        {
                            Attacking = true;
                            Attack();
                        }
                    } 
                    
                }
                else if (transform.position.x > target.position.x)      // ���� ����
                {
                    NextMove = -1;
                    if (Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4)
                    {
                        if (NextMove == -1 && rayHit.collider != null) // NextMove�� -1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                        {
                            spriteRenderer.flipX = false;
                            transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                            {
                                Attacking = true;
                                Attack();
                            }
                        }
                        else if (NextMove == -1 && rayHit.collider == null)
                        {
                            transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ

                            if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                            {
                                Attacking = true;
                                Attack();
                            }
                        }
                    }
                    else if (NextMove == -1 && Enemy_Mod == 3)  // ���� ������ �÷��̾� ����
                    {
                        spriteRenderer.flipX = false;
                        Vector2 resHeight = new Vector2(0f, 2.5f);
                        transform.position = Vector2.MoveTowards(transform.position , (Vector2)target.position + resHeight, Enemy_Speed * Time.deltaTime); // target.position �տ� Vector2�� �����Ͽ� +�� �� �� Vector2���� 3���� ��ȣ���� �ʰ� ��
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 2f <= transform.position.y) // Ÿ���� ��ġ�� 2.5f�� ���ؼ� Bee�� �÷��̾��� �Ʒ��ʿ��� �����ϴ� ���� ����
                        {
                            Attacking = true;
                            Attack();
                        }
                    }
                }
            }
            else if (Gap_Distance_Y > Enemy_Sensing_Y)
            {
                Move();
            }

        }
        else if (Gap_Distance_X > Enemy_Sensing_X && Gap_Distance_Y > Enemy_Sensing_Y)
        {
            Move();     // Move �Լ� ����
        }
    }

    public void Sensor()    // �÷��� ���� �Լ�
    {
        rigid = this.GetComponent<Rigidbody2D>();
        if(Enemy_Mod == 1 || Enemy_Mod == 2 || Enemy_Mod == 4)
        {
            // Enemy�� �� ĭ ���� ���� ��� ���� �ڱ� �ڽ��� ��ġ ���� (x)�� + NextMove���� ���ϰ� 1.2f�� ���Ѵ�.
            Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));
            // �������� �Ʒ��� ��Ƽ� �������� ������ ����(�������), LayMask.GetMask("")�� �ش��ϴ� ���̾ ��ĵ��
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)
            {
                StartCoroutine(Turn());
            }
        }
        
    }
       
    public void Attack() //���� �Լ�
    {
        Transform AtkTransform = transform.GetChild(0);
        animator = this.GetComponentInChildren<Animator>();
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (!Dying)
        {
            if (spriteRenderer.flipX == true)   // �̹��� �ø����� �� ���� ���� x�� ��ȯ ���ǹ�
            {
                AtkTransform.localPosition = new Vector3(0.8f, -1.2f);
            }
            else if (spriteRenderer.flipX == false)
            {
                AtkTransform.localPosition = new Vector3(-0.8f, -1.2f);
            }
            animator.SetTrigger("Attack");
            animator.SetBool("Attacking", true);
            Enemy_Speed = 0;
            if (Attacking == true)
            {
                Invoke("offAttkack", 0.5f);

            }
        }
        
    }
    public void offAttkack() // ���� ���� �Լ�
    {
        Attacking = false;
        animator.SetBool("Attacking", false);
        Enemy_Speed = 3f;
    }

}