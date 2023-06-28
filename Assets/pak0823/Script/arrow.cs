using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public float speed = 10f; // ȭ�� �̵� �ӵ�
    public float distance; // ȭ���� �����ϴ� �Ÿ�
    public LayerMask islayer; // �浹 ������ �� ���̾�
    public Transform pos; // ȭ�� ��ġ ����
    public bool SetSkill = false; // ��ų ��� ����
    public int Dmg = 5; //����� ����
    public float turnSpeed = 1f; // ȭ���� ���� �ӵ�
    public float maxTrackingDistance = 8f; // ���� ������ �ִ� �Ÿ�
    public float maxTrackingAngle = 70f; // ���� ������ �ִ� ����
    private Vector3 moveDirection;

    public SpriteRenderer spriteRenderer;
    private Collider2D coll; // Arrow ������Ʈ�� �ݶ��̴�
    private Transform target; // ���� ���
    private bool isTracking; // ȭ���� ���� ������ ����
    private float trackingDistance = 5f; // ���� ������ �Ÿ�
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // �̹� ������ ������� �������� ���θ� ����ϴ� Dictionary ����

    private void Start()
    {
        Player player = GetComponent<Player>();
        player = GameObject.FindObjectOfType<Player>(); // Player ��ũ��Ʈ�� ���� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
        coll = GetComponent<Collider2D>(); // Arrow ������Ʈ�� �ݶ��̴��� ������
        Invoke("DestroyArrow", 2f); // ���� �ð��� ���� �� ȭ���� �����ϴ� Invoke �Լ��� ȣ��
        pos = transform;

        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // �÷��̾ �������� �ٶ󺸸� ȭ���� ���������� �߻�
                moveDirection = Vector3.right;
                spriteRenderer.flipX = false;
            }
            else
            {
                // �÷��̾ ������ �ٶ󺸸� ȭ���� �������� �߻�
                moveDirection = Vector3.left;
                spriteRenderer.flipX = true;
            }
        }
    }

    private void Update()
    {
        if (player != null && player.isSkill == true)
        {
            SetSkill = true; // ��ų ��� ���̸� SetSkill ������ true�� ����
        }
        else
        {
            SetSkill = false; // ��ų ��� ���� �ƴϸ� SetSkill ������ false�� ����
        }


        if (SetSkill == true) // ��ų�������϶�
        {
            pos.position += moveDirection * speed * Time.deltaTime;
            Dmg = 10;
        }
        else
        {
            pos.position += moveDirection * speed * Time.deltaTime; //ȭ�� �⺻ �̵�
            Dmg = 5;
        }

        /*else  //�⺻���� �϶�
        {
            
            if (isTracking)  // �������϶�
            {
                if (target != null)
                {
                    // �� ĳ���͸� ���ϴ� ���� ���� ���
                    Vector2 direction = target.position - transform.position;
                    direction.Normalize(); // ���� ���͸� ����ȭ�Ͽ� ���̰� 1�� ���� ���ͷ� ����

                    // ȭ���� �ش� �������� �̵� �� ȸ��
                    transform.Translate(direction * speed * Time.deltaTime);
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                    // ������ �Ÿ��� ���� ������ �Ÿ����� �۾����� ���� ����
                    if (Vector3.Distance(transform.position, target.position) <= trackingDistance)
                    {
                        isTracking = false;
                    }
                }
                else
                {
                    // ����� ������ ���� ����
                    isTracking = false;
                }
            }
            else //�������� �ƴ� ��
            {

                pos.position += moveDirection * speed * Time.deltaTime; //ȭ�� �⺻ �̵�

                // ���� ��� Ž��
                RaycastHit2D rayHittargetX = Physics2D.Raycast(transform.position, transform.right, maxTrackingDistance, islayer);  //���鿡 �ִ� ��� Ž��
                RaycastHit2D rayHittargetU = Physics2D.Raycast(transform.position, Vector2.up, maxTrackingDistance, islayer);   //���ʿ� �ִ� ��� Ž��
                RaycastHit2D rayHittargetD = Physics2D.Raycast(transform.position, Vector2.down, maxTrackingDistance, islayer); //�Ʒ��ʿ� �ִ� ��� Ž��

                if (rayHittargetX.collider != null && rayHittargetX.collider.tag == "Enemy")
                {
                    // ���� ������ �ִ� ���� �ȿ� ������ ���� ����
                    Vector3 dir = rayHittargetX.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetX.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
                if (rayHittargetU.collider != null && rayHittargetU.collider.tag == "Enemy")
                {
                    Vector3 dir = rayHittargetU.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetU.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
                if (rayHittargetD.collider != null && rayHittargetD.collider.tag == "Enemy")
                {
                    Vector3 dir = rayHittargetD.collider.transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    if (Mathf.Abs(angle) <= maxTrackingAngle)
                    {
                        target = rayHittargetD.collider.transform;
                        trackingDistance = Vector3.Distance(transform.position, target.position);
                        isTracking = true;
                    }
                }
            }
        }*/
    }


    private void OnTriggerEnter2D(Collider2D collision) //ȭ���� �浹�� Ȯ���� ���� �� ���������
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.right, distance); // ȭ���� ������ �� �ִ� �Ÿ� ������ �浹�ϴ� ��ü�� ����
        if (SetSkill)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            if (collision.tag == "Enemy")
            {
                if (!hitDict.ContainsKey(rayHit.collider)) // �̹� ������ ������� ���� ���, Dictionary üũ
                {
                    //enemy.Hit(Dmg); // Enemy ��ũ��Ʈ�� Hit �Լ��� ȣ���� ������ �����
                    //enemy.Hit(Dmg);
                    hitDict.Add(rayHit.collider, true); // �� ������ Dictionary�� �߰�
                }
            }
        }
        else
        {
            if (collision.tag == "Enemy")
            {
                Debug.Log("Emeny����");
                //Enemy enemy = collision.transform.GetComponent<Enemy>();
                //StartCoroutine(enemy.Hit(Dmg)); // Enemy ��ũ��Ʈ�� Hit �Լ��� ȣ���� ������ �����
                //enemy.Hit(Dmg);   
                DestroyArrow();
            }
        }
        if (collision.tag == "Wall")
        {
            DestroyArrow();
        }
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}