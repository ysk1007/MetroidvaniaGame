using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;

public class CaninePb : MonoBehaviour
{
    Player player;
    Enemy enemy;
    Rigidbody2D rigid;


    public int Dir;
    public float Time;
    public float Power;
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right * 20f, ForceMode2D.Impulse);
        DestoryObject();
        Invoke("reRotation", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 vector2 = new Vector2(Dir, 1);
            Player.instance.GetComponent<Player>().Playerhurt(Power, vector2);
        }
        if (collision.tag != null)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

    }

    public void DestoryObject()
    {
        Destroy(gameObject, Time);
    }

    void reRotation()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
