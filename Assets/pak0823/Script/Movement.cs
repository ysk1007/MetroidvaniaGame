using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //public float offset;
    private Vector3 origin; //���� ��ġ��
    public Transform player; //player�� ��ġ���� ������ ����
    public float parallaxFactor = 0.1f; //���ٰ�
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        origin = transform.position;
    }

    void Update()
    {
        float distance = player.position.x - transform.position.x;
        Vector3 newPosition = origin + Vector3.right * distance * parallaxFactor;
        transform.position = newPosition;
    }
}
