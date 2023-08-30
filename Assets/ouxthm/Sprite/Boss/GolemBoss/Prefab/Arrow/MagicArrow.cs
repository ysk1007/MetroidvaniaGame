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
    public Transform target; // 타겟 오브젝트 설정
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
            // x 방향으로 이동하는 벡터를 계산합니다.
            Vector3 moveVector = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            // 이동 벡터를 적용하여 게임 오브젝트를 x 방향으로 이동시킵니다.
            transform.Translate(moveVector);
        }

        if (target != null && !isShoot)
        {
            // 타겟 방향 벡터 계산
            Vector2 targetDirection = target.position - transform.position;

            // 타겟 방향으로 회전 각도 계산 (라디안)
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // 회전을 적용
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인합니다.
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
