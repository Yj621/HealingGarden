using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakuMove : MonoBehaviour
{
    public GameObject cube1;    // ���� 1
    public GameObject cube2;    // ���� 2
    public float speed = 1f; // �ӵ�
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // ������ �� �� ť�� ������ �Ÿ��� ���
        journeyLength = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        startTime = Time.time;
    }

    void Update()
    {
        // Mathf.PingPong�� ����Ͽ� �������� �ð� ���� ���
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = Mathf.PingPong(distCovered, journeyLength) / journeyLength;

        // Vector3.Lerp�� ����Ͽ� ���� ������Ʈ�� ��ġ�� cube1�� cube2 ���̿��� ���ƴٴϰ� ��
        transform.position = Vector3.Lerp(cube1.transform.position, cube2.transform.position, fracJourney);
    }
}

// ���߿� �̰� �ӽŷ������� �����߰�,,,��,,,,, ����������