using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SoulEff : MonoBehaviour
{
    Player player;
    Enemy enemy;

    public int Dir;
    public float Time;
    public float Power;
    void Start()
    {
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
