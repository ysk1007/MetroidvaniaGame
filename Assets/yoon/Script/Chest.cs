using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject KeyUi;
    public bool ChestTouch = false;
    public bool ChestOpen = false;
    public Animator OpenAnimator;
    public GameObject Item;
    public float time;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && ChestTouch == true && !ChestOpen)
        {
            ChestOpen = true;
            OpenAnimator.SetTrigger("ChestOpen");
            sfx.Play();
            KeyUi.SetActive(false);
            Invoke("ItemAnim", time);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ChestOpen)
        {
            if (other.CompareTag("Player") || other.CompareTag("Sliding"))
            {
                ChestTouch = true;
                KeyUi.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!ChestOpen)
        {
            if (other.CompareTag("Player") || other.CompareTag("Sliding") && !ChestOpen)
            {
                ChestTouch = false;
                KeyUi.SetActive(false);
            }
        }
    }

    public void ItemAnim()
    {
        Item.GetComponent<DropItem>().isMoving = true;
        Item.SetActive(true);
    }
}
