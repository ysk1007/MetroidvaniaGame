using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    bool isHurt = false;
    Animator anim;
    public bool isknockback = false;
    public int Speed = 5;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void EnemyHurt(int damage, Vector2 pos) //Enemy_OnDamaged 변수
    {
        if (!isHurt)
        {
            hp = hp - damage;
            if (hp <= 0)
            {
                anim.SetTrigger("Die");
                StartCoroutine(Knockback());
                Invoke("Die", 3f);
            }
            else
            {
                anim.SetTrigger("hurt");
                
                float x = transform.position.x - pos.x;
                if (x < 0)
                    x = 1;
                else
                    x = -1;
            }
        }
    }

    IEnumerator Knockback() //피해입을시 넉백
    {
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.2f) //넉백 지속시간
        {
                transform.Translate(Vector2.left * Speed * 2 * Time.deltaTime);
           
            ctime += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = new Vector3(0,0,90);
        isknockback = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
