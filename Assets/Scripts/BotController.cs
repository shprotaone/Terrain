using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController  : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject localTarget;
    private NavMeshAgent agent;    
    private RandomPointNavMesh randomPointScript;

    private bool nextTarget = false;
    [SerializeField] private bool isStopped = false;

    public bool Stopped
    {
        get
        {
            return isStopped;
        }
        set
        {
            isStopped = value;          
        }
    }    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomPointScript = target.GetComponentInParent<RandomPointNavMesh>();

        StartCoroutine(Movement());
    }

    private void Update()
    {
        VisitorStop();
    }

    private IEnumerator Movement()
    {        
        while (true)
        {
            Debug.DrawLine(agent.transform.position, agent.destination,Color.red);
            if (agent.remainingDistance <= 0.01)
            {
                float waitingTime = Random.Range(5, 15);
                yield return new WaitForSeconds(waitingTime);

                if (!agent.pathPending && !isStopped)
                {
                    NextPoint();
                }
            }         
            yield return null;
        }              
    }
    private void NextPoint()
    {
        nextTarget = true;
        agent.SetDestination(randomPointScript.ChangePointPos(nextTarget));
        nextTarget = false;
    }

    private void VisitorStop()
    {
        if (isStopped)
        {
            print("Oh no" + name);
        }
    }

}
