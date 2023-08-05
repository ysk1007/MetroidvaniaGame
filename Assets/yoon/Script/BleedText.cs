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

        // 포물선 운동 공식을 이용하여 위치 계산
        float t = elapsedTime * speed;
        float x = startPosition.x;
        float y = Mathf.Lerp(startPosition.y, target.y, t);
        float z = startPosition.z;

        // 오브젝트 이동
        transform.position = new Vector3(x, y, z);

        // 목표 도착시 오브젝트 제거 등 추가 처리 가능
        if (t >= 1f)
        {
            // 목표 도착 처리
            Destroy(transform.parent.gameObject);
        }
    }
}

