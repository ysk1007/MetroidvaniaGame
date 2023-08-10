using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEFF : MonoBehaviour
{
    Enemy enemy;
    SpriteRenderer spriteRenderer;
    public float scalX;
    public int dir;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {
        if(dir == 1 && scalX == 1)
        {
            spriteRenderer.flipX = true;
        }
        else if(dir == -1 && scalX == 1)
        {
            spriteRenderer.flipX = false;
        }
        else if(dir == 1 && scalX == -1)
        {
            spriteRenderer.flipX = false;
        }
        else if(dir == -1 && scalX == -1)
        {
            spriteRenderer.flipX = true;
        }
        DestoryObject();
    }

    public void DestoryObject()
    {
        Destroy(gameObject, 0.5f);
    }
}
