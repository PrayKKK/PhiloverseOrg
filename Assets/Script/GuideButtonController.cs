using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using System;


public class GuideButtonController : MonoBehaviour
{
    bool subG = true;
    public string CompletCheck;
    public string SubtitleName;
    public string DefaultSubtitleName;
    public GameObject Player;
    public GameObject SubtitlePanel;
    public GameObject Mark;
    public GameObject Npc;
    public GameObject InforManager;
    public GameObject SubtitleManager;

    [Space(10f)]
    public Transform portalPos;
    public GameObject portalObj;

    // Start is called before the first frame update
    void Start()
    {
        InforManager = GameObject.Find("PlayerInformanager");
        SubtitleManager = GameObject.Find("SubtitleManager");
        SubtitlePanel = GameObject.Find("StartUI/Canvas/ConversationCanvas");
        Npc = transform.parent.gameObject;
        Mark = Npc.transform.GetChild(3).GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        CompletCheck = InforManager.GetComponent<PlayerInforManager>().CharacterName;
        Player = GameObject.FindWithTag("Player");
        PhotonView target = Player.GetComponent<PhotonView>();
        // 자막 1회 실행 코드
        if (Input.GetMouseButtonDown(0)&& target.IsMine)
        {
            transform.gameObject.SetActive(false);
            Mark.SetActive(false);
            switch (CompletCheck)
            {
                case "start":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S1_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Plato":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S3_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_MachiavelliPose":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S5_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Nietzsche":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S7_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Skinner":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S9_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Plato(Clone)":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S3_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_MachiavelliPose(Clone)":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S5_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Nietzsche(Clone)":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S7_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;
                case "Txt_Skinner(Clone)":
                    if (subG = true)
                    {
                        subG = false; ;
                        SubtitlePanel.SetActive(true);
                        SubtitleName = "S9_1Start";
                        SubtitleManager.SendMessage(SubtitleName, transform.position);
                    }
                    break;



            }
        }
    }
}
