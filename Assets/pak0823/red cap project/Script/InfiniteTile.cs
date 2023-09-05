using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTile: MonoBehaviour
{
    [SerializeField]
    private Transform background;           // 현재 배경과 이어지는 배경
    [SerializeField]
    private float scrollAmount;         // 플레이어와 배경 사이의 거리
    [SerializeField]
    private Vector3 moveDirection;      // 이동 방향

    public Transform target;

    private void Update()
    {
        if (target != null)
        {
            // 배경이 설정된 범위를 벗어나면 위치 재설정
            if (target.transform.position.x >= transform.position.x + scrollAmount) // 플레이어가 앞으로 갈 때
            {
                transform.position = background.position + moveDirection * scrollAmount;
            }
        }
    }
}
