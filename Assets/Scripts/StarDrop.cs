using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDrop : MonoBehaviour
{
    public GameObject starPrefab;
    
    private float timer;

    void Start()
    {
        timer = Random.Range(1f, 10f); // 초기 타이머를 랜덤하게 설정 (1~5초)
    }

    void Update()
    {
        timer -= Time.deltaTime; // 타이머 감소

        if (timer <= 0)
        {
            SpawnStar(); // Star 생성 함수 호출
            timer = Random.Range(1f, 10f);  // 타이머를 다시 랜덤하게 설정
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
}

