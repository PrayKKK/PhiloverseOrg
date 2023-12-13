using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInforManager : MonoBehaviour
{
    public string SceneName; // ��������κ�
    public string CharacterName; // ��������κ�

    public string SceneCode = "";
    public string CharName = "";

    [SerializeField]
    private GameObject LoginCanvas;

    [Space(20f)]
    public bool[] portalChecks;
    
    // Start is called before the first frame update
    private void Awake()
    {
       
        SceneName = "start";
        CharacterName = "start";
        var obj = FindObjectsOfType<PlayerInforManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            LoginCanvas.SetActive(true);
            print("Test Start Scene");
        }
        else 
        {
            LoginCanvas.SetActive(false);
            print("Test destroy");
            Destroy(gameObject); 

        }

        _clearInfo = ClearInfo.GREEK;
        CharNameMaker();
        
        

    }

    

    // ���������� Ŭ������ �� ������ ��� ������
    public enum ClearInfo
    {
        GREEK, Modern, Renaissance, Neoteric, Non
    }
    private ClearInfo _clearInfo;
    public ClearInfo clearinfo
    {
        get { return _clearInfo; }
        set { _clearInfo = value; }
    }
    private void CharNameMaker()
    {
        int randChar = Random.Range(1, 7);
        CharName = "MainChar" + randChar;
    }
    // ��������κ�
    public void CompletScene(string Scenename)
    {
        SceneName = Scenename;
    }
    public void CompletScene2(string CharName)
    {
        CharacterName = CharName;
    }
}
