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
        Screen.SetResolution(960, 540, false);  // ȭ�� �ػ� ����
        playerInfo = GameObject.Find("PlayerInformanager");

    }

    private void Start()
    {
        Connect();
        
    }
    public void Connect()
    {
        // ó���� Photon Online Server�� �����ϴ� �� ���� �߿�
        // Photon Online Server�� �����ϱ�

        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        print("�������ӿϷ�");
        JoinOrCreateRoom();

    }
    public void JoinOrCreateRoom()
    {
        //�� �����ϱ�.
        // �� �̸����� ���� ����
        //PhotonNetwork.JoinRoom(roomInput.text);

        // ���� ���� �ϴµ� ������ �� ����
        string sceneCode = playerInfo.GetComponent<PlayerInforManager>().SceneCode;
        PhotonNetwork.JoinOrCreateRoom(sceneCode, new RoomOptions { MaxPlayers = 10 }, null);
    }
    public override void OnJoinedRoom()
    { // �濡 ���� �� ȣ��
        print("�濡 �����մϴ�.");

        CharInstantiate();
       
    }
    public void CharInstantiate()
    {
        
        GameObject Player = PhotonNetwork.Instantiate(playerInfo.GetComponent<PlayerInforManager>().CharName, spawnPos.position, Quaternion.identity);

        // ĳ���� ���� �� ī�޶� ��� ������ ���󰡴� ������ �ذ��ϱ� ����
        // ���������� ī�޶� �ش� ���� ĳ������ ���� ��ü�� ������ �Ѵ�.
        // �ٵ� ���� ��� ����.
       /* if (Pv.IsMine)
        {/*
            CharCamera = Player.transform.GetChild(0).gameObject;
            CharCamera.transform.parent = Player.transform;
            CharCamera.transform.position = Player.transform.position + new Vector3(0, 1, 0);
            */
       // }
    }
}
