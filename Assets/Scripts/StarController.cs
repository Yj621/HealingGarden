using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    void Update()
    {
        // Star�� ���� ��ġ�� üũ
        if (transform.position.y <= 0f)
        {
            // ��ġ ����
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

            // Rigidbody�� kinematic���� �����Ͽ� �� �̻� ���� ������ ���� �������� �ʵ��� ��
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // ���� ������ ��Ȱ��ȭ�Ͽ� �߰����� ������ ������ ���� �ʵ��� ����
            }
        }
    }
}
// �� �ڵ带 �����ϸ� ������ Star�� Ư�� y��ǥ (0f)���� ��������� ������ Collider����
// isTrigger�� On���� �س��� ���ٴ����� �հ� ���������� �ʰ�, ���鳢�� ���ļ� �� �̸����� ���������� ����
