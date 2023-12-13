using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class NpcController : MonoBehaviour
{
    public Transform Player;
    public Transform Npc;
    public Animator animator;
    public GameObject button;
    const float DIST = 2.5f;
    private Quaternion setRot;


    
    

    // Start is called before the first frame update
    void Start()
    {

        button = transform.GetChild(2).gameObject;
        setRot = Npc.rotation;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //�÷��̾� Ž��
        if(GameObject.FindGameObjectsWithTag("Player").Length != 0)
        {
            Player = GameObject.FindWithTag("Player").transform;
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //�÷��̾ ������ �Ÿ�����ؼ� �÷��̾� �ٶ󺸱�
        if(Player != null)
        {
            Vector3 delta = Player.position - transform.position;
            float dist = delta.magnitude;
            if (dist <= DIST)
            {

                Quaternion rot = Quaternion.LookRotation(delta);
                // y�ุ ȸ�� �ʿ�
                rot.x = 0f;
                rot.z = 0f;
                Npc.rotation = Quaternion.Slerp(Npc.rotation, rot, 5 * Time.deltaTime);


            }
            else if (dist > DIST)
            {
                // �÷��̾ ���� �Ÿ� ���� �� �ʱ� ���� �ٶ󺸱�
                Npc.rotation = Quaternion.Slerp(Npc.rotation, setRot, 5 * Time.deltaTime);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        button.gameObject.SetActive(true);      
    }
    private void OnTriggerExit(Collider other)
    {
        button.gameObject.SetActive(false);
    }
}
