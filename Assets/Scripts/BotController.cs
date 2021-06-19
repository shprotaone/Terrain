using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController  : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    GameObject [] target;
    [SerializeField]
    NavMeshModifierVolume navMeshVolume;
    public bool finish;
    int index = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();          
    }

    private void Update()
    {
        CheckFinish();
    }

    void NextTarget()
    {           
            index = Random.Range(0, target.Length);
            agent.destination = target[index].transform.position;

        //print("Next Target");
    }

    void CheckFinish()
    {
        if (agent.remainingDistance <= 0.1)
        {
            finish = true;
            NextTarget();
            ChangeTarget();
        }
    }

    void ChangeTarget()
    {
        navMeshVolume = target[index].GetComponentInParent<NavMeshModifierVolume>();
        print(navMeshVolume.name);

        float x = Random.Range(0, navMeshVolume.size.x);
        float z = Random.Range(0, navMeshVolume.size.y);

        if (finish)
        {
            target[index].transform.localPosition = new Vector3(x, 0, z);
            //print("Next target position " + target[index].transform.localPosition);
        }            
    }

}
