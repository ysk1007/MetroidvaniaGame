using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTile: MonoBehaviour
{
    [SerializeField]
    private Transform background;           // ���� ���� �̾����� ���
    [SerializeField]
    private float scrollAmount;         // �÷��̾�� ��� ������ �Ÿ�
    [SerializeField]
    private Vector3 moveDirection;      // �̵� ����

    public Transform target;

    private void Update()
    {
        if (target != null)
        {
            // ����� ������ ������ ����� ��ġ �缳��
            if (target.transform.position.x >= transform.position.x + scrollAmount) // �÷��̾ ������ �� ��
            {
                transform.position = background.position + moveDirection * scrollAmount;
            }
        }
    }
}
