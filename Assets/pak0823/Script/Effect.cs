using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime;
    public Enemy enemy;
    public float Dmg =10;
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            StartCoroutine(enemy.Hit(Dmg));
            Desrtory();
        }
        if (collision.tag == "Wall" || collision.tag == "Tilemap") // 벽이나 땅에 맞으면 화살 사라짐 패드는 없는게 나은것 같아서 뺐음
        {
            Desrtory();
        }
    }

    void Desrtory()
    {
        Destroy(this.gameObject);
    }
}
