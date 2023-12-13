using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneMoveUi : MonoBehaviour
{
    public PortalManagement Pm; // PortalManagement 에서 자동으로 할당해줌 ( 단 스폰UI가 인스턴스화 되었을때만 )


    public Text text;
    [SerializeField]
    private InputField serverNum;

    public string TestServerNum = "TestServerNum";
    
    private void Start()
    {
        // 중복 방지 넘버
        
        // pm의 현재 이동할 씬의 이름 호출 함수 입니다.
        text.text = Pm.MoveSceneNameCall() + " 로 이동합니다.";
    }
    public void MoveScene()
    {
        Pm.InforManager.GetComponent<PlayerInforManager>().SceneCode = TestServerNum; // 임시로 방제목 고정 추후 inputField로 전환할것!
        Pm.MoveScenes(Pm.MoveSceneNameCall());
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }





}
