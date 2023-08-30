using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float Damage = 20f;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        // x �������� �̵��ϴ� ���͸� ����մϴ�.
        Vector3 moveVector = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        // �̵� ���͸� �����Ͽ� ���� ������Ʈ�� x �������� �̵���ŵ�ϴ�.
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
