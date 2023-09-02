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
    public bool goldChest;
    public GameObject coin;
    public GameObject potion;
    public float forceStrength = 10f;
    public int dir = 1;
    public Transform pos;
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
            if (!goldChest)
            {
                Invoke("ItemAnim", time);
            }
            else
            {
                float time = 0f;
                CreatePotionWithForce();
                for (int i = 0; i < 40; i++)
                {
                    Invoke("CreateObjectWithForce", time);
                    time += 0.05f;
                }
            }
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

    private void CreateObjectWithForce()
    {
        // 게임 오브젝트 생성
        GameObject go = Instantiate(coin, pos.position, Quaternion.identity);

        // Rigidbody2D 컴포넌트 가져오기
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        float rannum = Random.Range(0.5f, 3f);
        if (rb != null)
        {
            // 위 방향으로 힘을 추가
            rb.AddForce(Vector2.up * forceStrength, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * dir * rannum, ForceMode2D.Impulse);
        }
        forceStrength = Random.Range(5f, 10f);

        if (dir > 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }

    private void CreatePotionWithForce()
    {
        // 게임 오브젝트 생성
        GameObject go = Instantiate(potion, pos.position, Quaternion.identity);

        // Rigidbody2D 컴포넌트 가져오기
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 위 방향으로 힘을 추가
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}
