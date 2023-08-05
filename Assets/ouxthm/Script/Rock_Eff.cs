using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;

public class Rock_Eff : MonoBehaviour
{
    Player player;
    Enemy enemy;
    Rigidbody2D rigid;

    //public float height = 50f;
    public float distance;

    public int Dir;
    public float Time;
    public float Power;

    
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.up * 25f, ForceMode2D.Impulse);
        rigid.AddForce(transform.right * distance, ForceMode2D.Impulse); // �̰� Upadate���� ������ ���� ��� �޾Ƽ� ���� ��� // Start�� �־� �� ���� ���� �ϰ� �ϴ� ó�� ���� ���� �ۿ��Ͽ� �������� �׸�
        DestoryObject();
    }

    public void DestoryObject()
    {
        Destroy(gameObject, Time);
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
