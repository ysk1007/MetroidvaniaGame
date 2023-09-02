using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public LayerMask islayer; // �浹 ������ �� ���̾�
    public Transform pos; // ȭ�� ��ġ ����
    public float Dmg = 1; //����� ����, ���Ͱ� �ǰݽ� ȭ�� ���������� �ޱ� ����
    public float SkillDmg = 2;
    public float speed = 20f; // ȭ�� �̵� �ӵ�
    public bool isSkill = false; // ��ų ��� ����
    public BoxCollider2D box;   // ȭ�� �ڽ� �ݶ��̴�
    private Vector3 moveDirection = Vector3.right; // ȭ���� ������ ����
    private float detectRadius = 2.5f; // ȭ���� ������ �� �ִ� �ݰ� (���� �ִ��� ������ Ȯ��)
    public SpriteRenderer spriteRenderer;
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // �̹� ������ ������� �������� ���θ� ����ϴ� Dictionary ����

    private void Awake()
    {
        player = Player.instance.GetComponent<Player>();
        Dmg = (player.AtkPower + player.GridPower) * Dmg;
        SkillDmg =  (10 + player.AtkPower + player.GridPower) * SkillDmg;
        if (player.isSkill == true)
        {
            isSkill = true; // ��ų ��� ���̸� SetSkill ������ true�� ����
        }
        else
        {
            isSkill = false; // ��ų ��� ���� �ƴϸ� SetSkill ������ false�� ����
        }
    }

    private void Start()
    {
        Invoke("DestroyArrow", player.ArrowDistance); // ���� �ð��� ���� �� ȭ���� �����ϴ� Invoke �Լ��� ȣ��
        pos = transform;
        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // �÷��̾ �������� �ٶ󺸸� ȭ���� ���������� �߻�
                moveDirection = Vector3.right;
                spriteRenderer.flipX = false;
                if(isSkill)
                    box.offset = new Vector2(0.8f, 0);
                else
                    box.offset = new Vector2(0.4f, 0);
            }
            else
            {
                // �÷��̾ ������ �ٶ󺸸� ȭ���� �������� �߻�
                moveDirection = Vector3.left;
                spriteRenderer.flipX = true;
                if (isSkill)
                    box.offset = new Vector2(-0.8f, 0);
                else
                    box.offset = new Vector2(-0.4f, 0);
            }
        }
    }


    Collider2D FindCollider(Collider2D[] colliders)// ���� ����� �� ã��
    {
        Collider2D closestCollider = null;
        float distance = float.MaxValue;
        foreach (Collider2D coll in colliders)
        {
            if (LayerMask.LayerToName(coll.gameObject.layer) != "Pad" && LayerMask.LayerToName(coll.gameObject.layer) != "Tilemap")
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

        if (isSkill == true) // ��ų�� ��
        {
            pos.position += moveDirection * speed * Time.deltaTime; // ȭ�� ���� �̵�
        }
        else
        {
            Dmg = (player.ATP + player.AtkPower + player.GridPower) / 2;
            if (closestCollider != null && closestCollider.tag != "Wall" && closestCollider.tag != "Pad" && closestCollider.tag != "Tilemap") // ���� �Ÿ� ���� ���� ������ ���� ����� ������ �̵�
            {
                if (hitColliders.Length > 0 && player.proLevel >= 1)
                {
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
            else
            {
                DestroyArrow();
            }
        }
        if (collision.tag == "Wall" || collision.tag == "Tilemap") // ���̳� ���� ������ ȭ�� ����� �е�� ���°� ������ ���Ƽ� ����
        {
            if (isSkill)
                return;
            DestroyArrow();
        }
    }

    public void DestroyArrow()  // ȭ�� ���� �Լ�
    {
        Destroy(gameObject);
    }
}


