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
        private int drinkingAnim = Animator.StringToHash("Drinking");
        private float forTimer;

        [SerializeField] private GameObject randomSystem;
        [SerializeField] private Transform hand;
        [SerializeField] private GameObject itemInHand;
        [SerializeField] private Transform destination;

        private NavMeshAgent agent;
        private RandomPointNavMesh randomPoint;
        private Animator animator;
        private NearestObj nearestObj;

        #region Properties
        public bool IsStopped { get; set; }
        public bool SecurityCheck { get; set; }
        public bool Finished { get; set; }
        public bool Wait { get; set; }
        public bool Dancing { get; set; }
        public bool HaveABottle { get; set; }
        public bool Danced { get; set; }
        public string Zone { get; set; }
        #endregion

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            randomPoint = randomSystem.GetComponent<RandomPointNavMesh>();

            movementSM = new StateMachine();
            idleState = new IdleState(this,movementSM);
            walkingState = new WalkingState(this, movementSM);
            dancingState = new DanceState(this, movementSM);
            drinkingState = new DrinkingState(this, movementSM);

            movementSM.Initialize(idleState);    //инцициализация первого состояния
        }

        private void Update()
        {         
            movementSM.CurrentState.LogicUpdate();
            nameState.text = movementSM.CurrentState.OutputName();
            CheckDistance();
        }
        /// <summary>
        /// Метод для обновления анимации перемещения
        /// </summary>
        public void Move()
        {
            if (agent.remainingDistance <= 0.1)
            {               
                Finished = true;
            }          
            animator.SetFloat(vertical, agent.velocity.magnitude);     
        }
        /// <summary>
        /// Отрисовка пути для дебага
        /// </summary>
        public void CheckDistance()
        {
            Debug.DrawLine(agent.transform.position, agent.destination, Color.red);
        }
        /// <summary>
        /// Сброс аниматора
        /// </summary>
        public void ResetMoveParams()
        {
            animator.SetFloat(vertical, 0);
            animator.SetFloat(horizontalMove, 0);
        }
        /// <summary>
/// Время ожидания
/// </summary>
/// <param name="timeToWait">время, которое нужно ждать</param>
        public void Timer(float timeToWait)
        {
            forTimer += Time.deltaTime;

            if (timeToWait <= forTimer)
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
            destination.transform.position = randomPoint.ChangePointPos(true);
            agent.SetDestination(destination.transform.position);
            print("Next");
        }
        /// <summary>
        /// проигрывание анимации для танца
        /// </summary>
        /// <param name="value"></param>
        public void StartDance(bool value)
        {         
           animator.SetBool(dancingAnim, value);               
        }
        /// <summary>
        /// Проигрывание анимации питья
        /// </summary>
        /// <param name="value"></param>
        public void Drinking(bool value)
        {
            animator.SetBool(drinkingAnim, value);
        }
        /// <summary>
        /// Взять бутылку
        /// </summary>
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
            HaveABottle = true;
        }
        /// <summary>
        /// Выключение бутылки после использования
        /// </summary>
        public void DestroyBottle()
        {
            itemInHand.SetActive(false);
        }
        /// <summary>
        /// Выдает время текущей анимации
        /// </summary>
        /// <param name="nameOfClip">Имя анимации</param>
        /// <returns></returns>
        public float AnimationLenght(string nameOfClip)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            float time = 0;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name == nameOfClip)
                {
                    time = clip.length;                    
                }
            }
            return time;            
        }
        /// <summary>
        /// Остановка охранником
        /// </summary>
        /// <param name="transform"></param>
        public void SecurityStop(Transform transform)
        {
            this.transform.LookAt(transform);
            agent.ResetPath();
            SecurityCheck = true;
            movementSM.ChangeState(idleState);            
        }
    }
}
