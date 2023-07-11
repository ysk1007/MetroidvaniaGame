using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class Projective_Body : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Player player;
    public int Dir;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();

        sprite = this.GetComponent<SpriteRenderer>();

        DestoryObject();
    }
    private void Update()
    {
        Shot();
    }
    public void DestoryObject()
    {
        Destroy(gameObject, 1f);
    }
    public void Shot()
    { 
        if (Dir == 1)
        {
            rigid.AddForce(transform.right * 0.05f, ForceMode2D.Impulse);
            sprite.flipX = false;
        }
        else if (Dir == -1)
        {
            rigid.AddForce(transform.right * -0.05f, ForceMode2D.Impulse);
            sprite.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall"){
            Destroy(gameObject);
        }
    }
}
