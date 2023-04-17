using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider; // HP �����̴��� ���� ��ü
    public Slider BackHpSlider; // �ܻ� �����̴��� ���� ��ü
    public bool backHpHit = false; // �ܻ� ü���� �������� �ִ��� Ȯ��, �����ϴ� bool ��

    public float maxHp; //�ִ� ü��
    public float currentHp; //���� ü��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�����̴��� ����� [���� ü��/�ִ� ü��] Mathf.Lerp�� ���� ������� ��ǥ ������� �ε巴�� ����
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f); 

        if (backHpHit) //�ܻ� ü�� ��� ���� ����
        {
            //�ܻ�ü�� �����̴��� ����� ü�� �����̴��� �����ŭ �ε巴�� ����
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
        }
    }

    public void Dmg(float damage) //�������� �Դ� �Լ� (�ܺο��� �ҷ���)
    {
        backHpHit = false; //�ܻ��� ���ߵ��� ��
        currentHp -= damage; //���� ü�¿��� �����ŭ ����
        Invoke("BackUpFun", 1f); //1�ʵ� BackUpFun �Լ�
    }

    public void Heal(float value) //ȸ���ϴ� �Լ�
    { 
        if (currentHp + value >= maxHp) //ȸ�������� �ִ�ü���� �Ѵ°��� ����
        {
            currentHp = maxHp;
            return;
        }
        backHpHit = false; 
        currentHp += value; //���� ü�¿��� �����ŭ ����

        Invoke("BackUpFun", 1f);
    }

    void BackUpFun() //�ܻ� ü�� ��� ��
    {
        backHpHit = true;
    }

    public void SelfDestroy() //ü�¹� ���� (���� ����)
    {
        Destroy(gameObject);
    }
}
