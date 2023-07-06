using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeCollider : MonoBehaviour
{
    BoxCollider2D boxcollider;

    public void ChangeCollider1()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0.5f);
        boxcollider.size = new Vector2(0.8f, 1.4f);
    }

    public void ChangeCollider2()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0.48f);
        boxcollider.size = new Vector2(1.1f, 0.8f);
    }

    public void ChangeCollider3()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0.4f);
        boxcollider.size = new Vector2(1.1f, 0.8f);
    }

    public void ChangeCollider4()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0.4f);
        boxcollider.size = new Vector2(0.8f, 1.3f);
    }

    public void ChangeCollider5()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0.05f);
        boxcollider.size = new Vector2(1, 1.25f);
    }

    public void ChangeColliderFin()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0, 0);
        boxcollider.size = new Vector2(1, 1.1f);
    }
}
