using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MakuMove : MonoBehaviour
{
    public GameObject cube1;    // 거점 1
    public GameObject cube2;    // 거점 2
    public float speed = 1f;
    private float startTime;
    private float journeyLength;
    public GameObject makuSpot;

    private bool isEnter = false;
    void Start()
    {
        journeyLength = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        startTime = Time.time;
        isEnter = false;
    }

    void Update()
    {
        MoveStart();
        if (isEnter != false)
        {
            makuSpot.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void MoveStart ()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = Mathf.PingPong(distCovered, journeyLength) / journeyLength;

        Vector3 targetPosition = Vector3.Lerp(cube1.transform.position, cube2.transform.position, fracJourney);
        transform.position = targetPosition;

        // 목표를 계속 바라보게 하는 코드 수정 (Y축 회전만 적용)
        Vector3 direction = (targetPosition - transform.position).normalized;
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
    }
}