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
    public string Scenename; // 준희수정부분
    public string CharName; // 준희수정부분
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
            Scenename = SceneManager.GetActiveScene().name; // 준희수정부분
            InforManager.SendMessage("CompletScene", Scenename); // 준희수정부분
            if(GameObject.FindWithTag("MainNpc") != null)
            {
                CharName = GameObject.FindWithTag("MainNpc").name; // 준희수정부분
            }
            
            InforManager.SendMessage("CompletScene2", CharName); // 준희수정부분
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
    private void PotalManager()     // 포탈 인덱스를 구별 하며 씬으로 이동 합니다.
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
    private string TagCall()    // 이동할 객체의 태그 이름을 반환합니다.
    {
        switch (TargetTag)
        {
            case TagIndex.Player: return "Player";
            case TagIndex.Test: return "Test";
            default: break;
        }
        return "Null";
    }
    public string MoveSceneNameCall()   // 이동할 씬의 이름을 반환합니다.
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
    public void MoveScenes(string name) // 광범위하게 이동합니다.
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(name);
    }
    private void DebatePortal()
    {
        // 토론장 포탈 시 정보 입력을 위해 호출
        if (MoveScene != PotalIndex.DebateScene) return;
        
        // 현재 씬 코드에서 개별적으로 이동하기 위해 씬 이름 추가.
        InforManager.GetComponent<PlayerInforManager>().SceneCode += MoveSceneNameCall();
        Scenename = SceneManager.GetActiveScene().name; // 준희수정부분
        InforManager.SendMessage("CompletScene", Scenename); // 준희수정부분
        PotalManager();


    }
    
}
