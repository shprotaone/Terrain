using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController  : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool isStopped = false;

    private NavMeshAgent agent;    
    private RandomPointNavMesh randomPointScript;

    private bool nextTarget = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomPointScript = target.GetComponentInParent<RandomPointNavMesh>();

        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {        
        while (!isStopped)
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
    /// <summary>
    /// Следующая точка и возобновление движения
    /// </summary>
    public void NextPoint()
    {
        nextTarget = true;
        agent.SetDestination(randomPointScript.ChangePointPos(nextTarget));
        nextTarget = false;
    }
    /// <summary>
    /// Остановка
    /// </summary>
    public void VisitorStop()
    {
        print("Oh no say: " + name);
        isStopped = true;
        agent.ResetPath();    
    }
}
