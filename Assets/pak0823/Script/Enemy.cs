using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    bool isHurt = false;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void EnemyHurt(int damage, Vector2 pos) //Enemy_OnDamaged º¯¼ö
    {
        if (!isHurt)
        {
            //isHurt = true;
            anim.SetTrigger("hurt");
            hp = hp - damage;
            if (hp <= 0)
            {
                hp += 3;
            }
            else
            {
                float x = transform.position.x - pos.x;
                if (x < 0)
                    x = 1;
                else
                    x = -1;
            }
        }
    }
}
