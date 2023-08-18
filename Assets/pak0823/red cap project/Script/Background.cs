using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform target;           // ���� ���� �̾����� ���
    [SerializeField]
    private float scrollAmount;         // �̾����� �� ��� ������ �Ÿ�
    //[SerializeField]
    //private float moveSpeed;            // �̵� �ӵ�
    [SerializeField]
    private Vector3 moveDirection;      // �̵� ����

    private void Update()
    {
        // ����� moveDirection �������� moveSpeed�� �ӵ��� �̵�
        transform.position += moveDirection * Time.deltaTime;

        // ����� ������ ������ ����� ��ġ �缳��
        if(transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}
