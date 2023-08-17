using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //GameManager �ν��Ͻ��� ������ �� �ִ� Instance ������Ƽ�� ����
    //�ٸ� �Լ����� ���� ��� > GameManager mg = GameManager.GetInstance();
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) //�ν��Ͻ��� �̹� �����ϴ��� Ȯ��, �ڱ� �ڽ����� Ȯ��
        {
            Destroy(this.gameObject); //�����Ϸ��� �ν��Ͻ� �ı�
        }
        else
        {
            _instance = this; //�ƴϸ� �ν��Ͻ��� ���� GameManager ��ü�� ����
            /*DontDestroyOnLoad(this.gameObject); */
        }
    }

    // ���ӸŴ��� ��� ����
    public void gameover()
    {
        StartCoroutine(DataSave());
        Application.Quit();
    }

    IEnumerator DataSave()
    {
        DataManager.instance.JsonSave(default);
        yield return null;
    }
}
