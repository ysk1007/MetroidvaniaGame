using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public bool ItemPick = false;
    public GameObject Description_screen;
    public Description_Setting Description;
    public itemStatus item;

    public float moveSpeed = 1.0f; // 오브젝트 이동 속도
    public float stopHeight = 1.8f; // 멈출 높이

    public bool isMoving = false;
    private Vector3 initialPosition;
    public Animator ItemAnim;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        GameObject Boxitem = DataManager.instance.ChestItem();
        GameObject newChild = Instantiate(Boxitem, gameObject.transform.position, Quaternion.identity);
        newChild.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        newChild.transform.SetParent(gameObject.transform.GetChild(0));
        item = newChild.GetComponent<itemStatus>();
        item.InitSetting();
        Description.DropSetting(item.data.itemimg,item.data.itemName,item.data.itemStat,item.data.itemExplanation,item.data.itemPrice,item.data.color,item.data.Rating);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && ItemPick == true)
        {
            if (item.data.itemNameEng == "HpPotion")
            {
                GameManager.Instance.GetComponent<Ui_Controller>().Heal(Player.instance.GetComponent<Player>().MaxHp / 2);
                Destroy(gameObject);
            }
            else if (GameManager.Instance.GetComponent<inven>().PickUpItem(item))
            {
                DataManager.instance.UnlockListUpdate(item.data.itemNumber);
                Destroy(gameObject);
            }
        }

        if (isMoving)
        {
            // 오브젝트를 위로 이동시킵니다.
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // 멈출 높이에 도달하면 이동을 멈춥니다.
            if (transform.position.y >= initialPosition.y + stopHeight)
            {
                isMoving = false;
                ItemAnim.SetTrigger("Hit");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            ItemPick = true;
            Description_screen.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            ItemPick = false;
            Description_screen.SetActive(false);
        }
    }
}
