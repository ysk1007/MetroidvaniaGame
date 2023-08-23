using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Player player;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        player = Player.instance.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("open", true);
            StartCoroutine(Delay());
            
        }
        else
        {
            anim.SetBool("open", false);
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("open", false);
        }
        else
            anim.SetBool("open", true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Delay());
            player.transform.position = new Vector3(0, 0, 0);
        }
    }

    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
