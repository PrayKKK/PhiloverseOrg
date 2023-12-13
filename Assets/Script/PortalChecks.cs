using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChecks : MonoBehaviour
{
    public PlayerInforManager inFor;
    // --- 해당 오브젝트 고유 넘버 ---
    public int ObjNum = 0;  // 가고자 하는 씬의 번호를 넣어주세요 (예: Greek 1번)
    [SerializeField]
    private GameObject Object;

    private void Start()
    {
        inFor = GameObject.Find("PlayerInformanager").GetComponent<PlayerInforManager>();
        NumberCheckSettings();
    }
    public void NumberCheckSettings()
    {
        bool[] pcheck = inFor.portalChecks;
        for(int i = 0; i< pcheck.Length; i++)
        {
            if( !pcheck[i])
            {
                if(i == (ObjNum -1))
                {
                    Object.SetActive(true);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
