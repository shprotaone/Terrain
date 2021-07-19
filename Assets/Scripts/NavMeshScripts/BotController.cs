using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController  : MonoBehaviour
{
    private const float remainingDistance = 0.3f;
    private const string walkingNameStates = "Walking";
    private const string idleNameStates = "Idle";
    private const string drinkingNameStates = "Drinking";
    private const string dancingNameStates = "Dancing";

    [SerializeField] private GameObject target;
    [SerializeField] private bool isStopped = false;
    [SerializeField] private bool isDanced = false;
    [SerializeField] private bool inMove = false;
    [SerializeField] private NavMeshModifierVolume navMeshVolume;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject itemInHand;

    private NavMeshAgent agent;    
    private RandomPointNavMesh randomPointScript;
    private NearestObj nearestObj;
    private Animator animator;

    private float pickDistance = 0.2f;          //дистанция для взятия бутылки.
    private bool nextTarget = false;
    private bool haveABottle;
    private bool drinkingBottle = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomPointScript = target.GetComponentInParent<RandomPointNavMesh>();
        animator = GetComponent<Animator>();
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        
        //isStopped = true;
        while (true)
        {            
            Debug.DrawLine(agent.transform.position, agent.destination,Color.red);

            if (agent.remainingDistance <= remainingDistance)
            {
                float waitingTime = Random.Range(10, 20);
                isStopped = true;
                animator.SetBool(walkingNameStates, false);

                yield return new WaitForSeconds(waitingTime);
                
                if (!agent.pathPending && isStopped)
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
        isDanced = false;
        isStopped = false;
        inMove = true;
        agent.SetDestination(randomPointScript.ChangePointPos(nextTarget));

        animator.SetBool(dancingNameStates, false);
        animator.SetBool(walkingNameStates, true);
        
        nextTarget = false;
    }
    /// <summary>
    /// Остановка
    /// </summary>
    public void VisitorStop(Transform target)
    {  
        this.transform.LookAt(target);
        isStopped = true;
        isDanced = false;

        animator.SetBool(dancingNameStates, false);
        animator.SetBool(walkingNameStates, false);
        animator.SetBool(idleNameStates, true);

        agent.ResetPath();        
    }
    /// <summary>
    /// Взять бутылку если находишься в определнной зоне
    /// </summary>
    public void TakeABottle()
    {
        if (isStopped == true && !haveABottle)
        {
            nearestObj = GetComponent<NearestObj>();
            GameObject bottle = nearestObj.FindClosestObject();

            print("Im go to bottle");

            agent.SetDestination(bottle.transform.position);
            animator.SetBool(walkingNameStates, true);

            if (agent.remainingDistance <= pickDistance)
            {
                bottle.transform.SetParent(hand.transform);
                bottle.transform.position = hand.transform.position;
                bottle.transform.rotation = hand.transform.rotation;

                itemInHand = bottle;
                agent.ResetPath();
                isStopped = true;
                animator.SetBool(walkingNameStates, false);
                Invoke("NextPoint", 5);
                haveABottle = true;
            }
            else
            {
                animator.SetBool(idleNameStates, true);
            }
        }
    }
    /// <summary>
    /// Начать пить, если есть бутылка
    /// </summary>
    public void Drinking()
    { 
        if (haveABottle && Random.value < 0.35&& isStopped)
        {
            animator.SetBool(drinkingNameStates, false);
            Destroy(itemInHand, animator.GetCurrentAnimatorStateInfo(0).length);
        }

        NextPoint();

    }
    /// <summary>
    /// Начать танцевать, если находишься в определенной зоне. 
    /// </summary>
    public void DanceStarted()
    {
        float chance = Random.value;
        isDanced = true;

        if (isDanced && isStopped)
        {
            if (Random.value > 0.35)
            {
                animator.SetBool(dancingNameStates, true);
            }           
        }
        else
        {
            animator.SetBool(dancingNameStates, false);
            animator.SetBool(idleNameStates, true);
        }
    }
}
