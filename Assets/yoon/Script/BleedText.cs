using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BleedText : MonoBehaviour
{
    public TextMeshProUGUI DamagedValue;

    public float speed = 1f;
    public float height = 1f;

    public Vector3 target;
    public Vector3 startPosition;

    public float sx;
    public float sy;

    private float elapsedTime = 0f;

    void Start()
    {
        startPosition = new Vector3(startPosition.x + sx, startPosition.y + sy + 0.5f, startPosition.z);
        target = new Vector3(startPosition.x, startPosition.y - height, startPosition.z);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // ������ � ������ �̿��Ͽ� ��ġ ���
        float t = elapsedTime * speed;
        float x = startPosition.x;
        float y = Mathf.Lerp(startPosition.y, target.y, t);
        float z = startPosition.z;

        // ������Ʈ �̵�
        transform.position = new Vector3(x, y, z);

        // ��ǥ ������ ������Ʈ ���� �� �߰� ó�� ����
        if (t >= 1f)
        {
            // ��ǥ ���� ó��
            Destroy(transform.parent.gameObject);
        }
    }
}

