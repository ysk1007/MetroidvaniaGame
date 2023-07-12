using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTree : MonoBehaviour
{
    public float deleteTime = 5f;
    public float Attackdelay = 0;
    public float Dmg = 5;
    public float speed = 1f;
    public Enemy enemy;
    public GameObject Tree;  // 숙련도 오브젝트

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            if(Attackdelay <= 0)
            {
                print("맞았음");
                StartCoroutine(enemy.Hit(Dmg));
                Attackdelay = 1f;
            }
            else
            {
                if (Attackdelay >= 0)
                    Attackdelay -= Time.deltaTime;
            }
                
        }
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
