using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float waypointDwellTime = 0.75f;
        [Range(0, 1)] [SerializeField] float patrolSpeedFraction = 0.2f;

        [SerializeField] PatrolPath patrolPath;

        GameObject player;
        Fighter fighter;
        Mover mover;
        Health health;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedWaypoint = Mathf.Infinity;

        int currentWaypointIndex = 0;
        [SerializeField] float waypointTolerance = 2f;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer <= suspicionTime && fighter.CanAttack(player))
            {
                SuspicionBehaviour();
            }
            else if(patrolPath!=null)
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            if (AtWayPoint())
            {
                timeSinceArrivedWaypoint = 0;
                CycleWaypoint();
            }

            if (timeSinceArrivedWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(patrolPath.GetWaypointPosition(currentWaypointIndex), patrolSpeedFraction);
            }          
        }

        private bool AtWayPoint()
        {
            float distance = Vector3.Distance(transform.position, patrolPath.GetWaypointPosition(currentWaypointIndex));
            return distance < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
