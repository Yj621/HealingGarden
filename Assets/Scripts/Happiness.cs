using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour
{
    public Slider happinessSlider;

    //  public float spotRadius = 1.0f;
    public float incressDuration = 10f; //슬라이더가 최대치까지 증가하는데 걸리는 시간

    private bool isIncreasing = false; //슬라이더 증가 상태
    private float startTime; //슬라이더 증가가 시작된 시간

    public GameObject starPrefab;

    // 타이머
    public float Timer = 300f;
    private bool timerIsRunning = false;

    Timer timer;

    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        timerIsRunning = true;
        timer.isTimerRunning = true;
        happinessSlider.value = 0;
        isIncreasing = true;
        startTime = Time.time;
    }

    void Update()
    {
        if (timerIsRunning) // 타이머
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }    
            else
            {
                Timer = 0;
                Debug.Log("Time is Done");
            }
        }

        if(isIncreasing)
        {
            float time = Time.time - startTime;
            //Debug.Log("경과 시간: " + time + "초");
            if (time < incressDuration)
            {
                
                happinessSlider.value = Mathf.Lerp(0, happinessSlider.maxValue, time / incressDuration);
            }
            else
            {
                happinessSlider.value = happinessSlider.maxValue;
                isIncreasing = false;
                SpawnStar();
                Debug.Log("Star Drop!!!");
                reset();
            }
        }
    }

    void reset()
    {
        happinessSlider.value = 0;
        isIncreasing = true;
        startTime = Time.time;
    }

    void SpawnStar()
    {
        // Maku 위치에서 조금 위의 위치에서 Star 인스턴스를 생성
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // Y축 방향으로 1만큼 올린 위치
        GameObject spawnedStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = spawnedStar.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(0, 5f, 0); // Y축 방향으로의 초기 속도 설정
        }
    }
}