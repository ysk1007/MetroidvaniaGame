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
        // x 방향으로 이동하는 벡터를 계산합니다.
        Vector3 moveVector = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        // 이동 벡터를 적용하여 게임 오브젝트를 x 방향으로 이동시킵니다.
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인합니다.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Playerhurt(Damage, collision.transform.position);
        }
    }
}
