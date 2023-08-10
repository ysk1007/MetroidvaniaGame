using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class bloodEFF : MonoBehaviour
{
    Enemy enemy;
    SpriteRenderer SpriteRenderer;

    public int dir;
    public float scalX;
    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (dir == 1 && scalX == 1)
        {
            SpriteRenderer.flipX = true;
        }
        else if (dir == -1 && scalX == 1)
        {
            SpriteRenderer.flipX = false;
        }
        else if (dir == 1 && scalX == -1)
        {
            SpriteRenderer.flipX = false;
        }
        else if (dir == -1 && scalX == -1)
        {
            SpriteRenderer.flipX = true;
        }
        DestoryObject();
    }
    
    public void DestoryObject()
    {
        Destroy(gameObject, 1f);
    }
}
