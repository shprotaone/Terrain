using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SecondBranch
{


    public class SecurityController : MonoBehaviour
    {
        private const float waitTime = 10f;

        [SerializeField] private List<BotControllerV2> visitors;
        [SerializeField] private float distanceStop = 1f;

        private NavMeshAgent securityAgent;
        private Animator animator;
        private int index;

        private void Start()
        {
            securityAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            visitors = new List<BotControllerV2>();
            visitors.AddRange(FindObjectsOfType<BotControllerV2>());

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
                    visitors[index].SecurityStop(securityAgent.gameObject.transform);
                    securityAgent.ResetPath();
                    this.transform.LookAt(visitors[index].transform);

                    animator.SetBool("Idle", true);
                    animator.SetBool("Walking", false);

                    yield return new WaitForSeconds(waitTime);

                    animator.SetBool("Idle", false);

                    NextTarget();                  
                }

                animator.SetBool("Walking", true);
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
}
