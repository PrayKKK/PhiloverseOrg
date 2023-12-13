using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PortalManagement : MonoBehaviour
{
    public string Scenename; // ��������κ�
    public string CharName; // ��������κ�
    //public NetworkManager nwm;
    public PhotonView Pv;
   
    public enum PotalIndex
    {
        MuseumScene, GREEK, Modern, Renaissance, Neoteric, DebateScene
    }
    public enum TagIndex
    {
        Player, Test
    }

    [SerializeField]
    private PotalIndex MoveScene = PotalIndex.MuseumScene;
    [SerializeField]
    private TagIndex TargetTag = TagIndex.Player;

    [Space(20)]
    public Canvas SpawnCv;
    private Canvas spawnCv;

    [Space(20)]
    public GameObject InforManager;
    private void Start()
    {
        InforManager = GameObject.Find("PlayerInformanager");
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Pv = collision.gameObject.GetComponent<PhotonView>();
        spawnCv =  Instantiate(SpawnCv);
        spawnCv.GetComponent<SceneMoveUi>().Pm = gameObject.GetComponent<PortalManagement>();
        
        if (collision.gameObject.tag == TagCall() && Pv.IsMine)
        {
            spawnCv = Instantiate(SpawnCv);
            spawnCv.GetComponent<SceneMoveUi>().Pm = gameObject.GetComponent<PortalManagement>();

        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        
        if (InforManager.GetComponent<PlayerInforManager>().clearinfo != PlayerInforManager.ClearInfo.Non)
        { DebatePortal(); }
        if (MoveScene == PotalIndex.DebateScene) return;
        Pv = other.gameObject.GetComponent<PhotonView>();
        if (other.gameObject.tag == TagCall() && Pv.IsMine)
        {
            Scenename = SceneManager.GetActiveScene().name; // ��������κ�
            InforManager.SendMessage("CompletScene", Scenename); // ��������κ�
            if(GameObject.FindWithTag("MainNpc") != null)
            {
                CharName = GameObject.FindWithTag("MainNpc").name; // ��������κ�
            }
            
            InforManager.SendMessage("CompletScene2", CharName); // ��������κ�
            spawnCv = Instantiate(SpawnCv);
            spawnCv.GetComponent<SceneMoveUi>().Pm = gameObject.GetComponent<PortalManagement>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (MoveScene == PotalIndex.DebateScene) return;
        Pv = other.gameObject.GetComponent<PhotonView>();
        if (other.gameObject.tag == TagCall() && Pv.IsMine)
        {
            spawnCv.GetComponent<SceneMoveUi>().DestroySelf();
        }
    }
    private void PotalManager()     // ��Ż �ε����� ���� �ϸ� ������ �̵� �մϴ�.
    {
        
        switch (MoveScene)
        {
            case PotalIndex.MuseumScene: MoveScenes("MuseumScene"); break;
            case PotalIndex.GREEK: MoveScenes("GREEK"); break;
            case PotalIndex.Modern: MoveScenes("Modern"); break;
            case PotalIndex.Renaissance: MoveScenes("Renaissance"); break;
            case PotalIndex.Neoteric: MoveScenes("Neoteric"); break;
            case PotalIndex.DebateScene: MoveScenes("DebateScene"); break;

            default: break;

        }
    }
    private string TagCall()    // �̵��� ��ü�� �±� �̸��� ��ȯ�մϴ�.
    {
        switch (TargetTag)
        {
            case TagIndex.Player: return "Player";
            case TagIndex.Test: return "Test";
            default: break;
        }
        return "Null";
    }
    public string MoveSceneNameCall()   // �̵��� ���� �̸��� ��ȯ�մϴ�.
    {
        switch (MoveScene)
        {
            case PotalIndex.MuseumScene: return "MuseumScene"; break;
            case PotalIndex.GREEK: return "GREEK"; break;
            case PotalIndex.Modern: return "Modern"; break;
            case PotalIndex.Renaissance: return "Renaissance"; break;
            case PotalIndex.Neoteric: return "Neoteric"; break;
            case PotalIndex.DebateScene: return "DebateScene"; break;
            default: break;

        }
        return null;
    }
    public void MoveScenes(string name) // �������ϰ� �̵��մϴ�.
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(name);
    }
    private void DebatePortal()
    {
        // ����� ��Ż �� ���� �Է��� ���� ȣ��
        if (MoveScene != PotalIndex.DebateScene) return;
        
        // ���� �� �ڵ忡�� ���������� �̵��ϱ� ���� �� �̸� �߰�.
        InforManager.GetComponent<PlayerInforManager>().SceneCode += MoveSceneNameCall();
        Scenename = SceneManager.GetActiveScene().name; // ��������κ�
        InforManager.SendMessage("CompletScene", Scenename); // ��������κ�
        PotalManager();


    }
    
}
