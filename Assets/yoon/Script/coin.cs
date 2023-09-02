using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coin : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem ps;
    public AudioSource sfx;
    public BoxCollider2D Collider;

    public bool pt;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ColliderOn", 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tilemap") || other.CompareTag("Pad"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            if (pt)
            {
                ps.Play(true);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                GameManager.Instance.GetComponent<Ui_Controller>().Heal(50f);
                Invoke("det", 1f);
            }
            else
            {
                ps.Play(true);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                sfx.Play();
                Invoke("det", 1f);
                GameManager.Instance.GetComponent<Ui_Controller>().GetGold(25f);
            }
        }
    }

    void det()
    {
        Destroy(gameObject);
    }

    void ColliderOn()
    {
        Collider.enabled = true;
    }
}
