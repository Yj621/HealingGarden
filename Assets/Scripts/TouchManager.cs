using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    UIController uIController;
    //줌 속도 변수
    private float perspectiveZoomSpeed = 0.008f;
    private float orthoZoomSpeed = 0.0001f;

    //이동 속도
    private float moveSpeed = 0.001f;

    void Start()
    {
        uIController = FindAnyObjectByType<UIController>();
    }

    void Update()
    {
        // 오브젝트 클릭 여부에 따라 처리
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            // 오브젝트가 선택되지 않은 경우 카메라 이동
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    // Frying pan 오브젝트를 터치했는지 여부 확인
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (Physics.Raycast(ray, out hit))
                        {
                            string objectName = hit.collider.gameObject.name;
                            switch (objectName)
                            {
                                case "FryingPan":
                                    uIController.ActiveConverUI();
                                    Debug.Log("ConvertUI");
                                    break;
                                case "Home":
                                    uIController.ActiveHomeUI();
                                    Debug.Log("HomeUI");
                                    break;
                                case "Pool":
                                    uIController.ActivePoolUI();
                                    Debug.Log("PoolUI");
                                    break;
                            }
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    Vector2 touchDelta = touch.deltaPosition;

                    if (touchDelta.x != 0 || touchDelta.y != 0)
                    {
                        transform.Translate(-touchDelta.x * moveSpeed, -touchDelta.y * moveSpeed, 0);
                    }
                    break;
            }
        }

        //접촉되어 있는 손가락 개수가 2일때(두 손가락을 터치했을때)
        else if (Input.touchCount == 2)
        {
            //첫번째 터치 정보 저장
            Touch touchZero = Input.GetTouch(0);
            //두번째 터치 정보 저장
            Touch touchOne = Input.GetTouch(1);

            //각각 터치의 이전 위치를 계산한다.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //(목적지-현재위치).magnitude : 남은 거리
            //이전 위치와 현재 위치간의 거리를 계산한다
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //두 손가락 간의 거리를 계산한다
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //orthographic모드일때
            if (GetComponent<Camera>().orthographic)
            {
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 0.1f);
            }
            //fieldOfView모드일때
            else
            {
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}