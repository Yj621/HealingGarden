using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTouch : MonoBehaviour
{
    // StarPoint를 저장할 정적 변수 선언
    public static int StarPoint = 0;

    void Update()
    {
        // 모바일 환경에서 터치 입력을 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 첫 번째 터치가 화면에 처음 닿는 순간에만 실행
            if (touch.phase == TouchPhase.Began)
            {
                // 카메라로부터 터치 위치로 Ray를 발사
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Ray에 오브젝트가 맞았는지 확인
                if (Physics.Raycast(ray, out hit))
                {
                    // 맞은 오브젝트의 태그가 'Star'인지 확인
                    if (hit.collider.gameObject.tag == "Star")
                    {
                        // 'Star' 오브젝트를 비활성화
                        Destroy(hit.collider.gameObject);
                        
                        // StarPoint 값을 1 증가
                        StarPoint++;

                        // StarPoint의 현재 값을 콘솔에 출력
                        Debug.Log("StarPoint: " + StarPoint);
                    }
                }
            }
        }
    }
}

