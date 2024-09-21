using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject timerCam, Maku1, Maku2;
    public TextMesh countdownText;
    public float countdownTimer = 300f; // 카운트다운 시작 숫자

    Vector3 startScale;
    public float distance = 10f; // 카메라와 텍스트의 거리
    public bool isTimerRunning = false; // 타이머 실행 여부
    
    void Start()
    {
        startScale = transform.localScale;
    }


    public void TextUpdate()
    {
        // 타이머를 분:초 (MM:SS) 형식으로 변환
        int minutes = Mathf.FloorToInt(countdownTimer / 60);
        int seconds = Mathf.FloorToInt(countdownTimer % 60);
        // 소수점 둘째 자리까지 표시
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        float dist = Vector3.Distance(timerCam.transform.position, transform.position);
        Vector3 newScale = startScale * dist / distance;
        transform.localScale = newScale;

        transform.rotation = timerCam.transform.rotation;

        // 타이머가 실행 중일 때만 카운트다운
        if (isTimerRunning && countdownTimer > 0)
        {
            // Time.deltaTime은 프레임 간의 시간 차이를 초 단위로 나타내므로 1/1000초 단위로 감소
            countdownTimer -= Time.deltaTime;
            // 시작 숫자를 0보다 작게 만들지 않기 위한 조정
            if (countdownTimer < 0)
                countdownTimer = 0;
        }

        TextUpdate();

        if (countdownTimer == 0)
        {
            Debug.Log("Count End. Stopping Happiness script.");
            Maku1.SetActive(false);
            Maku2.SetActive(true);
        }
    }
}