using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PortalNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerInfo;

    public PhotonView Pv;
    [Space(20f)]
    [SerializeField]
    private Transform spawnPos;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);  // 화면 해상도 고정
        playerInfo = GameObject.Find("PlayerInformanager");

    }

    private void Start()
    {
        Connect();
        
    }
    public void Connect()
    {
        // 처음에 Photon Online Server에 접속하는 게 가장 중요
        // Photon Online Server에 접속하기

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        JoinOrCreateRoom();

    }
    public void JoinOrCreateRoom()
    {
        //방 참가하기.
        // 방 이름으로 입장 가능
        //PhotonNetwork.JoinRoom(roomInput.text);

        // 방을 참가 하는데 없으면 방 생성
        string sceneCode = playerInfo.GetComponent<PlayerInforManager>().SceneCode;
        PhotonNetwork.JoinOrCreateRoom(sceneCode, new RoomOptions { MaxPlayers = 10 }, null);
    }
    public override void OnJoinedRoom()
    { // 방에 입장 시 호출
        print("방에 입장합니다.");

        CharInstantiate();
       
    }
    public void CharInstantiate()
    {
        
        GameObject Player = PhotonNetwork.Instantiate(playerInfo.GetComponent<PlayerInforManager>().CharName, spawnPos.position, Quaternion.identity);

        // 캐릭터 생성 시 카메라가 상대 유저를 따라가는 오류를 해결하기 위해
        // 직접적으로 카메라를 해당 유저 캐릭터의 하위 객체로 설정을 한다.
        // 근데 지금 사용 안함.
       /* if (Pv.IsMine)
        {/*
            CharCamera = Player.transform.GetChild(0).gameObject;
            CharCamera.transform.parent = Player.transform;
            CharCamera.transform.position = Player.transform.position + new Vector3(0, 1, 0);
            */
       // }
    }
}
