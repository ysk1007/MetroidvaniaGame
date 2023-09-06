using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Damage = 30f;
    public float moveSpeed = 2f;
    public int Dir = 1;

    private void Start()
    {
        Destroy(gameObject, 10.3f);
    }

    private void Update()
    {
        // ���� �̵��ϴ� ���� ���
        Vector3 moveVector = Vector3.right * Dir * moveSpeed * Time.deltaTime;

        // �̵� ���͸� �����Ͽ� ������Ʈ�� ���� �̵�
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ���մϴ�.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Playerhurt(Damage, collision.transform.position);
        }
    }
}
