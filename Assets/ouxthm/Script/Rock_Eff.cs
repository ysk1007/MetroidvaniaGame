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

    public float speed = 5f; // 등속도 운동 초기 속도
    public float angle = 45f; // 초기 발사각도
    public float gravity = -4.8f; // 중력 값

    public int Dir;
    public float Time;
    public float Power;
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();

        Vector2 force = Vector2.right * speed; // 초기 등속도 운동

        // 발사각도에 따라 힘의 방향을 조절합니다.
        force = Quaternion.Euler(0, 0, angle) * force;

        rigid.AddForce(force, ForceMode2D.Impulse); // 시작 시 힘을 가합니다.

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
        rigid.AddForce(Vector2.up * gravity, ForceMode2D.Force); // 중력 힘을 가합니다.
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
