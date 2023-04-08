using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fat_Boss : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider2D boxcollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform target;
    GameObject GameObject;

    public float Fat_speed = 2f; // Fat_Boss�� �ӵ�.
    public int Fat_pattern;   // Fat_Boss�� ������ ������ ����.
    public float Gap_Distance = 99;  // Fat_Boss�� Player ���� �Ÿ�.
    public float AtkDistance = 5f;  // Fat_Boss�� ���� ��Ÿ�.
    public float RushDistance = 10f;    // Fat_Boss�� ���� ��Ÿ�
    public float Fat_HP;    // Fat_Boss�� ü��.
    public bool Fat_Left = true;    // Fat_Boss�� ����.
    public bool Attacking;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        target = GameObject.Find("Player").transform;   // ������Ʈ �̸��� Player�� transform.
        Fat_HP = 10f;
        AtkDistance = 5f;   // Fat_Boss�� ���� ��Ÿ�.

        Attacking = false;
        StartCoroutine(Think());

    }

    void Update()
    {
        Gap_Distance = Mathf.Abs(target.position.x - transform.position.x); // �÷��̾�� Fat_Boss�� ���̰Ÿ�.

        StartCoroutine(Move());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")    // collicoin = ��Ҵٴ� ��, ���� ������Ʈ�� �±װ� Enemy�� ��
        {
            StartCoroutine(StopRunning());   
        }
    }


    IEnumerator Move()
    {
        //  ���� �ִϸ��̼��� �ƴ� �� �����̴� Ʈ��������Ʈ�� �� �� �ߵ��ϴ� Ʈ��������Ʈ ���� ���� ��Ű�� �� �� �÷��̾ �����ϰ� ������ ���� ������?
        if (target.transform.position.x < transform.position.x && !animator.GetBool("Running") && !animator.GetBool("Collision"))  // �÷��̾ ���ʿ� ���� ���� �ٴ� �ִϸ��̼��� �ƴ϶��.
        {
            //animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.

            if (Attacking == false)     // ���� ���� �ƴϸ�.
            {
                spriteRenderer.flipX = true;    // x�� �ø�.
            }
         }
        else if (target.transform.position.x > transform.position.x && !animator.GetBool("Running") && !animator.GetBool("Collision"))     // �÷��̾ �����ʿ� ���� ���� �ٴ� �ִϸ��̼��� �ƴ϶��.
        {
            //animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x�� �ø����� ����.
            }
        }
        else if (animator.GetBool("Running") && Fat_Left == true)       // �ٴ� �ִϸ��̼��� ���� ���̰�, Fat_Left�� ���� true���
        {
            gameObject.transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed );   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
        }
        else if (animator.GetBool("Running") && Fat_Left == false)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
        {
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
        }
        
        yield return null;
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance && Gap_Distance < RushDistance)  // ��Ÿ� ���̸�, �Ÿ� ���̰� ���� �Ÿ����� ª����.
        {
            //animator.SetBool("Walking", false);
            Fat_speed = 0;  // �ӵ� 0.
            Debug.Log("��Ÿ� ���̾�");
            StartCoroutine(Attack());   // ���� �ڷ�ƾ ����.
            yield return null;
        }
        else if (Gap_Distance < RushDistance && Gap_Distance > AtkDistance && Fat_HP <= 50)     // �Ÿ� ���̰� ���� �Ÿ����� ª��, �Ÿ����̰� ��Ÿ����� ���
        {
            //animator.SetBool("Walking", false);
            Attacking = true;
            Debug.Log("�� �ð�");
            StartCoroutine(Running());
        }
        else if (Gap_Distance > AtkDistance) // ��Ÿ� ���̸� .
        {
            yield return null;
            Debug.Log("����Ǵ�?");
            StartCoroutine(Think());    // Think �ڷ�ƾ ����.
        }
    }

    IEnumerator Attack()
    {
        yield return null;

       if (Attacking == false)
       {
            Attacking = true;   // ���� ��.
            Fat_pattern = Random.Range(1, 3);     // ���� ��ȣ�� 1 ~ 2���� �������� ����.
        }

        if (Fat_pattern == 1)
        {
            StartCoroutine(Left_Hooking());     // �޼� ���� �ڷ�ƾ ����.
        }
        if(Fat_pattern == 2)
        {
            StartCoroutine(Right_Hooking());    // ������ ���� �ڷ�ƾ ����.
        }      
    }

    IEnumerator Left_Hooking()  // �޼� ���� �ڷ�ƾ.
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�.
        animator.SetTrigger("Left_Hooking");
        Debug.Log("�޼�");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // �ӵ� 2
        Attacking = false;  // ������ ����
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }

    IEnumerator Right_Hooking() // ������ ���� �ڷ�ƾ.
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�.
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("������");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // �ӵ� 2.
        Attacking = false;  // ���� �� ����.
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }

    IEnumerator Running()   // ���� �ڷ�ƾ.
    {
        yield return null;
        if (target.transform.position.x < transform.position.x) // �÷��̾ Fat_Boss���� ���ʿ� �ִٸ�.
        {
            Fat_Left = true;    
        }
        else if (target.transform.position.x > transform.position.x)    // �÷��̾ Fat_Boss���� �����ʿ� �ִٸ�.
        {
            Fat_Left = false;
        }
        Fat_speed = 15f;        // Fat_Boss �ӵ� 10 ����.
        animator.SetBool("Running", true);
        yield return new WaitForSeconds(2f);
        Debug.Log("�ٴ� ��");
        
    }

    IEnumerator StopRunning()
    {
        StopCoroutine(Move());
        Debug.Log("�浹");
        animator.SetBool("Collision", true);
        Attacking = false;  // ���� �� ����
        Fat_speed = 0;      // �ӵ� 0���� �ؼ� ������ ���� �ݵ� �ֱ�.
        yield return new WaitForSeconds(0.5f);
        if(Fat_Left == true)    
        {
            Fat_Left = false;   
        }
        else if(Fat_Left == false)  
        {
            Fat_Left = true;    
        }
        Debug.Log("2�� �� ��ٸ��� �ӵ� 2�� �ٲٱ�");
        animator.SetBool("Collision", false);
        animator.SetBool("Running", false);
        yield return new WaitForSeconds(0.7f);
        Fat_speed = 2;  // �ȱ� ���� �ӵ� 2.
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }
}
