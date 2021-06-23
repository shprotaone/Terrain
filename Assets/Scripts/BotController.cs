using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController  : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    GameObject target;
    RandomPointNavMesh randomPointScript;

    bool nextTarget = false;
    bool isMoving;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        randomPointScript = target.GetComponentInParent<RandomPointNavMesh>();
        StartCoroutine(Movement());
    }
    void Update()
    {
        agent.destination = target.transform.position;
        //print(agent.remainingDistance);
    }

    void NextPoint()
    {
        print("NextTarget");
        nextTarget = true;
        randomPointScript.ChangePointPos(nextTarget);
        nextTarget = false;
    }

    IEnumerator Movement()
    {
        isMoving = true;

        while (isMoving)
        {
            if (agent.remainingDistance < 0.1)
            {
                //print("Agent pos: " + agent.transform.position);
                //print("Target pos: " + target.transform.position);
                Debug.LogError("Im Found!");
                yield return new WaitForSeconds(5f);
                NextPoint();
            }
            else Debug.Log("So Far");

            yield return null;
        }
    }

}
