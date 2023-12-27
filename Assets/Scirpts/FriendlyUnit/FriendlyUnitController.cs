using System.Linq;
using Scirpts.Enemy;
using Scirpts.Formations.Scripts;
using Scirpts.Money;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Scirpts.Unit
{
    public class FriendlyUnitController : UnitControllers
    {
        private const string PlayerFollowTag = "PlayerFollow";
        private const string PlayerRotationTag = "PlayerRotation";

        private Transform _playerFollow = null;
        private Transform _playerRotation = null;
        private bool _foundEnemyInChaseRange = false;
        
        private StatsManager _stats;
        private bool _hasDied = false;
        private bool foundEnemyInChaseRange = false;
        private Collider _collider;
        
        public ParticleSystem _deadPartical;


        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        protected override void Start()
        {
            base.Start();
            _playerFollow = GameObject.FindWithTag(PlayerFollowTag)?.transform;
            _playerRotation = GameObject.FindWithTag(PlayerRotationTag)?.transform;
            _stats = GetComponent<StatsManager>();
            _collider = GetComponent<Collider>();
        }

        private void SetFormation()
        {
            FriendlyUnitManager.Instance.points = BoxFormation.Instance.EvaluatePoints().ToList();

            for (var i = 0; i < FriendlyUnitManager.Instance.spawnedUnits.Count; i++)
            {
                var friendly = FriendlyUnitManager.Instance.spawnedUnits[i].GetComponent<UnitControllers>();
                var friendlyAnimation = friendly.GetComponent<Animator>();

                friendly.agent.SetDestination(_playerFollow.position + FriendlyUnitManager.Instance.points[i]);
                FaceTarget(_playerRotation);

                if (friendly.agent.remainingDistance <= friendly.agent.stoppingDistance && !friendly.agent.pathPending)
                {
                    friendlyAnimation.SetBool(IsWalking, false);
                }
                else
                {
                    friendlyAnimation.SetBool(IsWalking, true);
                }
            }
        }

        protected override void CheckStatus()
        {
            Died();
            DeadParticle();
            bool isWalking = isChasing;

            _animation.SetBool(IsWalking, isWalking);
            _foundEnemyInChaseRange = false;

            for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
            {
                var enemy = UnitsManager.Instance.enemies[i];
                float distanceToEnemy = ReturnDistance(enemy);

                if (distanceToEnemy < chaseDistance)
                {
                    _foundEnemyInChaseRange = true;

                    if (distanceToEnemy > attackRange)
                    {
                        isChasing = true;
                        FaceTarget(enemy);
                        agent.SetDestination(enemy.position);
                    }
                    else
                    {
                        isChasing = false;
                        FaceTarget(enemy);
                        PerformAttack(enemy.gameObject);
                    }
                }
            }

            if (!_foundEnemyInChaseRange)
            {
                SetFormation();
                isChasing = false;
            }
        }

        private void Died()
        {
            if (_stats.Health <= 0)
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                attackRange = 0;
                chaseDistance = 0;
                transform.rotation = Quaternion.identity;
                _collider.isTrigger = true;
                isChasing = false;
            }
        }

        private void DeadParticle()
        {
            if (_stats.Health <= 0 && !_hasDied)
            {
                _deadPartical.Play();
                _hasDied = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRange);
        }
    }
}