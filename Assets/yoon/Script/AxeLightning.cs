using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class AxeLightning : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D colider;
    public Enemy FindEnemys;
    public Collider2D[] Finds;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EffectOff", 0.9f);
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
                StartCoroutine(enemy.Hit(999f));
            }
        }
    }

}