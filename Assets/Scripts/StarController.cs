using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private float DuringSpawn;
    private float s_timer = 0f;
    private bool Escape_Spot = false;
    
    public GameObject starPrefab;

    public Happiness happiness;
    public Timer timer;
    private bool stardrop;
    public GameObject HappinessBar;
    public GameObject Timer;

    void Start()
    {
        Timer.SetActive(false);
        timer = FindAnyObjectByType<Timer>();
        Escape_Spot = false;
        DuringSpawn = Random.Range(1f, 10f); // 초기 타이머를 랜덤하게 설정 (1~5초)
        happiness.StarDropStop();
    }

    void Update()
    {
        happiness.StarDropTrigger();
        bool stardrop = happiness.happiness;

        if (stardrop)
        {
            SpawnStart();
            happiness.StarDropStop();
        }
        // Star의 현재 위치를 체크
        if (transform.position.y <= 0f)
        {
            Test();
        }

        if (Escape_Spot == true)
        {
            gameObject.SetActive(false);
        }


        if (s_timer >= 300f)   // 타이머가 10초가 되면 멈춘다.
        {
            Escape_Spot = true;
        }

    }
    void Test()
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

    void SpawnStart()
    {
        Timer.SetActive(true);
        Debug.Log("Bar Activation");
        timer.isTimerRunning = true;
        SpawnStar(); // Star 생성 함수 호출
        HappinessBar.SetActive(true);
    }
     void SpawnStar()
    {
        // Cube 위치에서 조금 위의 위치에서 Star 인스턴스를 생성
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // Y축 방향으로 1만큼 올린 위치
        GameObject spawnedStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
        
        Rigidbody rb = spawnedStar.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.velocity = new Vector3(0, 5f, 0); // Y축 방향으로의 초기 속도 설정
        }
    }
}
// 이 코드를 실행하면 생성된 Star가 특정 y좌표 (0f)에서 멈춰버리기 때문에 Collider에서
// isTrigger를 On으로 해놔도 땅바닥으로 뚫고 떨어지지도 않고, 별들끼리 겹쳐서 막 이리저리 움직이지도 않음
