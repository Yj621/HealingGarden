using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakuMove : MonoBehaviour
{
    public GameObject cube1;    // 거점 1
    public GameObject cube2;    // 거점 2
    public GameObject cube3;    // 거점 3
    public float speed = 1f;
    private float startTime;
    private float journeyLength;
    public GameObject makuSpot;

    private bool isEnter = false;
    private bool isHomeEntered = false; // Home에 진입했는지 여부

    void Start()
    {
        // 처음에는 cube1과 cube2 사이 거리로 설정
        journeyLength = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        startTime = Time.time;
        isEnter = false;
    }

    void Update()
    {
        MoveStart();
        if (isEnter)
        {
            makuSpot.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void MoveStart()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = Mathf.PingPong(distCovered, journeyLength) / journeyLength;

        // Home에 진입했으면 cube1과 cube3을 배회, 그렇지 않으면 cube2와 cube3를 배회
        if (isHomeEntered)
        {
            Vector3 targetPosition = Vector3.Lerp(cube1.transform.position, cube3.transform.position, fracJourney);
            transform.position = targetPosition;
        }
        else
        {
            Vector3 targetPosition = Vector3.Lerp(cube2.transform.position, cube3.transform.position, fracJourney);
            transform.position = targetPosition;
        }

        // 목표를 계속 바라보게 하는 코드 수정 (Y축 회전만 적용)
        Vector3 direction = (transform.position - cube1.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = targetRotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WarpZone1"))
        {
            isEnter = true;
            Debug.Log("WarpZone에 진입했습니다!"); // 디버그 메시지 출력
        }
        else if (other.CompareTag("Home"))
        {
            Debug.Log("집에 도착했습니다!"); // 디버그 메시지 출력

            // Home에 진입했으면 cube1과 cube3 사이 거리로 journeyLength 갱신
            journeyLength = Vector3.Distance(cube1.transform.position, cube3.transform.position);
            isHomeEntered = true; // Home에 진입했음을 표시
            startTime = Time.time; // 이동 시작 시간을 다시 초기화
        }
    }
}
