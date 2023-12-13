using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChecks : MonoBehaviour
{
    public PlayerInforManager inFor;
    // --- �ش� ������Ʈ ���� �ѹ� ---
    public int ObjNum = 0;  // ������ �ϴ� ���� ��ȣ�� �־��ּ��� (��: Greek 1��)
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
