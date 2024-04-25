using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    DataManager dataManager;

    [Header("변환 패널")]
    public GameObject ConvertUI;
    public GameObject HomeUI;
    public Slider convertSlider;
    public Text s_Text; // 변환된 값이 표시될 텍스트
    public Text c_Text; // 변환된 값이 표시될 텍스트

    private char currencyUnit;
    private int currentIndex = 65;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();

        UpdateConvertedValue(); // 시작 시 변환된 값을 업데이트

        // Slider 값이 변경될 때마다 호출되는 이벤트 핸들러 등록
        convertSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        currencyUnit = dataManager.GetCurrencyUnit(); // 재화 단위 가져오기
        
    }

    // plus 버튼 클릭 시 재화 단위를 증가시키고 변환된 값 업데이트
    public void OnBtnPlus()
    {       
        // 현재 재화 단위 인덱스가 최대 인덱스보다 작을 때만 증가시킴
        if (currentIndex-65 < dataManager.S_index)
        {
            currentIndex++;
        }
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }
        
    // minus 버튼 클릭 시 재화 단위를 감소시키고 변환된 값 업데이트
    public void OnBtnMinus()
    {
        // 현재 재화 단위 인덱스가 최소 인덱스보다 클 때만 감소시킴 (최소 단위 A를 넘어가지 않도록)
        if (dataManager.S_index > 0 && currentIndex > 65)
        {
            currentIndex--;
        }
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    // 변환 버튼 클릭 시 
    public void OnBtnConvert()
    {
        // 변환하려는 양을 계산하여 현재 가지고 있는 재화와 단위에 맞게 설정
        int convertedValue = Mathf.FloorToInt(convertSlider.value / 10); // Slider 값을 10으로 나눈 값
        int convertedStar = convertedValue * 10; // 변환할 Star 재화

        // 충분한 Star 재화가 있는지 확인
        if (convertedStar > dataManager.Star[dataManager.S_index])
        {
            Debug.Log("Star 재화가 부족합니다.");
            return; // Star 재화가 부족하면 변환을 진행하지 않음
        }

        // 충분한 재화가 있다면 변환 실행
        dataManager.Resource("Star",  -convertedStar, dataManager.S_index); // Star 재화를 차감
        dataManager.Resource("StarCandy", convertedValue, dataManager.C_index); // StarCandy 재화를 추가
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }


    // Slider 값이 변경될 때 호출되는 이벤트 핸들러
    public void OnSliderValueChanged(float value)
    {
        
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    void UpdateConvertedValue()
    {
        // Slider의 최대값 설정
        int maxConvertibleValue = Mathf.FloorToInt(dataManager.Star[currentIndex-65]);
        convertSlider.maxValue = maxConvertibleValue;
        // // 현재 가지고 있는 재화와 단위에 따라 최대값을 설정

        int convertedValue = Mathf.FloorToInt(convertSlider.value / 10); // Slider 값을 10으로 나눈 값
        s_Text.text = (convertedValue * 10).ToString() + (char)currentIndex; // 변환된 Star 값을 텍스트로 표시
        c_Text.text = convertedValue.ToString() + (char)currentIndex; // 변환된 StarCandy 값을 텍스트로 표시
    }

    public void ActiveConverUI()
    {
        ConvertUI.SetActive(true);
    }
    public void ActiveHomeUI()
    {
        HomeUI.SetActive(true);
    }
}
