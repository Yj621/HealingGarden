using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour
{
    public Slider happinessSlider;
    public Transform player;
    public Transform poolSpot;

    public float spotRadius = 1.0f;
    public float incressDuration = 10f; //슬라이더가 최대치까지 증가하는데 걸리는 시간

    private bool isIncressing = false; //슬라이더 증가 상태
    private float startTime; //슬라이더 증가가 시작된 시간

    void Start()
    {
        happinessSlider.value = 0;
    }

    void Update()
    {
        // 플레이어가 spot에 도달했는지 확인
        if (Vector3.Distance(player.position, poolSpot.position) <= spotRadius && !isIncressing)
        {
            //마쿠가 온천에서 잠깐 멈추게 해주세요
            isIncressing = true;
            startTime = Time.time;
        }

        if(isIncressing)
        {
            float time = Time.time - startTime;
            //Debug.Log("경과 시간: " + time + "초");
            if (time < incressDuration)
            {
                
                happinessSlider.value = Mathf.Lerp(0, happinessSlider.maxValue, time / incressDuration);
            }
            else
            {
                happinessSlider.value = happinessSlider.maxValue;
                isIncressing = false;
                Debug.Log("test");
                //마쿠가 다시 움직이게 해주세요
            }
        }
    }
}
