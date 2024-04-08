using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour
{
    DataManager dataManager;

    [Header("변환 패널")]
    public Slider convertSlider;
    public Text convertedText; // 변환된 값이 표시될 텍스트

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>(); 
        UpdateConvertedValue(); // 시작 시 변환된 값을 업데이트
    }

    // 변환 버튼 클릭 시 호출되는 이벤트 핸들러
    public void OnBtnConvert()
    {
        int convertedValue = Mathf.FloorToInt(convertSlider.value / 10); // Slider 값을 10으로 나눈 값
        dataManager.Resource("StarCandy", 0, -convertedValue); // StarCandy 재화를 차감
        dataManager.Resource("Star", 0, convertedValue); // Star 재화를 추가
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    // BtnMax 버튼 클릭 시 호출되는 이벤트 핸들러
    public void OnBtnMaxClicked()
    {
        // 최대 변환 가능한 값 계산
        int maxConvertedValue = Mathf.FloorToInt(dataManager.StarCandy[dataManager.C_index] / 10); 
        // Slider의 최대값과 비교하여 최대값보다 큰 경우 최대값으로 설정
        if (maxConvertedValue > convertSlider.maxValue)
        {
            maxConvertedValue = (int)convertSlider.maxValue;
        }
        convertSlider.value = maxConvertedValue * 10; // Slider 값을 최대 변환 가능 값으로 설정
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }
    // BtnMinus 버튼 클릭 시 호출되는 이벤트 핸들러
    public void OnBtnMinusClicked()
    {
        convertSlider.value = 0; // Slider 값을 0으로 초기화
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    // Slider 값이 변경될 때 호출되는 이벤트 핸들러
    public void OnSliderValueChanged(float value)    
    {
        UpdateConvertedValue(); // Slider 값이 변경될 때마다 변환된 값을 업데이트
    }

    // 변환된 값을 텍스트로 업데이트하는 메서드
    void UpdateConvertedValue()
    {
        int convertedValue = Mathf.FloorToInt(convertSlider.value / 10); // Slider 값을 10으로 나눈 값
        convertedText.text = convertedValue.ToString(); // 변환된 값을 텍스트로 표시
    }

}
