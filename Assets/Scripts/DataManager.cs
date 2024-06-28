using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataManager : MonoBehaviour
{
    // 총 재화의 지갑 역할
    public int Star;
    public int StarCandy;

    // 클릭당 돈은 얼마나 얻을 것인지
    public int S_getMoney;
    public int C_getMoney;

    // 출력하는 오브젝트를 받는 변수
    public Text S_Text;
    public Text C_Text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResource();
    }

    //재화를 업데이트하는 함수
    public void UpdateResource()
    {
        C_Text.text = StarCandy.ToString();
        S_Text.text = Star.ToString();
    }

    

    //사용 방법 Resource("변환할 재화의 string",  변환할 재화 값)
    public void Resource(string currencyType, int amount)
    {
        if (currencyType == "StarCandy")
        {
            StarCandy += amount;
            Debug.Log("StarCandy : " + StarCandy);
        }
        else if (currencyType == "Star")
        {
            Star += amount;
        }
    }
    

    public void GetStarCandy()
    {
        StarCandy += C_getMoney;
        UpdateResource();
    }
}
