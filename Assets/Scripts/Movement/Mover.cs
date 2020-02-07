using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    [DisallowMultipleComponent]
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        Health health;
        [SerializeField] float maxSpeed = 4.8f;
        
        private void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            GetComponent<NavMeshAgent>().enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
         }

        public void StartMoveAction(Vector3 position, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(position, speedFraction);
        }

        public void MoveTo(Vector3 position, float speedFraction)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.SetDestination(position);
        }
    }
}
