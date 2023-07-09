using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk_Collider : MonoBehaviour
{
    Enemy enemy;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();        
    }
    public void onCollider()
    {
        boxCollider.enabled = true;
    }
    public void offCollider()
    {
        boxCollider.enabled = false;
    }
}
