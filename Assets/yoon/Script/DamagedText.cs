using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamagedText : MonoBehaviour
{
    public TextMeshProUGUI DamagedValue;
    public TMP_ColorGradient criColor;
    public TMP_ColorGradient NormalColor;
    public float speed = 10f;
    public float height = 5f;

    public Vector3 target;
    public Vector3 startPosition;

    public float tx;
    public float ty;
    public float tz;

    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vc = new Vector3(startPosition.x + tx, startPosition.x + ty, startPosition.x + tz);
        target = vc;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // ������ � ������ �̿��Ͽ� ��ġ ���
        float t = elapsedTime * speed;
        float x = Mathf.Lerp(startPosition.x, target.x, t);
        float y = startPosition.y + height * Mathf.Sin(t * Mathf.PI);
        float z = Mathf.Lerp(startPosition.z, target.z, t);

        // ������Ʈ �̵�
        transform.position = new Vector3(x, y, z);

        // ��ǥ ������ ������Ʈ ���� �� �߰� ó�� ����
        if (t >= 0.75f)
        {
            // ��ǥ ���� ó��
            Destroy(transform.parent.gameObject);
        }
    }
}
