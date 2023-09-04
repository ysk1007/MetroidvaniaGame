using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static Gate instance;
    public GameObject key;
    Animator anim;
    MapManager mapManager;
    bool TouchPortal = false;
    public bool goingNextStage = false; // 2023-09-04 �߰�    ���� �������� �� �� true
    public bool boolLock = true;        // 2023-09-04 �߰�    ���� bool ���� ��״� ����
    private void Awake()
    {
        instance = this; 
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

    void Update()
    {
        if (TouchPortal && Input.GetKeyDown(KeyCode.E))
        {
            TouchPortal = false;
            mapManager.StageMove = true;
            goingNextStage = true;
            Debug.Log(goingNextStage + "����Ʈ��ũ��Ʈ");
            boolLock = false;

            if (goingNextStage == true && boolLock == false)
            {
                Invoke("OffGoingNextStage", 2.5f);
            }
        }
    }
    void OffGoingNextStage()
    {
        goingNextStage = false;
        boolLock = true;
    }
}
