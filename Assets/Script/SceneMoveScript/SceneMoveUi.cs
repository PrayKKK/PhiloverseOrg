using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneMoveUi : MonoBehaviour
{
    public PortalManagement Pm; // PortalManagement ���� �ڵ����� �Ҵ����� ( �� ����UI�� �ν��Ͻ�ȭ �Ǿ������� )


    public Text text;
    [SerializeField]
    private InputField serverNum;

    public string TestServerNum = "TestServerNum";
    
    private void Start()
    {
        // �ߺ� ���� �ѹ�
        
        // pm�� ���� �̵��� ���� �̸� ȣ�� �Լ� �Դϴ�.
        text.text = Pm.MoveSceneNameCall() + " �� �̵��մϴ�.";
    }
    public void MoveScene()
    {
        Pm.InforManager.GetComponent<PlayerInforManager>().SceneCode = TestServerNum; // �ӽ÷� ������ ���� ���� inputField�� ��ȯ�Ұ�!
        Pm.MoveScenes(Pm.MoveSceneNameCall());
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }





}
