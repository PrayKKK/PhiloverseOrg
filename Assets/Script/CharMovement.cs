using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using Cinemachine;


public class CharMovement : MonoBehaviour
{

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private Camera cm;
    public string MainCameraName;
    [Space(10f)]
    [SerializeField]
    private Rigidbody rigid;
    

    [SerializeField]
    private GameObject CamConfiner; // 카메라 제한 구역 설정이지만 부자연스러운 연출로 아직 미사용
    

    public PhotonView Pv;

    //  -- < 캐릭터 움직임에 대한 변수 선언 > ---

    public bool dontMove = false;   // 캐릭터 동적 여부 ( true 일경우 움직임 잠금 )

    public float speed = 5f;
    public float runSpeed = 3f;
    [Space(20f)]
    [Range(0f, 1000f)]
    public float jumpPw = 50f;
    private bool isrun = false;
    private float isMovingSp;
    [SerializeField]
    private bool isJump = false;

    private Vector3 moveDir;

    // animation
    public Animator playerMoveAni; // 모델의 애니메이터 가져오기
    Vector2 inpBut;

    CinemachineFreeLook Camfollow;
    //public Rigidbody chrRigid;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
         
    }
    void Start()
    {
       
        if (Pv.IsMine)
        {
            cm = GameObject.Find(MainCameraName).GetComponent<Camera>();
            Camfollow = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
            
            Camfollow.Follow = characterBody;
            Camfollow.LookAt = cameraArm;
           // CamConfiner.GetComponent<CinemachineConfiner>().m_BoundingVolume = GameObject.Find("CamCollider").GetComponent<BoxCollider>();

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pv.IsMine) return;
        Movement(dontMove);
        //LookAround();
        //Pv.RPC("PlayerAniRpc", RpcTarget.AllBuffered);
        
        Pv.RPC("PlayerAni", RpcTarget.AllBuffered);

    }
    private void FixedUpdate()
    {
        JumpCtl(dontMove);
    }
    void Movement(bool dontMove)
    {

        if (Input.GetMouseButton(1))
        {
            Camfollow.m_XAxis.m_MaxSpeed = 300f;
            Camfollow.m_YAxis.m_MaxSpeed = 2f;
        }
        else
        {
            Camfollow.m_XAxis.m_MaxSpeed = 0f;
            Camfollow.m_YAxis.m_MaxSpeed = 0f;
        }

        if (dontMove == true) return;
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
       

        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        */
        
   

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        isrun = Input.GetButton("Run");
        isMovingSp = 0;
        if(isMove)
        {
            
            cameraArm.transform.rotation = cm.transform.rotation;
            // 달리기 구별 
            
            if(isrun)
            {
                isMovingSp = speed + runSpeed;
            }
            else { isMovingSp = speed; }

            Vector3 lookForward = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.transform.right.x, 0f, cameraArm.transform.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * isMovingSp;
        }
    }
    private void JumpCtl(bool dontMove)
    {
        if ((dontMove == true || isJump ) || !Input.GetKey(KeyCode.Space)) return;

        rigid.AddForce(transform.up * jumpPw);
        isJump = true;
    }
    private void LookAround()
    {   // cinemachine 사용으로 사용 안함
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if(x< 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x,270f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
   
    [PunRPC]
    void PlayerAniRpc()
    {
        inpBut = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        

        playerMoveAni.SetFloat("isMoveY", inpBut.y);
        playerMoveAni.SetFloat("isMoveX", inpBut.x);

       // print(playerMoveAni.GetInteger("isMoveV"));
       // print(playerMoveAni.GetInteger("isMoveH"));
    }

    [PunRPC]
    void PlayerAni()
    {
        inpBut = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerMoveAni.SetFloat("y", inpBut.y);
        playerMoveAni.SetFloat("x", inpBut.x);
        playerMoveAni.SetFloat("MoveSpeed", isMovingSp);
       // print(isMovingSp);
       // print(inpBut);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground") return;
        isJump = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Ground") return;
        isJump = false;
    }
    
}
