using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using TMPro;

public class DebateManager : MonoBehaviour
{
    private PlayerInforManager info;
    private ChatManager chatManager;

    public PhotonView pv;

    public GameObject[] npc;
    public Transform npcSpawnPo;

    //public GameObject DebateStartUI;    // 토론 시작 알림 UI
    public GameObject debateStartUI;

    [SerializeField]
    private GameObject Txt;             // 토론 진행을 알릴 UI


    // ------------- < 시스템 진행에 필요한 변수들 > -----------------

    private int playerNum;  // 플레이어 인원수 체크를 위해 선언
    [SerializeField]
    private int playerStartNum = 2; // 토론 진행이 가능한 플레이어 인원수 설정
    private int quizNum;    // 퀴즈 개수 체크를 위해 선언

    bool startUibtnCheck = true;   // 스타트 버튼 클릭 시 없앨 수 있도록 설정
    // 테스트

    //public GameObject[] readysChr = new GameObject[5];

    // ------------- < NPC 스폰 구별을 위한 변수들 > ------------------
    public enum npcIndex    // NPC 구별 스폰을 위한 씬별 열거형 정의
    {
        GREEK, Modern, Renaissance, Neoteric, Non
    }
    private npcIndex NpcIndex;   // 설정된 씬의 NPC가 스폰 할 예정
    void Start()
    {
        // 마우스 커서 보이게 고정
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        info = GameObject.Find("PlayerInformanager").GetComponent<PlayerInforManager>();
        
        NPCspawner();
    }

    public void NPCspawner()
    {
        ScenesNPC();
        switch (NpcIndex)
        {
            case npcIndex.GREEK: Instantiate(npc[0], npcSpawnPo.position, Quaternion.identity); break;
            case npcIndex.Modern: Instantiate(npc[1], npcSpawnPo.position, Quaternion.identity); break;
            case npcIndex.Renaissance: Instantiate(npc[2], npcSpawnPo.position, Quaternion.identity); break;
            case npcIndex.Neoteric: Instantiate(npc[3], npcSpawnPo.position, Quaternion.identity); break;
            case npcIndex.Non: break;
            default: break;


        }
    }
    private void ScenesNPC()
    {
        switch(info.SceneName)
        {
            case "GREEK": NpcIndex = npcIndex.GREEK; break;
            case "Modern": NpcIndex = npcIndex.Modern; break;
            case "Renaissance": NpcIndex = npcIndex.Renaissance; break;
            case "Neoteric": NpcIndex = npcIndex.Neoteric; break;
            default: break;

        }
    }

    private void Update()
    {
        ChairCheck();
    }

    /*
    IEnumerator ChairCheck()
    {
        int num = 0;
        GameObject[] chairs;
        chairs = GameObject.FindGameObjectsWithTag("Chair");

        for (int i = 0; i < chairs.Length; i++)
        {
            if (chairs[i].GetComponent<ChairCtl>().isTouch) num ++;

        }
        playerNum = num;

        yield return new WaitForSeconds(2.5f);
    }
    */
    private void ChairCheck()   // 현재 착석한 플레이어 인원수 체크
    {
        int num = 0;
        GameObject[] chairs;
        chairs = GameObject.FindGameObjectsWithTag("Chair");

        for (int i = 0; i < chairs.Length; i++)
        {
            if (chairs[i].GetComponent<ChairCtl>().isTouch) num++;

        }
        playerNum = num;
        DebateStartCheck(startUibtnCheck);
    }
    private void DebateStartCheck(bool ischeck)
    {


        if (playerNum >= playerStartNum && ischeck)
        {

            debateStartUI.SetActive(true); // 토론 진행 UI 활성화 (버튼을 클릭해야만 토론 진행)



        }
        else
        {
            debateStartUI.SetActive(false);
        }

    }
    [PunRPC]
    public void Debating()  // 토론 진행 함수
    {


        // 이하 내용들은 토론 진행 스크립트 작성
        // 채팅 막기 함수, 등 제작 --------------- <완료>
        // 60초 발언 기능 함수 제작 -------------- < 테스트 해야함 >
        // 답안 제출 기능 제작

        pv.RPC("TesttingRPCStart", RpcTarget.All);
       
    }
    [PunRPC]
    public void TesttingRPCStart()
    {
        StartCoroutine(ChatWaitCtl());
        startUibtnCheck = false;
    }

    public void ChatCtl(bool isCht)
    {
        if (isCht)
        {
            // 채팅막기 해제
            chatManager.isChat = true;
        }
        else if (!isCht)
        {
            // 채팅 막기 
            chatManager.isChat = false;
        }
    }
    IEnumerator ChatWaitCtl()
    {

        GameObject[] chairs;

        int checknm = 0;
        chairs = GameObject.FindGameObjectsWithTag("Chair");
        int n = chairs.Length;
        GameObject[] readysChr = new GameObject[n];
        print(chairs.Length);

        for (int i = 0; i < (chairs.Length); i++)
        {
            if (chairs[i].GetComponent<ChairCtl>().isTouch)
            {
                print("Test checknm : " + checknm);
                print("Test chairs num : " + chairs.Length);
                print("Test chairs obj : " + chairs[i]);

                readysChr[checknm] = chairs[i];
                print("Test ReadysChr Length : " + readysChr.Length);
                checknm++;
            }

        }
        GameObject txtObj = Instantiate(Txt);
        Text txt = txtObj.transform.GetChild(0).GetComponent<Text>();




        yield return new WaitForSeconds(3f);
        txt.text = " 토론을 시작합니다.";
        yield return new WaitForSeconds(3f);
        txt.text = " 주어진 문제에 대해 각 참가자 마다 발언을 하실 수 있습니다.";
        yield return new WaitForSeconds(3f);
        txt.text = " 발언 제한 시간은 60초 입니다. ";
        yield return new WaitForSeconds(3f);
        txt.text = " 첫 발언은 1번 의자 순서부터 시작합니다.";


        // 발언 잠금 
        chatManager.isChat = false;

        // 의자 정보 가져오기 (덤으로 플레이어도)

        //bool isPlaying = true;
        print(readysChr.Length);
        int plnum = readysChr.Length;
        int count = 1;
        while (count <= plnum)
        {
            yield return new WaitForSeconds(3f);
            txt.text = count + " 번 플레이어 부터 발언 시작 하겠습니다.";
            yield return new WaitForSeconds(3f);
            txt.text = "";
            // 발언 차례일때 플레이어 일때만 체팅 해제
            if (readysChr[(count - 1)].GetComponent<ChairCtl>().touchChar.GetComponent<PhotonView>().IsMine)
            {
                chatManager.isChat = true;
            }
            yield return new WaitForSeconds(60f);   // 원래는 60초지만 임시로 3초 설정
            chatManager.isChat = false;
            txt.text = " 발언 종료 하겠습니다.";
            count++;
            yield return new WaitForSeconds(3f);

        }
        txt.text = " 모든 플레이어가 발언을 바쳤습니다.";
        yield return new WaitForSeconds(3f);
        txt.text = " 이제부터는 자유 토론입니다. ";
        yield return new WaitForSeconds(3f);
        txt.text = " 플레이어들과 토론을 통해 자유롭게 결과를 NPC에게 제출해주세요 ";
        yield return new WaitForSeconds(3f);
        chatManager.isChat = true;
        Destroy(txtObj);

    }
}
