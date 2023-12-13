using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using TMPro;

public class ChairCtl : MonoBehaviour
{
    //  ����� Ȱ��ȭ�� (�÷��̾ �ǵ帰)���ڸ� �����ϱ� ���� ��ũ��Ʈ

    public GameObject touchChar;   // ��ȣ�ۿ�� ĳ���� ������Ʈ ����
    public bool isTouch = false;    // ��ȣ�ۿ��� �� �������� Ȯ�� �� ����
    [SerializeField]
    private GameObject Txt;    // ������ �Ҵ��� ���ؼ� ����
    private GameObject txt;    // ������ �������� ������� ����

    public PhotonView pv;

    public Transform charDownPo;    // ĳ���Ͱ� �ɰ� �� ��ġ
    public Transform outCharPo;     // ĳ���Ͱ� ���� �� ���� ��ġ

    // --------- < ���� �ѹ� > -------
    public int ChairNum = 0;
    // --------- < isTouch�� ����� ���� ���� > --------
    public static int TouchCount = 0;   // ���� ��ȣ�ۿ� ���� ���� ������ üũ��
    private void Update()
    {
        //pv.RPC("TouchCheck", RpcTarget.All);
        TouchCheck();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
        {
            touchChar = other.gameObject;

            txt = Instantiate(Txt);
            TextSet(" ��п� ������ ���Ͻø� Ctl Ű�� �������� ");


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PhotonView>().IsMine)
        {
            touchChar = null;

            TextSet();
            Destroy(txt);
        }
    }
    public void TouchChair()
    {

        // ĳ���� ������ ����
        touchChar.GetComponent<CharMovement>().dontMove = true;


        touchChar.transform.position = charDownPo.position;
        touchChar.transform.rotation = charDownPo.rotation;

        Animator ani;
        ani = touchChar.GetComponent<Animator>();
        ani.SetBool("isChair", true);

    }
    public void ExitChair()
    {
        // ĳ���� ������ ����
        touchChar.GetComponent<CharMovement>().dontMove = false;

        touchChar.transform.position = outCharPo.position;


        // ���Ŀ� �ɴ� �ִϸ��̼� �߰��� �ּ���
        // �Ķ���� ���� ������ isChair �� ���ֽð� bool Ÿ������ ���� ���ּ���
        // ���� ������ ������ �Ǿ��ٸ� �� �ּ��� ���� �ּ���
        Animator ani;
        ani = touchChar.GetComponent<Animator>();
        ani.SetBool("isChair", false);
    }


    // �ؽ�Ʈ �Ҵ��� ���� �Լ���
    private void TextSet()
    {
        if (txt == null) return;
        txt.transform.GetChild(0).GetComponent<Text>().text = "";
    }
    private void TextSet(string input)
    {
        if (txt == null) return;
        txt.transform.GetChild(0).GetComponent<Text>().text = input;
    }
    public void TouchCheck()   // ��ȣ�ۿ� Ű �Ǵ� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isTouch)
        {
            TouchChair();   // �ɱ� �Լ�
            TextSet();
            pv.RPC("Tst", RpcTarget.All, true, 0);

        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isTouch)
        {
            ExitChair();
            TextSet();
            pv.RPC("Tst", RpcTarget.All, false, 1);
            TouchCount--;
        }



    }
    [PunRPC]
    public void Tst(bool input, int i)
    {

        isTouch = input;
        if (i == 0) TouchCount++;
        else if (i == 1) TouchCount--;



    }







    /*
    [PunRPC]
    private void TouchCheck()   // ��ȣ�ۿ� Ű �Ǵ� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isTouch)
        {
            TouchChair();   // �ɱ� �Լ�
            TextSet();
            isTouch = true;
            TouchCount++;   // TouchCount�� ��� ���ڿ��� ���� �Ѵ�.
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && isTouch)
        {
            ExitChair();
            TextSet();
            isTouch = false;
            TouchCount--;
        }

    }
    */

}
