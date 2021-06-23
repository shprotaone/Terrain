using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    NavMeshAgent securityAgent;
    [SerializeField]
    GameObject [] targets;

    bool finish;
    int index;

    void Start()
    {
        securityAgent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Visitor");
        NextTarget();
    }
    private void Update()
    {
        RefreshPosition();
        CheckFinish();
    }

    int NextTarget()
    {
        index = Random.Range(0, targets.Length);
        print("Target number Change");
        return index;
        
    }
    //НАДО НАЙТИ КАК ОБНОВЛЯТЬ ПОЗИЦИЮ
    void CheckFinish()
    {
        if (securityAgent.remainingDistance < 0.9f)
        {
            finish = true;
            print("NextTarget");
            NextTarget();
        }
    }
    void RefreshPosition()
    {        
        securityAgent.destination = targets[index].transform.position;
        //if (!finish)
        //{
        //    currentPosition = targets[index].transform.position;
        //}
    }
}
