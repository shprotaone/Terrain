using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SecondBranch
{
    public class BotControllerV2 : MonoBehaviour
    {
        public StateMachine animSM;
        public IdleState idleState;
        public WalkingState walkingState;

        public int idleParam => Animator.StringToHash("Idle");
        public int walkParam => Animator.StringToHash("Walking");

        [SerializeField] private GameObject target;

        private NavMeshAgent agent;
        private RandomPointNavMesh randomPoint;
        private Animator animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            randomPoint = target.GetComponent<RandomPointNavMesh>();

            animSM = new StateMachine();
            idleState = new IdleState(this,animSM);
            walkingState = new WalkingState(this, animSM);


            animSM.Initialize(walkingState);
        }

        private void Update()
        {
            animSM.CurrentState.Input();

            animSM.CurrentState.LogicUpdate();
        }

        public void Move()
        {
            agent.SetDestination(randomPoint.ChangePointPos(true));
            print("GO GO GO");
        }

        public void Stop()
        {
            agent.ResetPath();
            print("I,m Stay man");
        }

        public void CheckDistance()
        {
            Debug.DrawLine(agent.transform.position, agent.destination, Color.red);

            if (agent.remainingDistance <= 0.3f)
            {
                Stop();
                print("I,m Here");
            }
        }

        public void SetAnimationBool(int param, bool value)
        {
            animator.SetBool(param, value);
        }
    }
}
