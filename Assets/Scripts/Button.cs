using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    // 버튼의 이름을 저장하는 변수
    public string buttonName;
    private int buttonLv = 1;

    // 버튼의 작업에 필요한 기본 자원 양
    private int _requiredAmount = 100;
    public int requiredAmount
    {
        get { return _requiredAmount; }
        set
        {
            _requiredAmount = value;
            UpdateAmountText(); // 필요 자원 양이 변경될 때 텍스트 업데이트
        }
    }

    // 버튼의 상태를 정의하는 열거형 (구매 또는 업그레이드)
    public enum ButtonState { Buy, Upgrade }
    public ButtonState buttonState = ButtonState.Buy;

    // 데이터 관리자를 참조하는 변수
    public DataManager dataManager;
    // 필요 자원 양을 표시하는 UI 텍스트
    public Text amountText;
    public Text buttonLvText;

    void Start()
    {
        if (dataManager == null)
        {
            dataManager = FindObjectOfType<DataManager>();
        }

        buttonLvText.text = "Lv." + buttonLv;
        UpdateButtonText();
        UpdateAmountText();
    }
    // 버튼 상태를 설정하는 함수
    public void SetButtonState(ButtonState state)
    {
        buttonState = state;
        UpdateButtonText(); // 버튼 텍스트 업데이트
    }

    // 버튼 텍스트를 업데이트하는 함수
    private void UpdateButtonText()
    {
        // 버튼의 자식 텍스트 컴포넌트를 찾아 텍스트를 설정
        GetComponentInChildren<Text>().text = buttonState == ButtonState.Buy ? "구매" : "업그레이드";
    }

    // 필요 자원 텍스트를 업데이트하는 함수
    private void UpdateAmountText()
    {
        // amountText가 null이 아닌 경우 텍스트 업데이트
        if (amountText != null)
        {
            amountText.text = requiredAmount.ToString();
        }
    }

    // 버튼이 클릭되었을 때 호출되는 함수
    public void OnClick()
    {
        if (buttonState == ButtonState.Buy)
        {
            if (CanAction()) // 자원이 충분한지 확인
            {
                Action(); // 행동 수행
                buttonState = ButtonState.Upgrade; // 상태를 업그레이드로 변경
                UpdateButtonText(); // 버튼 텍스트 업데이트
            }
            else
            {
                Debug.Log("구매할 자원이 부족합니다."); // 자원이 부족한 경우 메시지 출력
            }
        }
        else if (buttonState == ButtonState.Upgrade)
        {
            if (CanAction()) // 자원이 충분한지 확인
            {
                Action(); // 행동 수행
                Debug.Log("업그레이드 처리"); // 업그레이드 메시지 출력
            }
            else
            {
                Debug.Log("업그레이드할 자원이 부족합니다."); // 자원이 부족한 경우 메시지 출력
            }
        }
    }

    // 돈이 충분한지 확인하는 함수 (구매 또는 업그레이드)
    private bool CanAction()
    {
        switch (buttonName)
        {
            case "별사탕 보너스":
                return dataManager.StarCandy >= requiredAmount;
            case "변환 자동화":
                return dataManager.StarCandy >= requiredAmount;
            default:
                return false; // 지원되지 않는 버튼 이름의 경우 false 반환
        }
    }

    // 버튼을 누르면 작동하는 함수
    private void Action()
    {
        switch (buttonName)
        {
            case "별사탕 보너스":
                dataManager.C_getMoney += 5; // 별사탕 보너스 행동 수행
                buttonLv++;
                break;
            case "변환 자동화":
                // 변환 자동화 추가 로직 구현
                break;
            default:
                break;
        }
        dataManager.StarCandy -= requiredAmount; // 필요한 자원 차감
        requiredAmount += (buttonName == "별사탕 보너스") ? 100 : 50; // 필요 자원 증가
        buttonLvText.text = "Lv." + buttonLv;
    }
}