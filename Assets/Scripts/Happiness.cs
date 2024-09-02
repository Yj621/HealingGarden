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

        if (transform.position.y <= 0f)
        {
            Stand_Here();   // 땅에 떨어지지 않게 하는 것
        }

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
                Reset();
            }
        }
    }

    void Reset()
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

    void Stand_Here()
    {
        // 위치 고정
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        // Rigidbody를 kinematic으로 설정하여 더 이상 물리 엔진에 의해 움직이지 않도록 함
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 물리 연산을 비활성화하여 추가적인 물리적 영향을 받지 않도록 설정
        }
    }
}