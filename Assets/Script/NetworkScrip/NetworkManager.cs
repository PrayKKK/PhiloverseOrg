using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks // ��� �ޱ� ���� Photon.Pun �� Photon.Reltime �� using �ؾ��Ѵ�.
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
        Screen.SetResolution(960, 540, false);  // ȭ�� �ػ� ����
        plInfo = GameObject.Find("PlayerInformanager").GetComponent<PlayerInforManager>();
        
    }

    private void Update()
    {
       // statusText.text = PhotonNetwork.NetworkClientState.ToString(); // ���� � �������� ȣ�� 

    }

    public void Connect()
    {
        // ó���� Photon Online Server�� �����ϴ� �� ���� �߿�
        // Photon Online Server�� �����ϱ�

        PhotonNetwork.ConnectUsingSettings();
    }

    // Photon Online Server�� �����ϸ� �Ҹ��� �ݹ� �Լ�
    // PhotonNetwork.ConnectUsingSettings()�� �����ϸ� �Ҹ�
    public override void OnConnectedToMaster()
    {
        print("�������ӿϷ�");
        // �÷��̾� �г��� ����
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        // JoinLobby();
        JoinOrCreateRoom();

        
    }
    public void JoinLobby()
    {
        //�κ� �����ϱ�
        PhotonNetwork.JoinLobby();
        print("�κ� ����");
    }

    public void Disconnect()
    {
        // ���� ����.
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("�������");
        base.OnDisconnected(cause);
    }

    public void JoinOrCreateRoom()
    {
        //�� �����ϱ�.
        // �� �̸����� ���� ����
        //PhotonNetwork.JoinRoom(roomInput.text);

        // ���� ���� �ϴµ� ������ �� ����
        int radRoomNum = Random.Range(roomMin, roomMax);
        PhotonNetwork.JoinOrCreateRoom(radRoomNum.ToString(), new RoomOptions { MaxPlayers = 10 }, null);
    }
    public void LeaveRoom()
    {
        //�� ������
        PhotonNetwork.LeaveRoom();
    }


    //------------------------- override -----------------------------------

    public override void OnCreatedRoom()
    {
        print("���� ���� ���� �����մϴ�.");
    }
    public override void OnJoinedRoom()
    { // �濡 ���� �� ȣ��
        print("�濡 �����մϴ�.");

        CharInstantiate();
        StartUIOut();

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("�游��� ����");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("�� ���� ����");
    }


    public void CharInstantiate()
    {
        
        GameObject Player = PhotonNetwork.Instantiate(plInfo.CharName, spawnPos.position, Quaternion.identity);

        // ĳ���� ���� �� ī�޶� ��� ������ ���󰡴� ������ �ذ��ϱ� ����
        // ���������� ī�޶� �ش� ���� ĳ������ ���� ��ü�� ������ �Ѵ�.
        // �ٵ� ���� ��� ����.
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
