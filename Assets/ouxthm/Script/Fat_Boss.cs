using System.Collections;
using System.Collections.Generic;
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

        Move();
    }

    void Move()
    {
        if (target.transform.position.x < transform.position.x)  // �÷��̾ ���ʿ� ���� ��.
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = true;    // x�� �ø�.
            }
            else if (Attacking == true && animator.GetBool("Running"))
            {
                spriteRenderer.flipX = true;    // x�� �ø�.
            }
            animator.SetBool("Walking", true);
            transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
        }
        else if(target.transform.position.x > transform.position.x)     // �÷��̾ �����ʿ� ���� ��.
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x�� �ø����� ����.
            }
            else if (Attacking == true && animator.GetBool("Running"))
            {
                spriteRenderer.flipX = false;    // x�� �ø�.
            }
            animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
        }
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance && Gap_Distance < RushDistance)  // ��Ÿ� ���̸�, �Ÿ� ���̰� ���� �Ÿ����� ª����
        {
            animator.SetBool("Walking", false);
            Fat_speed = 0;  // �ӵ� 0.
            Debug.Log("��Ÿ� ���̾�");
            StartCoroutine(Attack());   // ���� �ڷ�ƾ ����.
            yield return new WaitForSeconds(0.1f);
        }
        else if (Gap_Distance < RushDistance && Gap_Distance > AtkDistance && Fat_HP <= 50)
        {
            Attacking = true;
            //animator.SetBool("Walking", false);
            Debug.Log("�� �ð�");
            StartCoroutine(Running());
        }
        else if (Gap_Distance > AtkDistance) // ��Ÿ� ���̸� .
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("����Ǵ�?");
            StartCoroutine(Think());    // Think �ڷ�ƾ ����.
        }
    }

    IEnumerator Attack()
    {
       yield return new WaitForSeconds(0f);

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

    IEnumerator Left_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�.
        animator.SetTrigger("Left_Hooking");
        Debug.Log("�޼�");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // �ӵ� 2
        Attacking = false;  // ������ ����
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }

    IEnumerator Right_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�.
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("������");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // �ӵ� 2.
        Attacking = false;  // ���� �� ����.
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }

    IEnumerator Running()
    {
        animator.SetBool("Running", true);
        Fat_speed = 10f;      // Fat_Boss�� �ӵ��� �� �ٲ�.
        Debug.Log("�ٴ� ��");
        yield return new WaitForSeconds(5f);   
        Debug.Log("5�� ��ٸ�");
        animator.SetBool("Running", false);
        Fat_speed = 0;      // �ӵ� 0���� �ؼ� ������ ���� �ݵ� �ֱ�.
        yield return new WaitForSeconds(2f);
        Debug.Log("2�� �� ��ٸ��� �ӵ� 2�� �ٲٱ�");
        Attacking = false;  // ���� �� ����
        Fat_speed = 2;  // �ȱ� ���� �ӵ� 2.
        StartCoroutine(Think());    // Think �ڷ�ƾ ����.
    }
}
