using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClear : MonoBehaviour
{
    // 씬을 클리어 하고자 하는 포탈 오브젝트에다가 추가 해주세요

    public int SceneClearNum = 0; // 1부터 시작 입니다. (예 : Greek 1번)
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
