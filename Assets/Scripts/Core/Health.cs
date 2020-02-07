using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100;

        bool isDead = false;

        float tempHealth;

        public void Start()
        {
            tempHealth = healthPoints;
        }

        public void Update()
        {
            tempHealth = healthPoints;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints-damage, 0);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}