using System;
using UnityEngine;

namespace Scirpts.Enemy
{
    public class EnemyMeleeAttack : MeleeAttack
    {
        [SerializeField] private float chaseRange = 10f;
        [SerializeField] private float moveSpeed = 3.0f;

        [SerializeField] private Transform player;
        [SerializeField] private Animator enemyAnimator;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            foreach (var friendly in EnemyManager.Instance.friendlyUnit)
            {
                float distanceToFriendly = Vector3.Distance(friendly.position, transform.position);

                if (distanceToFriendly <= attackRange && CanAttack)
                {
                    PerformAttack();
                }
                else if (distanceToFriendly <= chaseRange)
                {
                    MoveTowardsTarget(friendly.position);
                }
                else
                {
                    enemyAnimator.SetBool("IsWalking", false);
                }
            }

            PlayerAttack();
        }

        private void MoveTowardsTarget(Vector3 targetPosition)
        {
            enemyAnimator.SetBool("IsWalking", true);

            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            rb.velocity = moveDirection * (moveSpeed * Time.deltaTime);

            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        private void PlayerAttack()
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= attackRange && CanAttack)
            {
                PerformAttack();
            }
            else if (distanceToPlayer <= chaseRange)
            {
                MoveTowardsTarget(player.position);
            }
            else
            {
                enemyAnimator.SetBool("IsWalking", false);
            }
        }

        protected override void PerformAttack()
        {
            if (!CanAttack) return;

            CanAttack = false;
            enemyAnimator.SetTrigger("IsAttack");
            StartCoroutine(ResetAttackCooldown());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseRange);
        }
    }
}