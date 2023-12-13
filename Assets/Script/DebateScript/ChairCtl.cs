using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;
using TMPro;

public class ChairCtl : MonoBehaviour
{
    //  토론장 활성화된 (플레이어가 건드린)의자를 구별하기 위한 스크립트

    public GameObject touchChar;   // 상호작용된 캐릭터 오브젝트 저장
    public bool isTouch = false;    // 상호작용이 된 상태인지 확인 불 변수
    [SerializeField]
    private GameObject Txt;    // 프리팹 할당을 위해서 선언
    private GameObject txt;    // 스폰된 프리팹을 담기위해 선언

    public PhotonView pv;

    public Transform charDownPo;    // 캐릭터가 앉게 될 위치
    public Transform outCharPo;     // 캐릭터가 나올 때 있을 위치

    // --------- < 의자 넘버 > -------
    public int ChairNum = 0;
    // --------- < isTouch를 대신할 정적 변수 > --------
    public static int TouchCount = 0;   // 현재 상호작용 중인 의자 갯수를 체크함
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
            TextSet(" 토론에 참석을 원하시면 Ctl 키를 누르세요 ");


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

        // 캐릭터 움직임 고정
        touchChar.GetComponent<CharMovement>().dontMove = true;


        touchChar.transform.position = charDownPo.position;
        touchChar.transform.rotation = charDownPo.rotation;

        Animator ani;
        ani = touchChar.GetComponent<Animator>();
        ani.SetBool("isChair", true);

    }
    public void ExitChair()
    {
        // 캐릭터 움직임 해제
        touchChar.GetComponent<CharMovement>().dontMove = false;

        touchChar.transform.position = outCharPo.position;


        // 추후에 앉는 애니메이션 추가해 주세요
        // 파라메터 네임 설정은 isChair 로 해주시고 bool 타입으로 생성 해주세요
        // 위의 내용이 수정이 되었다면 이 주석을 지워 주세요
        Animator ani;
        ani = touchChar.GetComponent<Animator>();
        ani.SetBool("isChair", false);
    }


    // 텍스트 할당을 위한 함수들
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
    public void TouchCheck()   // 상호작용 키 판단 함수
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isTouch)
        {
            TouchChair();   // 앉기 함수
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
    private void TouchCheck()   // 상호작용 키 판단 함수
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isTouch)
        {
            TouchChair();   // 앉기 함수
            TextSet();
            isTouch = true;
            TouchCount++;   // TouchCount는 모든 의자에서 공유 한다.
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
