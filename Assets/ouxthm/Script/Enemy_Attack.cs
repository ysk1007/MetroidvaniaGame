using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class Enemy_Attack : MonoBehaviour
{
    public Transform posi;
    public BoxCollider2D Box;
    Enemy Enemy;
    private void Start()
    {
        posi = this.gameObject.GetComponent<Transform>();
        Box = this.gameObject.GetComponent<BoxCollider2D>();
    }
    public void GiveDamage()    // 플레이어에게 데미지를 주는 함수
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(posi.position, Box.size, 0);

        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
            {
                collider.GetComponent<Player>().Playerhurt(Enemy.Enemy_Power, Enemy.Pos.position);
                Debug.Log("데미지 줌");

            }
            Debug.Log("데미지 못 줌");
        }

    }
}
