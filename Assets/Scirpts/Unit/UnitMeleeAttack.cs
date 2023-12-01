using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Unit
{
    public class UnitMeleeAttack : MeleeAttack
    {
        private Rigidbody _rb;
        public float chaseRange = 10f;
        public float moveSpeed = 3.0f;
        public Animator unitAnimator;

        private void Start()
        {
            unitAnimator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (enemy == null) continue;

                if (distanceToPlayer <= attackRange && CanAttack)
                {
                    PerformAttack();
                }

                else if (distanceToPlayer <= chaseRange)
                {
                    Vector3 moveDirection = (enemy.position - transform.position).normalized;
                    moveDirection.y = 0f;
                    _rb.velocity = moveDirection * moveSpeed;

                    Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }


        protected override void PerformAttack()
        {
            if (!CanAttack) return;

            CanAttack = false;
            unitAnimator.SetTrigger("IsAttack");
            StartCoroutine(ResetAttackCooldown());
            unitAnimator.SetBool("IsWalking", false);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseRange);
        }
    }
}