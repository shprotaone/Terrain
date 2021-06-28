using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private List<GameObject> visitors;
    [SerializeField] private GameObject currentVisitor;

    private NavMeshAgent securityAgent; 
    
    private int index;
    private int visitorsCount;

    private bool visitorStopped = false;
    private bool cheked = false;

    private void Awake()
    {        
        securityAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {        
        visitors = new List<GameObject>();
        visitors.AddRange(GameObject.FindGameObjectsWithTag("Visitor"));
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
            bool checkNextTarget = securityAgent.remainingDistance < 0.83f;
            print("Условия следующей цели " + checkNextTarget);

            if (securityAgent.remainingDistance < 0.83f && securityAgent.pathPending)
            {
                yield return new WaitForSeconds(5f);
                NextTarget();                
            }
            
            securityAgent.SetDestination(visitors[index].transform.position);
            
            yield return null;
        }
    }

    /// <summary>
    /// Генерация номера следующего моба
    /// </summary>
    /// <returns></returns>
    private int NextTarget()
    {
        visitorsCount = visitors.Count;
        index = Random.Range(0, visitorsCount);
        cheked = true;
        return index;       
    }
    /// <summary>
    /// Попытка остановить при касании
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        BotController botController;        
        currentVisitor = collision.gameObject;
       
        if (currentVisitor != null && currentVisitor.name == visitors[index].name)  //2nd
        {
            print(collision.collider.name);
            botController = currentVisitor.GetComponent<BotController>();
            botController.Stopped = true;
            cheked = true;
            visitors.RemoveAt(index);
        }        
    }
}
