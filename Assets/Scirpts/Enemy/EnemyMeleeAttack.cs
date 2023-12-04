using UnityEngine;

namespace Scirpts.Enemy
{
    public class EnemyMeleeAttack : UnitAttackBase
    {
        private const string PlayerTag = "Player";
        private Transform _playerReference;
        private Stats _stats;

        protected override void Start()
        {
            base.Start();
            _stats = GetComponent<Stats>();
            _playerReference = GameObject.FindWithTag(PlayerTag)?.transform;
        }
        
        protected override void CheckStatus()
        {
            bool foundFriendlyInChaseRange = false;

            for (int i = UnitsManager.Instance.friendlyUnit.Count - 1; i >= 0; i--)
            {
                var friendly = UnitsManager.Instance.friendlyUnit[i];
                if (friendly == null) return;
                
                float distanceToFriendly = ReturnDistance(friendly);

                if (distanceToFriendly < chaseDistance)
                {
                    foundFriendlyInChaseRange = true;

                    if (distanceToFriendly > attackRange)
                    {
                        IsChasing = true;
                        FaceTarget(friendly);
                        Agent.SetDestination(friendly.position);
                        Agent.isStopped = false;
                    }
                    else
                    {
                        IsChasing = false;
                        FaceTarget(friendly);
                        PerformAttack(friendly.gameObject);
                        Agent.SetDestination(OriginalPosition);
                    }
                }
            }

            if (!foundFriendlyInChaseRange)
            {
                float distanceToPlayer = ReturnDistance(_playerReference);
                
                if (distanceToPlayer > chaseDistance)
                {
                    Agent.SetDestination(OriginalPosition);
                    IsChasing = false;
                    chaseDistance = 10;
                }
                else if (distanceToPlayer > attackRange)
                {
                    IsChasing = true;
                    FaceTarget(_playerReference);
                    Agent.isStopped = false;
                    Agent.SetDestination(_playerReference.position);
                    chaseDistance = 4;
                }
                else
                {
                    IsChasing = false;
                    FaceTarget(_playerReference);
                    PerformAttack(_playerReference.gameObject);
                    Agent.SetDestination(OriginalPosition);
                    chaseDistance = 10;
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