using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HolyArrow : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public GameObject HolyEffect;    // ���� ȭ���� �¾��� �� �߻��ϴ� �ǰ� ����Ʈ
    public LayerMask islayer; // �浹 ������ �� ���̾�
    public Transform pos; // ȭ�� ��ġ ����
    public float speed = 20f; // ȭ�� �̵� �ӵ�
    private Vector3 moveDirection = Vector3.right; // ȭ���� ������ ����

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = Player.instance.GetComponent<Player>();
        Invoke("DestroyArrow", 1.5f); // ���� �ð��� ���� �� ȭ���� �����ϴ� Invoke �Լ��� ȣ��
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
        pos.position += moveDirection * speed * Time.deltaTime; // ȭ�� ���� �̵�
    }

    private void OnTriggerEnter2D(Collider2D collision) //ȭ���� �浹�� Ȯ���� ���� �� ���������
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                Vector3 newpos = enemy.Pos.position;
                Instantiate(HolyEffect, newpos, transform.rotation);
                DestroyArrow();
            }
        }
    }

    public void DestroyArrow()  // ȭ�� ���� �Լ�
    {
        Destroy(gameObject);
    }
}


