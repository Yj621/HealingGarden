using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    UIController uIController;
    //줌 속도 변수
    private float perspectiveZoomSpeed = 0.008f;
    private float orthoZoomSpeed = 0.001f;

    //이동 속도
    private float moveSpeed = 0.01f;

    // 별조각 관련 변수
    DataManager dataManager;    // 데이터매니저
    public static int StarPoint = 0;    // 먹은 별조각
    private float touchCooldown = 0.2f; // 터치 입력을 무시할 시간 (초)
    private float lastTouchTime = -1f; // 마지막 터치 시간 저장 변수
    public GameObject starPrefab;   // 부숴버릴 별조각 Prefab

    void Start()
    {
        uIController = FindAnyObjectByType<UIController>();
        dataManager = FindObjectOfType<DataManager>();  // 데이터매니저 (별조각)
    }

    void Update()
    {
        TouchDestroy(); // 별 부수기 함수 (항상 켜져있어야 언제든 별조각을 터치로 먹을 수 있음)

        if (uIController.isPanelOn==false)
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

    void TouchDestroy() // 별 부수기 - 1
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
    public void DestroyGameObject(GameObject obj)   // 별 부수기 - 2
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}