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
    [SerializeField] private NavMeshModifierVolume navMeshVolume;
    [SerializeField] private Transform hand;

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
        while (!isStopped)
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
                    isStopped = false;
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
        StopAnimation();        //задать вопрос

        nextTarget = true;
        agent.SetDestination(randomPointScript.ChangePointPos(nextTarget));
        animator.SetBool(walkingNameStates, true);
        nextTarget = false;
    }
    /// <summary>
    /// Остановка
    /// </summary>
    public void VisitorStop(Transform target)
    {
        //StopAnimation();
        animator.SetBool(dancingNameStates, false);
        animator.SetBool(idleNameStates, true);

        this.transform.LookAt(target);
        isStopped = true;
        isDanced = false;

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

                agent.ResetPath();
                isStopped = false;
                animator.SetBool(walkingNameStates, false);
                Invoke("NextPoint",5);                
                haveABottle = true;
            }

            if (drinkingBottle)
            {
                Destroy(bottle);
            }
        }       
    }
    /// <summary>
    /// Начать пить, если есть бутылка
    /// </summary>
    public void Drinking()
    { 
        if (haveABottle && Random.value < 0.35)
        {
            animator.SetTrigger(drinkingNameStates);
            print("I,m Drink!");
        }
    }
    /// <summary>
    /// Начать танцевать, если находишься в определенной зоне. 
    /// </summary>
    public void DanceStarted()
    {
        float chance = Random.value;
        print("isDanced " + isDanced);

        if (chance > 0.35 && !isDanced)
        {
            if (isStopped)
            {
                isDanced = true;
                animator.SetBool(dancingNameStates,true);                  
            }
            else
            {
                isDanced = false;
                animator.SetBool(dancingNameStates, false);
            }
        }        
    }
    private void StopAnimation()
    {
        animator.SetBool(dancingNameStates, false);
        animator.SetBool(walkingNameStates, false);
        animator.SetBool(idleNameStates, true);
    }

}
