using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyHit : MonoBehaviour
{
    public Animator anim;
    public CircleCollider2D colider;
    public Enemy FindEnemys;
    public Collider2D[] Finds;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetTrigger("Hit");
        Invoke("EffectOff",1.749f);
        Hit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EffectOff()
    {
        Destroy(gameObject);
    }

    void Hit()
    {
        Search();
        Invoke("Hit", 0.6f);
    }

    void Search()
    {
        // 원 내부에 있는 모든 콜라이더들을 찾습니다.
        Finds = Physics2D.OverlapCircleAll(colider.transform.position, colider.radius);
        // 찾은 콜라이더들의 태그를 출력합니다.
        foreach (Collider2D collider in Finds)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                StartCoroutine(enemy.Hit(20f));
            }
        }
    }
}
