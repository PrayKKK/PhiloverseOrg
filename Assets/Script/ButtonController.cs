using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using System;


public class ButtonController : MonoBehaviour
{
    bool subG = true;
    public GameObject SubtitleManager;
    public string SubtitleName;
    public string DefaultSubtitleName;
    public GameObject Player;
    public GameObject SubtitlePanel;
    public GameObject Mark;
    public GameObject Npc;


    [Space(10f)]
    public Transform portalPos;
    public GameObject portalObj;

    // Start is called before the first frame update
    void Start()
    {
        SubtitleManager = GameObject.Find("SubtitleManager");
        SubtitlePanel = GameObject.Find("StartUI/Canvas/ConversationCanvas");
        Npc = transform.parent.gameObject;
        Mark = Npc.transform.GetChild(3).GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindWithTag("Player");
        PhotonView target = Player.GetComponent<PhotonView>();
        // 자막 1회 실행 코드
        if (Input.GetMouseButtonDown(0) && target.IsMine)
        {
            transform.gameObject.SetActive(false);
            Mark.SetActive(false);
            if (subG = true)
            {
                subG = false; ;
                SubtitlePanel.SetActive(true);
                SubtitleManager.SendMessage(SubtitleName, transform.position);
            }
        }
    }
}
