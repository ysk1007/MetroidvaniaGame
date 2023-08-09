using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeCollider : MonoBehaviour
{
    BoxCollider2D boxcollider;

    public void originCollider()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.003f);
        boxcollider.size = new Vector2(1.7f, 1.4f);
    }

    public void ChangeCollider1()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.8f);
        boxcollider.size = new Vector2(1.7f, 2.3f);
    }

    public void ChangeCollider2()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.77f);
        boxcollider.size = new Vector2(1.7f, 2.3f);
    }

    public void ChangeCollider3()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.67f);
        boxcollider.size = new Vector2(1.7f, 1.35f);
    }

    public void ChangeCollider4()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.64f);
        boxcollider.size = new Vector2(1.7f, 2.1f);
    }

    public void ChangeCollider5()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.03f, 0.09f);
        boxcollider.size = new Vector2(1.7f, 2.04f);
    }

    public void ChangeColliderFin()
    {
        boxcollider = GetComponentInParent<BoxCollider2D>();

        boxcollider.offset = new Vector2(0.06f, -0.4f);
        boxcollider.size = new Vector2(2.6f, 1.05f);
    }
}
