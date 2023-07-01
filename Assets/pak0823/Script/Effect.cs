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
        if (collision.tag == "Wall" || collision.tag == "Tilemap") // ���̳� ���� ������ ȭ�� ����� �е�� ���°� ������ ���Ƽ� ����
        {
            Desrtory();
        }
    }

    void Desrtory()
    {
        Destroy(this.gameObject);
    }
}
