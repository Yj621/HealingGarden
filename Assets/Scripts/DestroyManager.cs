using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    private static DestroyManager instance;
    public static DestroyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DestroyManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("DestroyManager");
                    instance = obj.AddComponent<DestroyManager>();
                }
            }
            return instance;
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
// 별 부숴버리는건데.. 비활성화 안해놓고 그냥 부숴버리면 버그가 생겨서 어쩔 수 없이 파괴 스크립트 만들어버림
// 왜냐면 비활성화하면 부수는함수가 호출이 안되기에