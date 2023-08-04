using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Rock_Eff : MonoBehaviour
{
    Player player;
    Enemy enemy;
    Rigidbody2D rigid;

    public float speed = 5f; // ��ӵ� � �ʱ� �ӵ�
    public float angle = 45f; // �ʱ� �߻簢��
    public float gravity = -4.8f; // �߷� ��

    public int Dir;
    public float Time;
    public float Power;
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();

        Vector2 force = Vector2.right * speed; // �ʱ� ��ӵ� �

        // �߻簢���� ���� ���� ������ �����մϴ�.
        force = Quaternion.Euler(0, 0, angle) * force;

        rigid.AddForce(force, ForceMode2D.Impulse); // ���� �� ���� ���մϴ�.

        DestoryObject();
    }
    private void Update()
    {
        Throw();
    }
    public void DestoryObject()
    {
        Destroy(gameObject, Time);
    }
    public void Throw()
    {
        rigid.AddForce(transform.right*3f , ForceMode2D.Force);
        rigid.AddForce(Vector2.up * gravity, ForceMode2D.Force); // �߷� ���� ���մϴ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 vector2 = new Vector2(Dir, 1);
            Player.instance.GetComponent<Player>().Playerhurt(Power, vector2);
        }
    }

}
