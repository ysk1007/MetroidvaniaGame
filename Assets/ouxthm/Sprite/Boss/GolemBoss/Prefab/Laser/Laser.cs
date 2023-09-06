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
        // 위로 이동하는 벡터 계산
        Vector3 moveVector = Vector3.right * Dir * moveSpeed * Time.deltaTime;

        // 이동 벡터를 적용하여 오브젝트를 위로 이동
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
