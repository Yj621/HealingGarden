using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour
{
    public Slider happinessSlider;

    //  public float spotRadius = 1.0f;
    public float incressDuration = 10f; //슬라이더가 최대치까지 증가하는데 걸리는 시간

    private bool isIncreasing = false; //슬라이더 증가 상태
    private float startTime; //슬라이더 증가가 시작된 시간

    public bool happiness = false;

    public StarController resetHappiness;

    void Start()
    {
        happinessSlider.value = 0;
        isIncreasing = true;
        happiness = false;
        startTime = Time.time;
    }

    void Update()
    {
        if(isIncreasing)
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
                isIncreasing = false;
                StarDropTrigger();
                Debug.Log("Star Drop!!!");
                StarDropStop();
                gameObject.SetActive(false);
            }
        }
    }

    public bool StarDropTrigger()
    {
        happiness = true;
        return happiness;
    }
    public bool StarDropStop()
    {
        happiness = false;
        return happiness;
    }
}