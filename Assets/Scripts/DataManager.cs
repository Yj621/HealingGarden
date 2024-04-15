using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataManager : MonoBehaviour
{
    // 총 재화의 지갑 역할
    public int[] Star;
    public int[] StarCandy;

    // 현재 나의 재화의 상태
    public int S_index;
    public int C_index;

    // 클릭당 돈은 얼마나 얻을 것인지
    public int[] S_getMoney;
    public int[] C_getMoney;

    // 출력하는 오브젝트를 받는 변수
    public Text S_Text;
    public Text C_Text;

    // 추가: 현재 재화 단위를 가져오는 메서드
    public char GetCurrencyUnit()
    {
        // 자료형에서 65부터 A를 표현하기 때문에 쓰는 코드 
        return (char)(65 + S_index);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Theorem();
        S_ToString();
        C_ToString();
    }

    void Theorem()
    {
        // 내 자산의 현재 상태값을 알 수 있도록 하는 코드
        for (int i = 0; i < 26; i++)
        {
            if (StarCandy[i] > 0)
            {
                C_index = i;
            }
        }
        // index값 만큼 돈 단위를 정리하는 반복문을 돌린다.
        for (int i = 0; i <= C_index; i++)
        {
            // 만약, i번째 배열에 돈이 1000이상이라면
            // 거기서 1000을 빼고 윗 배열에 1을 더해준다.
            if (StarCandy[i] >= 1000)
            {
                StarCandy[i] -= 1000;
                StarCandy[i + 1] += 1;
            }
            // 만약, i번째 배열의 값이 음수라면
            if (StarCandy[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번째 배열에 1000을 더한다.
                if (C_index > i)
                {
                    StarCandy[i + 1] -= 1;
                    StarCandy[i] += 1000;
                }
            }
        }

        // 내 자산의 현재 상태값을 알 수 있도록 하는 코드
        for (int i = 0; i < 26; i++)
        {
            if (Star[i] > 0)
            {
                S_index = i;
            }
        }
        // index값 만큼 돈 단위를 정리하는 반복문을 돌린다.
        for (int i = 0; i <= S_index; i++)
        {
            // 만약, i번째 배열에 돈이 1000이상이라면
            // 거기서 1000을 빼고 윗 배열에 1을 더해준다.
            if (Star[i] >= 1000)
            {
                Star[i] -= 1000;
                Star[i + 1] += 1;
            }
            // 만약, i번째 배열의 값이 음수라면
            if (Star[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번째 배열에 1000을 더한다.
                if (S_index > i)
                {
                    Star[i + 1] -= 1;
                    Star[i] += 1000;
                }
            }
        }
    }

    string C_ToString()
    {
        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현
        float a = StarCandy[C_index];
        // 만약, index가 0보다 크다면 소수점이 나온다는 것
        if (C_index > 0)
        {
            float b = StarCandy[C_index - 1];
            a += b / 1000;
        }
        // 만약, 0과 같다면 바로 출력
        if (C_index == 0)
        {
            a += 0;
        }
        // 자료형에서 65부터 A를 표현하기 때문에 쓰는 코드 
        char unit = (char)(65 + C_index);
        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + unit.ToString();
        C_Text.text = p;

        return p;
    }

    string S_ToString()
    {
        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현
        float a = Star[S_index];
        // 만약, index가 0보다 크다면 소수점이 나온다는 것
        if (S_index > 0)
        {
            float b = Star[S_index - 1];
            a += b / 1000;
        }
        // 만약, 0과 같다면 바로 출력
        if (S_index == 0)
        {
            a += 0;
        }
        // 자료형에서 65부터 A를 표현하기 때문에 쓰는 코드 
        char unit = (char)(65 + S_index);
        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + unit.ToString();
        S_Text.text = p;

        return p;
    }

    //사용 방법 Resource("변환할 재화의 string",  변환할 재화 값, 변환할 재화의 단위)
    public void Resource(string currencyType, int amount, int index)
    {
        if (currencyType == "StarCandy")
        {
            if (index >= 0 && index < StarCandy.Length)
            {
                StarCandy[index] += amount;
                if (amount < 0)
                {
                    // 재화가 음수가 되면 윗 배열에서 값을 빼고 음수 값을 조정
                    for (int i = index; i < StarCandy.Length - 1 && StarCandy[i] < 0; i++)
                    {
                        StarCandy[i + 1] += StarCandy[i] / 1000 - 1;
                        StarCandy[i] = (StarCandy[i] % 1000 + 1000) % 1000;
                    }
                }
                else
                {
                    // 재화가 양수가 되면 윗 배열로 넘어가는 부분 조정
                    for (int i = index; i < StarCandy.Length - 1 && StarCandy[i] >= 1000; i++)
                    {
                        StarCandy[i + 1] += StarCandy[i] / 1000;
                        StarCandy[i] %= 1000;
                    }
                }
            }
        }
        else if (currencyType == "Star")
        {
            if (index >= 0 && index < Star.Length)
            {
                Star[index] += amount;
                if (amount < 0)
                {
                    // 재화가 음수가 되면 윗 배열에서 값을 빼고 음수 값을 조정
                    for (int i = index; i < Star.Length - 1 && Star[i] < 0; i++)
                    {
                        Star[i + 1] += Star[i] / 1000 - 1;
                        Star[i] = (Star[i] % 1000 + 1000) % 1000;
                    }
                }
                else
                {
                    // 재화가 양수가 되면 윗 배열로 넘어가는 부분 조정
                    for (int i = index; i < Star.Length - 1 && Star[i] >= 1000; i++)
                    {
                        Star[i + 1] += Star[i] / 1000;
                        Star[i] %= 1000;
                    }
                }
            }
        }
    }
    

    public void GetStarCandy()
    {
        for (int i = 0; i < 26; i++)
        {
            Star[i] += S_getMoney[i];
        }
    }
}
