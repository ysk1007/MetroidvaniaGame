using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform background;           // ���� ���� �̾����� ���
    [SerializeField]
    private float scrollAmount;         // �÷��̾�� ��� ������ �Ÿ�
    [SerializeField]
    private Vector3 moveDirection;      // �̵� ����

    public Transform target;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        // ����� ������ ������ ����� ��ġ �缳��
        if(target.transform.position.x >= transform.position.x + scrollAmount) // �÷��̾ ������ �� ��
        {
            transform.position = background.position + moveDirection * scrollAmount;
        }

        if ((target.transform.position.x + scrollAmount) <= transform.position.x) // �÷��̾ �ڷ� �� ��
        {
            transform.position = background.position + moveDirection * (-1) * scrollAmount;
        }
    }
}
