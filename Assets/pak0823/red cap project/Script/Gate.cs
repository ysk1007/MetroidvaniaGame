using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Player player;
    Animator anim;
    MapManager mapManager;
    bool TouchPortal = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        player = Player.instance;
        mapManager = MapManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            anim.SetBool("open", true);
            TouchPortal = true;
        }
        else
        {
            anim.SetBool("open", false);
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            anim.SetBool("open", false);
            TouchPortal = false;
        }
        else
            anim.SetBool("open", true);
    }

    private void Update()
    {
        if (TouchPortal && Input.GetKeyDown(KeyCode.E))
        {
            TouchPortal = false;
            StartCoroutine(Delay());
        }
    }


    IEnumerator Delay()
    {
        player.isdelay = true;
        yield return new WaitForSeconds(2f);
        mapManager.StageMove = true;
        player.transform.position = new Vector3(0, 0, 0);
        player.isdelay = false;
    }
}
