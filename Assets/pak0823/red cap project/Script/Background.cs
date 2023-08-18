using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform target;           // 현재 배경과 이어지는 배경
    [SerializeField]
    private float scrollAmount;         // 이어지는 두 배경 사이의 거리
    //[SerializeField]
    //private float moveSpeed;            // 이동 속도
    [SerializeField]
    private Vector3 moveDirection;      // 이동 방향

    private void Update()
    {
        // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
        transform.position += moveDirection * Time.deltaTime;

        // 배경이 설정된 범위를 벗어나면 위치 재설정
        if(transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}
