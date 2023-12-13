using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClear : MonoBehaviour
{
    // ���� Ŭ���� �ϰ��� �ϴ� ��Ż ������Ʈ���ٰ� �߰� ���ּ���

    public int SceneClearNum = 0; // 1���� ���� �Դϴ�. (�� : Greek 1��)
    public PlayerInforManager inFor;

    private void Awake()
    {
        inFor = GameObject.Find("PlayerInformanager").GetComponent<PlayerInforManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        inFor.portalChecks[(SceneClearNum - 1)] = true;
    }
}
