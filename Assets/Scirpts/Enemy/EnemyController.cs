using UnityEngine;

namespace Scirpts.Enemy
{
    public class EnemyController : UnitControllers
    {
        private const string PlayerTag = "Player";
        private Transform _playerReference;
        public ParticleSystem _deadPartical;
        private Stats _stats;
        private Collider _collider;
        private bool hasDied = false;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        protected override void Start()
        {
            base.Start();
            _playerReference = GameObject.FindWithTag(PlayerTag)?.transform;
            _stats = GetComponent<Stats>();
            _collider = GetComponent<Collider>();
        }

        protected override void CheckStatus()
        {
            Died();
            DiedPartical();

            bool isWalking = IsChasing || (Agent.remainingDistance > 0.1f && Agent.destination != OriginalPosition);

            Animator.SetBool(IsWalking, isWalking);

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
                }
                else if (distanceToPlayer > attackRange)
                {
                    IsChasing = true;
                    FaceTarget(_playerReference);
                    Agent.isStopped = false;
                    Agent.SetDestination(_playerReference.position);
                }
                else
                {
                    IsChasing = false;
                    FaceTarget(_playerReference);
                    PerformAttack(_playerReference.gameObject);
                    Agent.SetDestination(OriginalPosition);
                }
            }
        }

        private void Died()
        {
            if (_stats.Health <= 0)
            {
                Agent.isStopped = false;
                Agent.velocity = Vector3.zero;
                IsChasing = false;
                attackRange = 0;
                chaseDistance = 0;
                _collider.isTrigger = true;
                FaceTarget(transform);
            }
        }

        private void DiedPartical()
        {
            if (_stats.Health <= 0 && !hasDied)
            {
                _deadPartical.Play();
                hasDied = true;
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