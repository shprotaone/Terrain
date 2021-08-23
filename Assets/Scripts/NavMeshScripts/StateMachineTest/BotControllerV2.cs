using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SecondBranch
{
    public class BotControllerV2 : MonoBehaviour
    {
        public StateMachine movementSM; //ссылка на машину состояний          
        public Text nameState;

        //Состояния
        public IdleState idleState;
        public WalkingState walkingState;
        public DanceState dancingState;
        public DrinkingState drinkingState;

        private int horizontalMove = Animator.StringToHash("Horizontal");
        private int vertical = Animator.StringToHash("Vertical");
        private int dancingAnim = Animator.StringToHash("Dancing");
        private int drinking = Animator.StringToHash("Drinking");
        private float forTimer;
        private bool haveABottle;

        [SerializeField] private GameObject target;
        [SerializeField] private Transform hand;
        [SerializeField] private GameObject itemInHand;

        private NavMeshAgent agent;
        private RandomPointNavMesh randomPoint;
        private Animator animator;
        private NearestObj nearestObj;

        public bool IsStopped { get; set; }
        public bool Finished { get; set; }
        public bool Wait { get; set; }
        public bool Dancing { get; set; }
        public string Zone { get; set; }

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            randomPoint = target.GetComponent<RandomPointNavMesh>();

            movementSM = new StateMachine();
            idleState = new IdleState(this,movementSM);
            walkingState = new WalkingState(this, movementSM);
            dancingState = new DanceState(this, movementSM);
            drinkingState = new DrinkingState(this, movementSM);

            movementSM.Initialize(idleState);    //инцициализация первого состояния
        }

        private void Update()
        {
            movementSM.CurrentState.Input();            //обновление текущего состояния
            movementSM.CurrentState.LogicUpdate();
            nameState.text = movementSM.CurrentState.OutputName();
            CheckDistance();
        }

        public void Move()
        {           
            if (agent.remainingDistance <= 0.1)
            {
                //animator.SetFloat(horizontalMove, agent.velocity.x);
                Finished = true;
            }
            
            animator.SetFloat(vertical, agent.velocity.magnitude);
            target.transform.position = agent.steeringTarget;
            print(agent.isStopped);
        }
        public void Stop()
        {
            agent.isStopped = true;
            print("I,m Stay man");
        }
        public void CheckDistance()
        {
            Debug.DrawLine(agent.transform.position, agent.destination, Color.red);
        }
        /// <summary>
        /// Сброс аниматора
        /// </summary>
        public void ResetMoveParams()
        {
            animator.SetFloat(horizontalMove, 0);
            animator.SetFloat(vertical, 0);
        }
        /// <summary>
        /// Таймер для ожидания
        /// </summary>
        public void Timer()
        {
            forTimer += Time.deltaTime;

            if (5 <= forTimer)
            {
                Wait = true;
                forTimer = 0;
            }            
        }
        /// <summary>
        /// Задание пути
        /// </summary>
        public void SetDestiny()
        {
            agent.SetDestination(randomPoint.ChangePointPos(true));
            print("Next");
        }
        /// <summary>
        /// Метод для танца
        /// </summary>
        /// <param name="value"></param>
        public void StartDance(bool value)
        {         
           animator.SetBool(dancingAnim, value);               
        }
        public void Drinking()
        {

        }
        public void TakeABottle()
        {
            Vector3 defaultPos = new Vector3(0.15f, 0.03f, 0.03f);
            Vector3 defaultRotation = new Vector3(-10.5f, -80f, -39.2f);

            nearestObj = GetComponent<NearestObj>();
            GameObject bottle = nearestObj.FindClosestObject();

            print("Im go to bottle");

            bottle.transform.SetParent(hand.transform);
            bottle.transform.localPosition = defaultPos;
            bottle.transform.localEulerAngles = defaultRotation;

            itemInHand = bottle;                               
        }
    }
}
