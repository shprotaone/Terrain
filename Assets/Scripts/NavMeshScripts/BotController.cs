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
 
    [SerializeField] private bool isStopped = false;
    [SerializeField] private NavMeshModifierVolume navMeshVolume;
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject itemInHand;
    [SerializeField] private GameObject target;

    private NavMeshAgent agent;    
    private RandomPointNavMesh randomPointScript;
    private NearestObj nearestObj;
    private Animator animator;
    private Rigidbody rigid;
    
    private float waitingTime;
    private bool nextTarget = false;
    private bool startDrink = false;
    private bool startDance = false;
    private bool takeABottle = false;
    private bool haveABottle = false;
    private string currentArea;

    public GameObject destination;

    #region Properties
    public string Zone
    {
        get { return currentArea; }
        set { currentArea = value; }
    }
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomPointScript = target.GetComponentInParent<RandomPointNavMesh>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        StopAllCoroutines();
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {   
        while (true)
        {            
            if (agent.remainingDistance <= remainingDistance)
            {
                
                waitingTime = Random.Range(5,20);
                //isStopped = true;
                animator.SetBool(walkingNameStates, false);

                yield return new WaitForSeconds(2);               
                isStoppedMeth();
                ReadArea();

                if (startDance) yield return StartCoroutine(DanceStarted());
                if (startDrink) yield return StartCoroutine(Drinking());
                if (takeABottle) yield return StartCoroutine(TakeABottle());

                yield return new WaitForSeconds(waitingTime);
                yield return StartCoroutine(NextPoint());
                
                yield return null;               
            }

            #region Debug
            Debug.DrawLine(agent.transform.position, agent.destination, Color.red);
            #endregion
            
            
            yield return null;
        }              
    }
    /// <summary>
    /// Следующая точка и возобновление движения
    /// </summary>
    public IEnumerator NextPoint()
    {
        nextTarget = true;
        destination.transform.position = randomPointScript.ChangePointPos(nextTarget);
        agent.SetDestination(destination.transform.position);

        animator.SetBool(dancingNameStates, false);
        animator.SetBool(walkingNameStates, true);
        
        nextTarget = false;

        yield return null;
    }
    /// <summary>
    /// Остановка
    /// </summary>
    public void VisitorStop(Transform target)
    {  
        this.transform.LookAt(target);

        animator.SetBool(dancingNameStates, false);
        animator.SetBool(walkingNameStates, false);
        animator.SetBool(idleNameStates, true);
        
        agent.ResetPath();        
    }
    /// <summary>
    /// Взять бутылку если находишься в определнной зоне
    /// </summary>
    public IEnumerator TakeABottle()
    {
        Vector3 defaultPos = new Vector3(0.15f, 0.03f, 0.03f);
        Vector3 defaultRotation = new Vector3(-10.5f, -80f, -39.2f);

        if (isStopped && !haveABottle)
        {
            nearestObj = GetComponent<NearestObj>();
            GameObject bottle = nearestObj.FindClosestObject();

            print("Im go to bottle");
            agent.ResetPath();
            if (agent.remainingDistance <= 0.2f)
            {
                animator.SetBool(walkingNameStates, false);

                bottle.transform.SetParent(hand.transform);
                bottle.transform.localPosition = defaultPos;
                bottle.transform.localEulerAngles = defaultRotation;

                itemInHand = bottle;
                haveABottle = true;

                yield return null;
            }
                  
        }
    }
    /// <summary>
    /// Начать пить, если есть бутылка
    /// </summary>
    public IEnumerator Drinking()
    {
        if (haveABottle && isStopped)
        {
            float waitForDrinking = 7;
            animator.SetBool(drinkingNameStates, true);
            yield return new WaitForSeconds(waitForDrinking);
            animator.SetBool(drinkingNameStates, false);
            animator.SetBool(idleNameStates, true);
            
            itemInHand.SetActive(false);
            haveABottle = false;
        }
        yield return null;
    }
    /// <summary>
    /// Начать танцевать, если находишься в определенной зоне. 
    /// </summary>
    public IEnumerator DanceStarted()
    {   
        float chance = Random.value;
        float lenght = 7;

        animator.SetBool(dancingNameStates, true);

        yield return new WaitForSeconds(lenght);

        animator.SetBool(dancingNameStates, false);
        startDance = false;

        yield return null;
    }
    /// <summary>
    /// Передает информацию о текущей зоне
    /// </summary>
    public void ReadArea()
    {
        print(Zone);
        if (isStopped)
        {
            switch (Zone)
            {
                case "DanceNav":
                    startDance = true;
                    break;
                case "LaungeNav":
                    startDrink = true;
                    break;
                case "BarNav":
                    takeABottle = true;
                    break;

                default:
                    print("Walkble");
                    startDance = false;
                    startDrink = false;
                    takeABottle = false;
                    break;
            }
        }
    }

    public void isStoppedMeth()
    {
        print(isStopped);

        if (rigid.IsSleeping())
        {
            isStopped = true;
        }
        else
        {
            isStopped = false;
        }
    }
}
