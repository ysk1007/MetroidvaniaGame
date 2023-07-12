using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime=2f;
    public float Dmg = 10;
    public float speed = 15f;
    public bool isMasterSkill = false;  //�÷��̾� �����ͽ�ų ��� ���� Ȯ��
    public int TreeCnt; //���� ���� ��ȣ ����
    private Vector3 Direction = Vector3.right;  //ȭ���� ������ ����

    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public Transform pos;
    public SpriteRenderer spriteRenderer;
    public Vector3 dir;
    public GameObject BowTree;  // ���õ� ������Ʈ
    public GameObject BowTree2;  // ���õ� ������Ʈ
    public GameObject BowTree3;  // ���õ� ������Ʈ
    public GameObject BowTree4;  // ���õ� ������Ʈ

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player ��ũ��Ʈ�� ���� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
        pos = transform;

        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // �÷��̾ �������� �ٶ󺸸� ���������� �߻�
                Direction = Vector3.right;
                spriteRenderer.flipX = false;
            }
            else
            {
                // �÷��̾ ������ �ٶ󺸸� �������� �߻�
                Direction = Vector3.left;
                spriteRenderer.flipX = true;
            }

            if (player.isMasterSkill == true)
            {
                isMasterSkill = true; // �����ͽ�ų ������̸� true�� ����
            }
            else
            {
                isMasterSkill = false; // �����ͽ�ų ��� ���� �ƴϸ� isMasterSkill ������ false�� ����

            }
        }
    }

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();

        pos.position += Direction * speed * Time.deltaTime; // ���� �̵�
        TreeCnt = Random.Range(1, 5);
        print(TreeCnt);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (isMasterSkill)
                {
                    isMasterSkill = false;
                    StartCoroutine(MasterSkill());
                }
                Desrtory();
            }
        }
        if (collision.tag == "Wall")
        {
            Desrtory();
        }
    }

    IEnumerator MasterSkill()
    {
        if (TreeCnt == 1)
            Instantiate(BowTree, pos.position, transform.rotation);
        if (TreeCnt == 2)
            Instantiate(BowTree2, pos.position, transform.rotation);
        if (TreeCnt == 3)
            Instantiate(BowTree3, pos.position, transform.rotation);
        if (TreeCnt == 4)
            Instantiate(BowTree4, pos.position, transform.rotation);
        
        yield return null;
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
