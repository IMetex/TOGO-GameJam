using Scirpts.Money;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scirpts.Enemy
{
    public class EnemyController : UnitControllers
    {
        private Transform _playerReference;
        private const string PlayerTag = "Player";

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private bool _foundFriendlyInChaseRange = false;
        public ParticleSystem _deadPartical;
        private StatsManager _stats;
        private Collider _collider;
        private bool _hasDied = false;

        protected override void Start()
        {
            base.Start();
            _playerReference = GameObject.FindWithTag(PlayerTag)?.transform;
            _stats = GetComponent<StatsManager>();
            _collider = GetComponent<Collider>();
        }

        protected override void CheckStatus()
        {
            Died();
            DiedPartical();

            bool isWalking = isChasing || (agent.remainingDistance > 0.1f && agent.destination != OriginalPosition);
            _animation.SetBool(IsWalking, isWalking);
            _foundFriendlyInChaseRange = false;

            for (int i = UnitsManager.Instance.friendlyUnit.Count - 1; i >= 0; i--)
            {
                var friendly = UnitsManager.Instance.friendlyUnit[i];
                if (friendly == null) return;

                float distanceToFriendly = ReturnDistance(friendly);

                if (distanceToFriendly < chaseDistance)
                {
                    _foundFriendlyInChaseRange = true;

                    if (distanceToFriendly > attackRange)
                    {
                        isChasing = true;
                        FaceTarget(friendly);
                        agent.SetDestination(friendly.position);
                        agent.isStopped = false;
                    }
                    else
                    {
                        isChasing = false;
                        FaceTarget(friendly);
                        PerformAttack(friendly.gameObject);
                        agent.SetDestination(OriginalPosition);
                    }
                }
            }

            if (!_foundFriendlyInChaseRange)
            {
                float distanceToPlayer = ReturnDistance(_playerReference);

                if (distanceToPlayer > chaseDistance)
                {
                    agent.SetDestination(OriginalPosition);
                    isChasing = false;
                }
                else if (distanceToPlayer > attackRange)
                {
                    isChasing = true;
                    FaceTarget(_playerReference);
                    agent.isStopped = false;
                    agent.SetDestination(_playerReference.position);
                }
                else
                {
                    isChasing = false;
                    FaceTarget(_playerReference);
                    PerformAttack(_playerReference.gameObject);
                    agent.SetDestination(OriginalPosition);
                }
            }
        }

        private void Died()
        {
            if (_stats.Health <= 0)
            {
                agent.isStopped = false;
                agent.velocity = Vector3.zero;
                isChasing = false;
                attackRange = 0;
                chaseDistance = 0;
                _collider.isTrigger = true;
                FaceTarget(transform);
            }
        }

        private void DiedPartical()
        {
            if (_stats.Health <= 0 && !_hasDied)
            {
                _deadPartical.Play();
                _hasDied = true;
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