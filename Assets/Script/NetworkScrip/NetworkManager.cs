using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks // 상속 받기 위해 Photon.Pun 과 Photon.Reltime 을 using 해야한다.
{
    public Text statusText;
    public InputField roomInput, NickNameInput;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private GameObject startUi;
    [SerializeField]
    private GameObject CharCamera;

    [SerializeField]
    private int roomMin = 1;
    [SerializeField]
    private int roomMax = 2;

    public PhotonView Pv;

    public PlayerInforManager plInfo;

    private void Awake()
    {
        Screen.SetResolution(960, 540, false);  // 화면 해상도 고정
        plInfo = GameObject.Find("PlayerInformanager").GetComponent<PlayerInforManager>();
        
    }

    private void Update()
    {
       // statusText.text = PhotonNetwork.NetworkClientState.ToString(); // 현재 어떤 상태인지 호출 

    }

    public void Connect()
    {
        // 처음에 Photon Online Server에 접속하는 게 가장 중요
        // Photon Online Server에 접속하기

        PhotonNetwork.ConnectUsingSettings();
    }

    // Photon Online Server에 접속하면 불리는 콜백 함수
    // PhotonNetwork.ConnectUsingSettings()가 성공하면 불림
    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        // 플레이어 닉네임 설정
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        // JoinLobby();
        JoinOrCreateRoom();

        
    }
    public void JoinLobby()
    {
        //로비에 접속하기
        PhotonNetwork.JoinLobby();
        print("로비 참가");
    }

    public void Disconnect()
    {
        // 연결 끊기.
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("연결끊김");
        base.OnDisconnected(cause);
    }

    public void JoinOrCreateRoom()
    {
        //방 참가하기.
        // 방 이름으로 입장 가능
        //PhotonNetwork.JoinRoom(roomInput.text);

        // 방을 참가 하는데 없으면 방 생성
        int radRoomNum = Random.Range(roomMin, roomMax);
        PhotonNetwork.JoinOrCreateRoom(radRoomNum.ToString(), new RoomOptions { MaxPlayers = 10 }, null);
    }
    public void LeaveRoom()
    {
        //방 떠나기
        PhotonNetwork.LeaveRoom();
    }


    //------------------------- override -----------------------------------

    public override void OnCreatedRoom()
    {
        print("기존 방이 없어 생성합니다.");
    }
    public override void OnJoinedRoom()
    { // 방에 입장 시 호출
        print("방에 입장합니다.");

        CharInstantiate();
        StartUIOut();

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("방만들기 실패");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("방 입장 실패");
    }


    public void CharInstantiate()
    {
        
        GameObject Player = PhotonNetwork.Instantiate(plInfo.CharName, spawnPos.position, Quaternion.identity);

        // 캐릭터 생성 시 카메라가 상대 유저를 따라가는 오류를 해결하기 위해
        // 직접적으로 카메라를 해당 유저 캐릭터의 하위 객체로 설정을 한다.
        // 근데 지금 사용 안함.
        if (Pv.IsMine)
        {
            CharCamera = Player.transform.GetChild(0).gameObject;
            CharCamera.transform.parent = Player.transform;
            CharCamera.transform.position = Player.transform.position + new Vector3(0, 1, 0);
        }
    }
    public void StartUIOut()
    {
        startUi.SetActive(false);
    }
}
