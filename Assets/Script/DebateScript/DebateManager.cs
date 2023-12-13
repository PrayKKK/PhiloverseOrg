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

    //public GameObject DebateStartUI;    // ��� ���� �˸� UI
    public GameObject debateStartUI;

    [SerializeField]
    private GameObject Txt;             // ��� ������ �˸� UI


    // ------------- < �ý��� ���࿡ �ʿ��� ������ > -----------------

    private int playerNum;  // �÷��̾� �ο��� üũ�� ���� ����
    [SerializeField]
    private int playerStartNum = 2; // ��� ������ ������ �÷��̾� �ο��� ����
    private int quizNum;    // ���� ���� üũ�� ���� ����

    bool startUibtnCheck = true;   // ��ŸƮ ��ư Ŭ�� �� ���� �� �ֵ��� ����
    // �׽�Ʈ

    //public GameObject[] readysChr = new GameObject[5];

    // ------------- < NPC ���� ������ ���� ������ > ------------------
    public enum npcIndex    // NPC ���� ������ ���� ���� ������ ����
    {
        GREEK, Modern, Renaissance, Neoteric, Non
    }
    private npcIndex NpcIndex;   // ������ ���� NPC�� ���� �� ����
    void Start()
    {
        // ���콺 Ŀ�� ���̰� ����
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
    private void ChairCheck()   // ���� ������ �÷��̾� �ο��� üũ
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

            debateStartUI.SetActive(true); // ��� ���� UI Ȱ��ȭ (��ư�� Ŭ���ؾ߸� ��� ����)



        }
        else
        {
            debateStartUI.SetActive(false);
        }

    }
    [PunRPC]
    public void Debating()  // ��� ���� �Լ�
    {


        // ���� ������� ��� ���� ��ũ��Ʈ �ۼ�
        // ä�� ���� �Լ�, �� ���� --------------- <�Ϸ�>
        // 60�� �߾� ��� �Լ� ���� -------------- < �׽�Ʈ �ؾ��� >
        // ��� ���� ��� ����

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
            // ä�ø��� ����
            chatManager.isChat = true;
        }
        else if (!isCht)
        {
            // ä�� ���� 
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
        txt.text = " ����� �����մϴ�.";
        yield return new WaitForSeconds(3f);
        txt.text = " �־��� ������ ���� �� ������ ���� �߾��� �Ͻ� �� �ֽ��ϴ�.";
        yield return new WaitForSeconds(3f);
        txt.text = " �߾� ���� �ð��� 60�� �Դϴ�. ";
        yield return new WaitForSeconds(3f);
        txt.text = " ù �߾��� 1�� ���� �������� �����մϴ�.";


        // �߾� ��� 
        chatManager.isChat = false;

        // ���� ���� �������� (������ �÷��̾)

        //bool isPlaying = true;
        print(readysChr.Length);
        int plnum = readysChr.Length;
        int count = 1;
        while (count <= plnum)
        {
            yield return new WaitForSeconds(3f);
            txt.text = count + " �� �÷��̾� ���� �߾� ���� �ϰڽ��ϴ�.";
            yield return new WaitForSeconds(3f);
            txt.text = "";
            // �߾� �����϶� �÷��̾� �϶��� ü�� ����
            if (readysChr[(count - 1)].GetComponent<ChairCtl>().touchChar.GetComponent<PhotonView>().IsMine)
            {
                chatManager.isChat = true;
            }
            yield return new WaitForSeconds(60f);   // ������ 60������ �ӽ÷� 3�� ����
            chatManager.isChat = false;
            txt.text = " �߾� ���� �ϰڽ��ϴ�.";
            count++;
            yield return new WaitForSeconds(3f);

        }
        txt.text = " ��� �÷��̾ �߾��� ���ƽ��ϴ�.";
        yield return new WaitForSeconds(3f);
        txt.text = " �������ʹ� ���� ����Դϴ�. ";
        yield return new WaitForSeconds(3f);
        txt.text = " �÷��̾��� ����� ���� �����Ӱ� ����� NPC���� �������ּ��� ";
        yield return new WaitForSeconds(3f);
        chatManager.isChat = true;
        Destroy(txtObj);

    }
}
