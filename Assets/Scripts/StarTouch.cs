using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTouch : MonoBehaviour
{
    public static int StarPoint = 0;
    DataManager dataManager;
    private float touchCooldown = 0.2f; // 터치 입력을 무시할 시간 (초)
    private float lastTouchTime = -1f; // 마지막 터치 시간 저장 변수

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    void Update()
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
                            DestroyManager.Instance.DestroyGameObject(hitObj); // 즉시 파괴 요청

                            StarPoint++;
                            dataManager.GetStarCandy();
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
}