using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private float DuringSpawn;
    private float s_timer = 0f;
    private bool Escape_Spot = false;

    public Timer timer;
    public GameObject Timer;

    void Start()
    {
        Timer.SetActive(false);
        timer = FindAnyObjectByType<Timer>();
        Escape_Spot = false;
    }

    void Update()
    {
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
}
