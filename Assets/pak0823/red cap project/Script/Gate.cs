using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject key;
    Animator anim;
    MapManager mapManager;
    bool TouchPortal = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        mapManager = MapManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            anim.SetBool("open", true);
            TouchPortal = true;
            key.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            anim.SetBool("open", false);
            TouchPortal = false;
            key.SetActive(false);
        }
    }

    private void Update()
    {
        if (TouchPortal && Input.GetKeyDown(KeyCode.E))
        {
            TouchPortal = false;
            mapManager.StageMove = true;
        }
    }
}
