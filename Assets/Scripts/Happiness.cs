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
    public float incressDuration = 10f; //�����̴��� �ִ�ġ���� �����ϴµ� �ɸ��� �ð�

    private bool isIncressing = false; //�����̴� ���� ����
    private float startTime; //�����̴� ������ ���۵� �ð�

    void Start()
    {
        happinessSlider.value = 0;
    }

    void Update()
    {
        // �÷��̾ spot�� �����ߴ��� Ȯ��
        if (Vector3.Distance(player.position, poolSpot.position) <= spotRadius && !isIncressing)
        {
            //���� ��õ���� ��� ���߰� ���ּ���
            isIncressing = true;
            startTime = Time.time;
        }

        if(isIncressing)
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
                isIncressing = false;
                Debug.Log("test");
                //���� �ٽ� �����̰� ���ּ���
            }
        }
    }
}
