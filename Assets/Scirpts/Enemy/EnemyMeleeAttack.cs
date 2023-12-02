using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scirpts.Enemy
{
    public class EnemyMeleeAttack : UnitAttackBase
    {
        private const string PlayerTag = "Player";
        private Transform _playerReference;

        protected override void Start()
        {
                                     
            base.Start();
            _playerReference = GameObject.FindWithTag(PlayerTag)?.transform;
        }

        protected override void CheckStatus()
        {
            bool foundFriendlyInChaseRange = false;

            for (int i = UnitsManager.Instance.friendlyUnit.Count - 1; i >= 0; i--)
            {
                var friendly = UnitsManager.Instance.friendlyUnit[i];
                float distanceToFriendly = ReturnDistance(friendly);

                if (distanceToFriendly < chaseDistance)
                {
                    foundFriendlyInChaseRange = true;

                    if (distanceToFriendly > attackRange)
                    {
                        IsChasing = true;
                        FaceTarget(friendly);
                        Agent.SetDestination(friendly.position);
                        _isAttacking = false;
                    }
                    else
                    {
                        IsChasing = false;
                        FaceTarget(friendly);
                        PerformAttack(friendly.gameObject);
                        Agent.SetDestination(OriginalPosition);
                        _isAttacking = true;
                    }
                }
            }
            
            // Attack to player
            if (!foundFriendlyInChaseRange)
            {
                float distanceToPlayer = ReturnDistance(_playerReference);

                if (distanceToPlayer > chaseDistance)
                {
                    Agent.SetDestination(OriginalPosition);
                    IsChasing = false;
                }

                if (distanceToPlayer < chaseDistance && distanceToPlayer > attackRange)
                {
                    IsChasing = true;
                    FaceTarget(_playerReference);
                    Agent.SetDestination(_playerReference.position);
                }

                if (distanceToPlayer < chaseDistance && distanceToPlayer <= attackRange)
                {
                    IsChasing = false;
                    FaceTarget(_playerReference);
                    PerformAttack(_playerReference.gameObject);
                    Agent.SetDestination(OriginalPosition);
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRange);
        }
    }
}