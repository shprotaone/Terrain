using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private List<BotController> visitors;
    [SerializeField] private float distanceStop = 1f;
    const float waitTime = 10f;     //уточнить, на том ли месте()

    private NavMeshAgent securityAgent;    
    private int index;

    private void Start()
    {
        securityAgent = GetComponent<NavMeshAgent>();

        visitors = new List<BotController>();
        visitors.AddRange(FindObjectsOfType<BotController>());

        NextTarget();
        StartCoroutine(CheckVisitor());
    }
    private void Update()
    {
        Debug.DrawLine(securityAgent.transform.position, visitors[index].transform.position);
    }

    private IEnumerator CheckVisitor()
    {
        while (true)
        {           
            if (securityAgent.remainingDistance < distanceStop && securityAgent.hasPath)
            {
                visitors[index].VisitorStop();
                securityAgent.ResetPath();

                yield return new WaitForSeconds(waitTime);

                visitors[index].NextPoint();
                NextTarget();                
            }
            securityAgent.SetDestination(visitors[index].transform.position);
            
            yield return null;
        }
    }
    /// <summary>
    /// Генерация номера следующего посетителя
    /// </summary>
    /// <returns></returns>
    private int NextTarget()
    {
        index = Random.Range(0, visitors.Count);
        return index;
    }
}
