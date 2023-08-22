using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tilemap"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
