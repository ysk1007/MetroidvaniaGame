using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicArrow : MonoBehaviour
{
    public Enemy boss;
    public float moveSpeed = 10f;
    public float Damage = 30f;
    public bool isShoot;
    public Transform target; // Ÿ�� ������Ʈ ����
    public Player player;
    public float rotationSpeed = 5f;
    public SpriteRenderer sprite;
    public AudioSource sfx;
    private void Start()
    {
        player = Player.instance;
        Invoke("Shoot", 2f);
        Destroy(gameObject, 5.5f);
    }

    private void Update()
    {
        target = player.transform;
        if (isShoot)
        {
            // x �������� �̵��ϴ� ���͸� ����մϴ�.
            Vector3 moveVector = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            // �̵� ���͸� �����Ͽ� ���� ������Ʈ�� x �������� �̵���ŵ�ϴ�.
            transform.Translate(moveVector);
        }

        if (target != null && !isShoot)
        {
            // Ÿ�� ���� ���� ���
            Vector2 targetDirection = target.position - transform.position;

            // Ÿ�� �������� ȸ�� ���� ��� (����)
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // ȸ���� ����
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ���մϴ�.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Playerhurt(Damage, collision.transform.position);
        }
    }

    public void Shoot()
    {
        isShoot = true;
        sprite.enabled = false;
        sfx.Play();
    }
}
