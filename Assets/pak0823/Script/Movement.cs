using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //public float offset;
    private Vector3 origin; //시작 위치값
    public Transform player; //player의 위치값을 저장할 변수
    public float parallaxFactor = 0.1f; //원근값
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
