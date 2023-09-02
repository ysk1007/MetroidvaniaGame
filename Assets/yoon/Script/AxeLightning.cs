using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;

public class AxeLightning : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D colider;
    public Enemy FindEnemys;
    public Collider2D[] Finds;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EffectOff", 3f);
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
    }

    void Search()
    {
        colider = this.gameObject.transform.GetComponent<BoxCollider2D>();

        Finds = Physics2D.OverlapBoxAll(transform.position, colider.size, 0);
        // ã�� �ݶ��̴����� �±׸� ����մϴ�.
        foreach (Collider2D collider in Finds)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                if (enemy.axeUltHit)
                {
                    return;
                }
                enemy.axeUltHit = true;
                Debug.Log("����?");
                if (enemy.AmIBoss == true)
                {
                    StartCoroutine(enemy.Hit(1000f));
                }
                else
                {
                    StartCoroutine(enemy.Hit(999f));
                }
            }
        }

        colider.enabled = false;

    }

}
