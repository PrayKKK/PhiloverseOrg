using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class Sub : MonoBehaviour
{
    List<string> Subt = new List<string>();
    public string m_text;
    public Text SubtitleBox;
    public Text NameBox;
    public GameObject SubManager;
    public Transform Player;
    public int sendNumber = 0;
    AudioSource audioSource;

    private bool isTxting = false;
    //강인이가 원하는 bool값 만약에 캐릭터 각각 bool값 달라야하면 다시 말해줘
    public bool Check = true; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SubManager = GameObject.Find("SubtitleManager");
        SubtitleBox.text = "";
        NameBox.text = "";
        sendNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    
        Player = GameObject.FindWithTag("Player").transform;
        PhotonView target = Player.GetComponent<PhotonView>();
        Subt = SubManager.GetComponent<SubScript>().a;
        
        int j = 0;
        TextAsset textAsset = (TextAsset)Resources.Load("Subtitle");
        
        XmlDocument xmlDoc = new XmlDocument();
        XmlDocument xmlNameDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        xmlNameDoc.LoadXml(textAsset.text);
        //배열받고 호출후 돌리는 코드
        if (Input.GetMouseButtonDown(0) && target.IsMine && !isTxting )   // 강제 넘기기 
        {
            XmlNodeList nodes = xmlDoc.SelectNodes("SubtitleInfo/Subtitle/" + Subt[sendNumber]);
            XmlNodeList node = xmlNameDoc.SelectNodes("SubtitleInfo/Name/" + Subt[sendNumber]);
            audioSource.clip = Resources.Load("Audio/" + Subt[sendNumber]) as AudioClip;
            Debug.Log("title number="+ Subt[sendNumber]);
            sendNumber++;
            if (Subt.Count == sendNumber)
            {
                sendNumber = 0;
                SubtitleBox.text = "";
                NameBox.text = "";
                transform.gameObject.SetActive(false);
                audioSource.Stop();
            }
            NameBox.text = node[j].InnerText;
            m_text = nodes[j].InnerText;
            audioSource.Play();
            StartCoroutine(SubText());
        }
        
    }
    IEnumerator SubText()
    {
        isTxting = true;
        for (int t = 0; t <= m_text.Length; t++)
        {
            SubtitleBox.text = m_text.Substring(0, t);
            yield return new WaitForSeconds(0.07f);
        }
        isTxting = false;
    }
}
