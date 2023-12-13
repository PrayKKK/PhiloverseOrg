using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public GameObject m_Content;
    //public TMP_InputField m_inputField;
    public InputField m_inputField;
    public bool isChat = true;  // 채팅이 가능한 상태인지 확인

    PhotonView photonview;

    GameObject m_ContentText;

    string m_strUserName;


    void Start()
    {

        //PhotonNetwork.ConnectUsingSettings();
        m_ContentText = m_Content.transform.GetChild(0).gameObject;
        photonview = GetComponent<PhotonView>();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Return) && m_inputField.isFocused == false)
        {
            m_inputField.ActivateInputField();
        }
        */
        OnEndEditEvent(isChat);
    }
    public override void OnConnectedToMaster()
    {/*
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 5;

        int nRandomKey = Random.Range(0, 100);

        m_strUserName = "user" + nRandomKey;

        PhotonNetwork.LocalPlayer.NickName = m_strUserName;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
        */
    }

    public override void OnJoinedRoom()
    {
        AddChatMessage("connect user : " + PhotonNetwork.LocalPlayer.NickName);
    }

    public void OnEndEditEvent(bool isChat)
    {
        if (!isChat) return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            string strMessage = m_strUserName + " : " + m_inputField.text;

            photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
            m_inputField.text = "";
        }
    }

    void AddChatMessage(string message)
    {
        GameObject goText = Instantiate(m_ContentText, m_Content.transform);

        goText.GetComponent<Text>().text = message;
        m_Content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

    }

    [PunRPC]
    void RPC_Chat(string message)
    {
        AddChatMessage(message);
    }

}
