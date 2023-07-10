using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective_Body : MonoBehaviour
{
    void Start()
    {
        DestoryObject();
    }

    public void DestoryObject()
    {
        Destroy(gameObject, 2f);
    }
}
