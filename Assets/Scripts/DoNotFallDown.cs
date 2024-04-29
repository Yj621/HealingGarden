using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotFallDown : MonoBehaviour
{
    // 이 스크립트는 생성된 스타가 땅에 떨어지는 것을 막는 임시 스크립트입니다.

    void Update()
    {
        // 위치 고정
        if (transform.position.y <= 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

            // Rigidbody를 kinematic으로 설정하여 더 이상 물리 엔진에 의해 움직이지 않도록 함
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // 물리 연산을 비활성화하여 추가적인 물리적 영향을 받지 않도록 설정
            }
        }
    }
}
