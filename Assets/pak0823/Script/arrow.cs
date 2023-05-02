using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask islayer;
    public Transform pos;
    public bool Skill = false;


    GameObject Player;
    private void Start()
    {
        Invoke("DestroyArrow", 0.7f);
        Player = GameObject.Find("Player");
    }
    
    void Update()
    {
        Skill = Player.GetComponent<Player>().isSkill;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.right, distance, islayer);
        if(rayHit.collider != null)
        {
            Debug.Log(Skill);
            if (rayHit.collider.tag == "Enemy")
            {
                Debug.Log("Hit!");
                rayHit.collider.GetComponent<Enemy>().EnemyHurt(1, transform.position);
                if(Skill)
                {
                    Invoke("DestroyArrow", 0.7f);
                }
                else
                    DestroyArrow();
            }
            
        }
        if(transform.rotation.y == 0)
            transform.Translate(transform.right * speed * Time.deltaTime);
        else
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
    }

    void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
