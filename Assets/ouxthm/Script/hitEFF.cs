using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEFF : MonoBehaviour
{
    void Start()
    {
        DestoryObject();
    }

    public void DestoryObject()
    {
        Destroy(gameObject, 0.5f);
    }
}
