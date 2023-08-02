using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class bloodEFF : MonoBehaviour
{
    Enemy enemy;
    SpriteRenderer SpriteRenderer;

    public int dir;

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (dir == 1)
        {
            SpriteRenderer.flipX = true;
        }
        else if(dir == -1)
        {
            SpriteRenderer.flipX = false;
        }
        DestoryObject();
    }
    
    public void DestoryObject()
    {
        Destroy(gameObject, 1f);
    }
}
