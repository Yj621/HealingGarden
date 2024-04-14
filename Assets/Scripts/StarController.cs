using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    void Update()
    {
        // Star의 현재 위치를 체크
        if (transform.position.y <= 0f)
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
}
// 이 코드를 실행하면 생성된 Star가 특정 y좌표 (0f)에서 멈춰버리기 때문에 Collider에서
// isTrigger를 On으로 해놔도 땅바닥으로 뚫고 떨어지지도 않고, 별들끼리 겹쳐서 막 이리저리 움직이지도 않음
