using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public static int StarPoint = 0;
    DataManager dataManager;
    private float touchCooldown = 0.2f; // 터치 입력을 무시할 시간 (초)
    private float lastTouchTime = -1f; // 마지막 터치 시간 저장 변수

    public GameObject starPrefab;
    
    private float DuringSpawn;
    private float timer = 0f;

    public bool EnterSpot = false;

    void Start()
    {
        DuringSpawn = Random.Range(1f, 10f); // 초기 타이머를 랜덤하게 설정 (1~5초)

        dataManager = FindObjectOfType<DataManager>();
    }

    void Update()
    {
        if (EnterSpot == true)
        {
            SpawnStart();
        }

        SpawnStart();
        // Star의 현재 위치를 체크
        if (transform.position.y <= 0f)
        {
            Test();
        }
        TouchDestroy();

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

    void TouchDestroy()
    {
        if (Time.time > lastTouchTime + touchCooldown)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit[] hits = Physics.RaycastAll(ray);

                    bool touchedStar = false;

                    foreach (RaycastHit hit in hits)
                    {
                        GameObject hitObj = hit.collider.gameObject;
                        if (hitObj.tag == "Star")
                        {
                            hitObj.SetActive(false); // Star(Clone) 비활성화
                            DestroyGameObject(hitObj); // 즉시 파괴 요청

                            StarPoint++;
                            dataManager.GetStar();
                            Debug.Log("StarPoint: " + StarPoint);

                            touchedStar = true;
                        }
                    }
                    if (touchedStar)
                    {
                        lastTouchTime = Time.time;
                    }
                }
            }
        }
    }

    void SpawnStart()
    {
        if (timer >= 10f)   // 타이머가 10초가 되면 멈춘다.
            return;

        timer += Time.deltaTime;    // 타이머 가동
        DuringSpawn -= Time.deltaTime; // 10초 스톱워치 감소

        if (DuringSpawn <= 0)
        {
            SpawnStar(); // Star 생성 함수 호출
            DuringSpawn = Random.Range(1f, 10f);  // 스톱워치를 다시 랜덤하게 설정
        }
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

    public void DestroyGameObject(GameObject obj)   // 별 부숴버리기
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
    
}
// 이 코드를 실행하면 생성된 Star가 특정 y좌표 (0f)에서 멈춰버리기 때문에 Collider에서
// isTrigger를 On으로 해놔도 땅바닥으로 뚫고 떨어지지도 않고, 별들끼리 겹쳐서 막 이리저리 움직이지도 않음
