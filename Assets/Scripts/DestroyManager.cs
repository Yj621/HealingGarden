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
// �� �ν������°ǵ�.. ��Ȱ��ȭ ���س��� �׳� �ν������� ���װ� ���ܼ� ��¿ �� ���� �ı� ��ũ��Ʈ ��������
// �ֳĸ� ��Ȱ��ȭ�ϸ� �μ����Լ��� ȣ���� �ȵǱ⿡