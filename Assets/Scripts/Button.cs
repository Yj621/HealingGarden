using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public string buttonName;

    private int _requiredAmount = 100;
    public int requiredAmount
    {
        get { return _requiredAmount; }
        set
        {
            _requiredAmount = value;
            UpdateAmountText();
        }
    }

    public enum ButtonState { Buy, Upgrade }
    public ButtonState buttonState = ButtonState.Buy;

    public DataManager dataManager;
    public Text amountText;

    void Start()
    {
        if (dataManager == null)
        {
            dataManager = FindObjectOfType<DataManager>();
        }
        UpdateButtonText();
        UpdateAmountText();
    }

    public void SetButtonState(ButtonState state)
    {
        buttonState = state;
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        GetComponentInChildren<Text>().text = buttonState == ButtonState.Buy ? "구매" : "업그레이드";
    }

    private void UpdateAmountText()
    {
        if (amountText != null)
        {
            amountText.text = requiredAmount.ToString();
        }
    }

    public void OnClick()
    {
        if (buttonState == ButtonState.Buy)
        {
            if (CanAction())
            {
                Action();
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
            if (CanAction())
            {
                Action();
                Debug.Log("업그레이드 처리");
            }
            else
            {
                Debug.Log("업그레이드할 자원이 부족합니다.");
            }
        }
    }

    //돈이 충분한지 확인하는(구매 or 업그레이드)
    private bool CanAction()
    {
        switch (buttonName)
        {
            case "별사탕 보너스":
                return dataManager.StarCandy >= requiredAmount;
            case "변환 자동화":
                return dataManager.StarCandy >= requiredAmount;
            default:
                return false;
        }
    }

    //버튼을 누르면 작동하는 함수
    private void Action()
    {
        switch (buttonName)
        {
            case "별사탕 보너스":
                dataManager.C_getMoney += 1;
                break;
            case "변환 자동화":
                // 추가 로직 구현
                break;
            default:
                break;
        }
        dataManager.StarCandy -= requiredAmount;
        requiredAmount += (buttonName == "별사탕 보너스") ? 100 : 50;
    }
}
