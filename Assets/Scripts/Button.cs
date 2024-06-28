using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public string buttonName;
    public enum ButtonState { Buy, Upgrade }
    public ButtonState buttonState = ButtonState.Buy;

    // DataManager에 대한 참조
    public DataManager dataManager;

    void Start()
    {
        // DataManager가 할당되지 않은 경우 Scene에서 DataManager를 찾습니다.
        if (dataManager == null)
        {
            dataManager = FindObjectOfType<DataManager>();
            if (dataManager == null)
            {
                Debug.LogError("DataManager를 찾을 수 없습니다!");
                return;
            }
        }

        UpdateButtonText();
    }
    
    // 구매 & 업그레이드 텍스트 상태 변경
    public void SetButtonState(ButtonState state)
    {
        buttonState = state;
        UpdateButtonText();
    } 

    private void UpdateButtonText()
    {
        GetComponentInChildren<Text>().text = buttonState == ButtonState.Buy ? "구매" : "업그레이드";
    }

    //구매 & 업그레이드 버튼
    public void OnClick()
    {
        if (buttonState == ButtonState.Buy)
        {
            // 충분한 자원이 있는지 확인하여 구매 처리
            if (CanBuy())
            {
                Debug.Log("구매 처리");
                buttonState = ButtonState.Upgrade;
                UpdateButtonText();
            }
            else
            {
                Debug.Log("구매할 자원이 부족합니다.");
            }
        }
        else if (buttonState == ButtonState.Upgrade)
        {
            // 충분한 자원이 있는지 확인하여 업그레이드 처리
            if (CanUpgrade())
            {
                Debug.Log("업그레이드 처리");
            }
            else
            {
                Debug.Log("업그레이드할 자원이 부족합니다.");
            }
        }
    }

    // 구매할 자원이 충분한지 확인
    private bool CanBuy()
    {
        // 플레이어가 구매에 필요한 자원을 충분히 가지고 있는지 확인하는 로직
        // 예시: 구매 비용에 대한 로직을 여기에 구현
        int requiredAmount = 100; // 예시: 구매 비용
        return dataManager.Star[dataManager.S_index] >= requiredAmount;
    }

    // 업그레이드할 자원이 충분한지 확인
    private bool CanUpgrade()
    {
        // 플레이어가 업그레이드에 필요한 자원을 충분히 가지고 있는지 확인하는 로직
        // 예시: 업그레이드 비용에 대한 로직을 여기에 구현
        int requiredAmount = 200; // 예시: 업그레이드 비용
        return dataManager.StarCandy[dataManager.C_index] >= requiredAmount;
    }
}
