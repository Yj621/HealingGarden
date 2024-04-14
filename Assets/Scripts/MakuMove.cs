using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakuMove : MonoBehaviour
{
    public GameObject cube1;    // 기준 1
    public GameObject cube2;    // 기준 2
    public float speed = 1f; // 속도
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // 시작할 때 두 큐브 사이의 거리를 계산
        journeyLength = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        startTime = Time.time;
    }

    void Update()
    {
        // Mathf.PingPong을 사용하여 움직임의 시간 값을 계산
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = Mathf.PingPong(distCovered, journeyLength) / journeyLength;

        // Vector3.Lerp를 사용하여 현재 오브젝트의 위치를 cube1과 cube2 사이에서 돌아다니게 함
        transform.position = Vector3.Lerp(cube1.transform.position, cube2.transform.position, fracJourney);
    }
}

// 나중에 이걸 머신러닝으로 만들어야겠,,,지,,,,, 후하하하하