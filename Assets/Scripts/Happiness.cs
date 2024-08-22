using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Happiness : MonoBehaviour
{
    public Slider happinessSlider;

    //  public float spotRadius = 1.0f;
    public float incressDuration = 10f; //�����̴��� �ִ�ġ���� �����ϴµ� �ɸ��� �ð�

    private bool isIncreasing = false; //�����̴� ���� ����
    private float startTime; //�����̴� ������ ���۵� �ð�

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
            //Debug.Log("��� �ð�: " + time + "��");
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