using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreekNpcAI : MonoBehaviour
{
    //NPC ��������Ʈ ����
    [SerializeField] Transform[] npcWayPoint = null;
    int Count = 0;
    //�÷��̾� ��ȣ��������� Ȱ��ȭ
    public NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ ���۰�
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(npcWayPoint[0].position);
    }
    // Update is called once per frame
    void Update()
    {
        // ��������Ʈ ���޽� 1��� �� �������� �̵�
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            Count = (Count + 1) % npcWayPoint.Length;
            navMeshAgent.SetDestination(npcWayPoint[Count].position);

        }
    }
}
