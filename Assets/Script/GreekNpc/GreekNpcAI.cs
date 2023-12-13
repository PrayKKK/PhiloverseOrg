using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreekNpcAI : MonoBehaviour
{
    //NPC 웨이포인트 지정
    [SerializeField] Transform[] npcWayPoint = null;
    int Count = 0;
    //플레이어 상호장용했을시 활성화
    public NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트 시작값
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(npcWayPoint[0].position);
    }
    // Update is called once per frame
    void Update()
    {
        // 웨이포인트 도달시 1상승 후 다음으로 이동
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            Count = (Count + 1) % npcWayPoint.Length;
            navMeshAgent.SetDestination(npcWayPoint[Count].position);

        }
    }
}
