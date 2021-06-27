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
    private bool visitorStopped = false;
    private bool cheked = false;

    private void Start()
    {
        securityAgent = GetComponent<NavMeshAgent>();

        visitors = new List<GameObject>();
        visitors.AddRange(GameObject.FindGameObjectsWithTag("Visitor"));

        NextTarget();
        StartCoroutine(CheckVisitor());
    }
    private void Update()
    {
        Debug.DrawLine(securityAgent.transform.position, visitors[index].transform.position);
        print("Visitor Stopped " + visitorStopped);
    }

    private IEnumerator CheckVisitor()
    {
        while (true)
        {           
            if (securityAgent.remainingDistance < 0.8f)
            {
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
        index = Random.Range(0, visitors.Count);
        return index;
    }
    /// <summary>
    /// Попытка остановить при касании
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider && !cheked)
        {
            currentVisitor = collision.gameObject;
            BotController botController;

            if(currentVisitor != null)
            {
                print(collision.collider.name);
                botController = currentVisitor.GetComponent<BotController>();
                botController.Stopped = true;
                cheked = true;                
            }
            visitors.RemoveAt(index);
        }
    }
}
