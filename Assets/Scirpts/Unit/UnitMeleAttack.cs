using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Unit
{
    public class UnitMeleAttack : MonoBehaviour
    {
        private Rigidbody rb;
        public float attackRange = 2f;
        public float chaseRange = 10f; // New variable for chase rangee
        public float attackCooldown = 0.5f;
        public float moveSpeed = 3.0f;

        private Transform player;
        public Animator enemyAnimator;
        private bool canAttack = true;
        private bool isChasing = false;

        private void Start()
        {
            enemyAnimator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            FindEnemies();
        }

        private void FindEnemies()
        {
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemyObject in enemyObjects)
            {
                EnemyManager.Instance.enemies.Add(enemyObject.transform);
            }
        }

        private void Update()
        {
            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToPlayer <= attackRange && canAttack)
                {
                    Attack();
                }

                else if (distanceToPlayer <= chaseRange)
                {
                    isChasing = true;
                    Vector3 moveDirection = (enemy.position - transform.position).normalized;
                    rb.velocity = moveDirection * moveSpeed; // Use Rigidbody velocity for movement
                    
                    
                    Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
                else
                {
                    isChasing = false;
                }
            }
        }
        
        private void Attack()
        {
            canAttack = false;
            enemyAnimator.SetTrigger("IsAttack");
            Invoke("ResetAttackCooldown", attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            canAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}