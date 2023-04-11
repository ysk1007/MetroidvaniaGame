using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{

    public float Enemy_HP;  // ���� ü��
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public float Enemy_Attack_Speed;    // ���� ���ݼӵ�
    public bool Enemy_Left; // ���� ����
    public bool Attacking;
    public bool Hit_Set;    // ���͸� ����� ����
    public float Gap_Distance;  // Fat_Boss�� Player ���� �Ÿ�.
    public float RushDistance;    // Fat_Boss�� ���� ��Ÿ�
    public float NextMove;  // ������ ���ڷ� ǥ��


    Rigidbody rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�

    public virtual void Short_Monster()
    {
        // �ִϸ��̼� �����ϸ� ��.
        //virtual�� ���� ������ ���� ��ũ��Ʈ���� ��Ӹ� ������ ���ư�
        Debug.Log("Short_Monster");
    }

    public virtual void Long_Monster()
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()
    {
        Debug.Log("Fly_Monster");
    }

}