using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject timerCam;
    public TextMesh countdownText;
    public float startNumber = 10; // 카운트다운 시작 숫자
    public float countdownInterval = 1.0f; // 카운트다운 간격 (초)


    Vector3 startScale;
    public float distance = 10; //카메라와 text의 거리

    void Start()
    {
        startScale = transform.localScale; 

        
    }
    
    public void OnClick()
    {
        StartCoroutine(CountdownRoutine()); // 카운트다운 코루틴 시작(이 코드를 마쿠가 온천에 들어오면 코드 옮기기)
    }

    void Update()
    {
        float dist = Vector3.Distance(timerCam.transform.position, transform.position);
        Vector3 newScale = startScale * dist / distance;
        transform.localScale = newScale;

        transform.rotation = timerCam.transform.rotation;
    }

    private IEnumerator CountdownRoutine()
    {
        float currentNumber = startNumber;
        
        while (currentNumber >= 0)
        {
            countdownText.text = currentNumber.ToString("F1"); // 현재 숫자를 텍스트로 표시
            yield return new WaitForSeconds(countdownInterval); // 지정된 시간만큼 대기
            currentNumber--; // 숫자 감소
        }

        OnCountdownFinished(); // 카운트다운이 끝났을 때 실행할 메서드 호출
    }

    private void OnCountdownFinished()
    {
        // 카운트다운이 끝났을 때 수행할 작업
        Debug.Log("Countdown finished!");
    }
}
