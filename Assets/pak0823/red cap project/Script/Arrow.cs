using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public LayerMask islayer; // �浹 ������ �� ���̾�
    public Transform pos; // ȭ�� ��ġ ����
    public int Dmg = 5; //����� ����, ���Ͱ� �ǰݽ� ȭ�� ���������� �ޱ� ����
    public float speed = 15f; // ȭ�� �̵� �ӵ�
    private bool isSkill = false; // ��ų ��� ����
    private bool isMasterSkill = false;   //���� ��ų ��� ����
    private Vector3 moveDirection = Vector3.right; // ȭ���� ������ ����
    private float detectRadius = 2f; // ȭ���� ������ �� �ִ� �ݰ� (���� �ִ��� ������ Ȯ��)
    public GameObject BowMaster;  // ���õ� ������Ʈ

    public SpriteRenderer spriteRenderer;
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // �̹� ������ ������� �������� ���θ� ����ϴ� Dictionary ����

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player ��ũ��Ʈ�� ���� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
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
            if (player.isSkill == true)
            {
                isSkill = true; // ��ų ��� ���̸� SetSkill ������ true�� ����
            }
            else if (player.isMasterSkill == true)
            {
                isMasterSkill = true; // ���õ� ��ų ������̸� true�� ����
            }
            else
            {
                isSkill = false; // ��ų ��� ���� �ƴϸ� SetSkill ������ false�� ����
                isMasterSkill = false;
            }
        }
    }

    
    Collider2D FindCollider(Collider2D[] colliders)// ���� ����� �� ã��
    {
        Collider2D closestCollider = null;
        float distance = float.MaxValue;
        foreach (Collider2D coll in colliders)
        {
            if (LayerMask.LayerToName(coll.gameObject.layer) != "Pad" || LayerMask.LayerToName(coll.gameObject.layer) != "Tilemap")
            {
                float tempDist = Vector2.Distance(transform.position, coll.transform.position);
                if (tempDist < distance)
                {
                    closestCollider = coll;
                    distance = tempDist;
                }
            }
        }
            return closestCollider;
    }

    private void Update()
    {
        // ȭ�� Ž�� ��� �߰�
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, islayer);
        Collider2D closestCollider = FindCollider(hitColliders);

        if(isSkill == true) // ��ų�� ��
        {
            
            Dmg = 10;
            pos.position += moveDirection * speed * Time.deltaTime; // ȭ�� ���� �̵�
        }
        else if(isMasterSkill == true)    // ���õ� ��ų�� ��
        {
            Dmg = 20;
            pos.position += moveDirection * speed * Time.deltaTime; // ȭ�� ���� �̵�
        }
        else
        {
            Dmg = 5;
            if (closestCollider != null && closestCollider.tag != "Wall" && closestCollider.tag != "Pad" && closestCollider.tag != "Tilemap") // ���� �Ÿ� ���� ���� ������ ���� ����� ������ �̵�
            {
                if (hitColliders.Length > 0)
                {
                    //print("�� ã����");
                    //print(closestCollider);
                    Vector2 direction = (closestCollider.transform.position - transform.position).normalized;
                    Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg); // ȭ�� ���� ����
                    Vector2 pos2D = new Vector2(pos.position.x, pos.position.y);
                    pos.rotation = rotation;
                    pos2D += speed * Time.deltaTime * direction;
                    pos.position = new Vector3(pos2D.x, pos2D.y, pos.rotation.z);
                }
                
            }
            else
            {
                //print("�� ã����");
                pos.position += moveDirection * speed * Time.deltaTime; // ȭ�� ���� �̵�
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //ȭ���� �浹�� Ȯ���� ���� �� ���������
    {
        if (collision.tag == "Enemy")
        {
            if (isSkill == true && !hitDict.ContainsKey(collision)) // ��ų ��� ���̰�, �̹� ������ ������� ���� ��찡 �ƴ� ��
            {
                hitDict.Add(collision, true); // �� ������ Dictionary�� �߰�
            }
            else if (isMasterSkill == true)
            {
                StartCoroutine(MasterSkill());
            }
            else
            {
                DestroyArrow();
            }

            
        }
        if (collision.tag == "Wall" || collision.tag == "Tilemap") // ���̳� ���� ������ ȭ�� ����� �е�� ���°� ������ ���Ƽ� ����
        {
            DestroyArrow();
        }
    }

    IEnumerator MasterSkill()
    {
        Instantiate(BowMaster, pos.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        isMasterSkill = false;
        Destroy(BowMaster);
    }

    public void DestroyArrow()  // ȭ�� ���� �Լ�
    {
        Destroy(gameObject);
    }
}

