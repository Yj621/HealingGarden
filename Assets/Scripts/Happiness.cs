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

    //별 카운트
    public int StarCount = 0;
    public int HowManyStar = 1;

    Timer timer;

    void Start()
    {
        
        timer = FindAnyObjectByType<Timer>();
        timerIsRunning = true;
        timer.isTimerRunning = true;
        happinessSlider.value = 0;
        isIncreasing = true;
        startTime = Time.time;
        StarCount = 0;
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
                for (int i = 0; i < HowManyStar; i++)
                {
                    SpawnStar();
                    Debug.Log(string.Format("{0} star drops!!!", HowManyStar)); // 별조각 몇개 떨어지는지 로그출력
                }
                Reset();
                StarCount++;
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
        // Y축을 제외한 X, Z 좌표를 0.5 반경 내에서 랜덤으로 설정
        float randomX = Random.Range(-0.3f, 0.3f); // -0.5 ~ 0.5 사이의 랜덤 값
        float randomZ = Random.Range(-0.3f, 0.3f); // -0.5 ~ 0.5 사이의 랜덤 값

        // Maku 위치에서 Y축은 1만큼 위, X와 Z는 랜덤으로 더한 위치에 Star 인스턴스 생성
        Vector3 spawnPosition = transform.position + new Vector3(randomX, 1f, randomZ);
        GameObject spawnedStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = spawnedStar.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Y축은 위로 상승하면서 X, Z축은 포물선을 그리도록 속도를 적용
            float upwardVelocity = Random.Range(3f, 6f); // Y축 속도를 랜덤으로 설정하여 다양성 부여
            float horizontalVelocityX = Random.Range(-1f, 1f); // X축 방향 랜덤 속도
            float horizontalVelocityZ = Random.Range(-1f, 1f); // Z축 방향 랜덤 속도

            rb.velocity = new Vector3(horizontalVelocityX, upwardVelocity, horizontalVelocityZ); // 포물선 형태의 초기 속도
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