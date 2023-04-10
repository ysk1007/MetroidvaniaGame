using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask islayer;

    private void Start()
    {
        Invoke("DestroyArrow", 0.7f);
    }


    void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.right, distance, islayer);
        if(rayHit.collider != null)
        {
            if(rayHit.collider.tag == "Enemy")
            {
                Debug.Log("Hit!");
            }
            DestroyArrow();
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
