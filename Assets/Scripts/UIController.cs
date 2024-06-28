using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public bool isPanelOn = false;
    DataManager dataManager;   
    public GameObject BlockingPanel;

    [Header("변환 패널")]
    public GameObject ConvertUI;
    public GameObject HomeUI;
    public GameObject PoolUI;
    public Slider convertSlider;
    public Text s_Text; // 변환된 값이 표시될 텍스트
    public Text c_Text; // 변환된 값이 표시될 텍스트


    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();

        UpdateConvertedValue(); // 시작 시 변환된 값을 업데이트

        // Slider 값이 변경될 때마다 호출되는 이벤트 핸들러 등록
        convertSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        UpdateConvertedValue();
    }

    // plus 버튼 클릭 시 재화 단위를 증가시키고 변환된 값 업데이트
    public void OnBtnPlus()
    {       
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }
        
    // minus 버튼 클릭 시 재화 단위를 감소시키고 변환된 값 업데이트
    public void OnBtnMinus()
    {
       
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    // 변환 버튼 클릭 시 
    public void OnBtnConvert()
    {
        // 변환하려는 양을 계산하여 현재 가지고 있는 재화와 단위에 맞게 설정
        int convertedStarCandy = Mathf.FloorToInt(convertSlider.value)*10;
        int convertedStar = convertedStarCandy /10 ; // 변환할 Star 재화


        // 충분한 Star 재화가 있는지 확인
        if (dataManager.StarCandy >= convertedStarCandy)
        {
            Debug.Log("convertedStarCandy: "+convertedStarCandy);

            // 충분한 재화가 있다면 변환 실행
            dataManager.Resource("StarCandy",  -convertedStarCandy); // StarCandy 재화를 차감
            dataManager.Resource("Star", convertedStar); // Star 재화를 추가
            UpdateConvertedValue(); // 변환된 값을 업데이트
        }
        else
        {
            Debug.Log("Candy 재화가 부족합니다.");
            return; 
        }
    }


    // Slider 값이 변경될 때 호출되는 이벤트 핸들러
    public void OnSliderValueChanged(float value)
    {
        UpdateConvertedValue(); // 변환된 값을 업데이트
    }

    void UpdateConvertedValue()
    {
        // Slider의 최대값 설정
        int maxConvertibleValue = Mathf.FloorToInt(dataManager.StarCandy);
        convertSlider.maxValue = maxConvertibleValue / 10;

        //현재 가지고 있는 재화와 단위에 따라 최대값을 설정
        int convertedValue = Mathf.FloorToInt(convertSlider.value); // Slider 값을 10으로 나눈 값
        s_Text.text = (convertedValue * 10).ToString(); // 변환된 Star 값을 텍스트로 표시
        c_Text.text = convertedValue.ToString(); // 변환된 StarCandy 값을 텍스트로 표시
    }

    public void ActiveConverUI()
    {
        ConvertUI.SetActive(true);
        BlockingPanelOn();
    }
    public void ActiveHomeUI()
    {
        HomeUI.SetActive(true);
        BlockingPanelOn();
    }
    public void ActivePoolUI()
    {
        PoolUI.SetActive(true);
        BlockingPanelOn();
    }

    public void BlockingPanelOn()
    {
        BlockingPanel.SetActive(true);
        isPanelOn = true;
    }
    public void BlockingPanelOff()
    {
        BlockingPanel.SetActive(false);
        isPanelOn = false;
    }
}
