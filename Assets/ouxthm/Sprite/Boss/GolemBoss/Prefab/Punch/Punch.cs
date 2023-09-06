using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public float Damage = 50f;

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {

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
