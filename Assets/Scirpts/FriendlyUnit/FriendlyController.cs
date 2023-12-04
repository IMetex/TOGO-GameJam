using System.Linq;
using Scirpts.Enemy;
using Scirpts.Formations.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Scirpts.Unit
{
    public class FriendlyController : UnitControllers
    {
        private const string PlayerFollowTag = "PlayerFollow";
        private const string PlayerRotationTag = "PlayerRotation";

        public ParticleSystem _deadPartical;
        private Transform _playerFollow;
        private Transform _playerRotation;
        private Stats _stats;
        private bool hasDied = false;
        private bool foundEnemyInChaseRange = false;
        private Collider _collider;

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        protected override void Start()
        {
            base.Start();
            _playerFollow = GameObject.FindWithTag(PlayerFollowTag)?.transform;
            _playerRotation = GameObject.FindWithTag(PlayerRotationTag)?.transform;
            _stats = GetComponent<Stats>();
            _collider = GetComponent<Collider>();
        }

        private void SetFormation()
        {
            
            FriendlyUnitManager.Instance.points = BoxFormation.Instance.EvaluatePoints().ToList();
            for (var i = 0; i < FriendlyUnitManager.Instance.spawnedUnits.Count; i++)
            {
                var friendly = FriendlyUnitManager.Instance.spawnedUnits[i].GetComponent<UnitControllers>();
                var friendlyAnimation = friendly.GetComponent<Animator>();
                
                friendly.Agent.SetDestination(_playerFollow.position + FriendlyUnitManager.Instance.points[i]);
                FaceTarget(_playerRotation);

                if (friendly.Agent.remainingDistance <= friendly.Agent.stoppingDistance && !friendly.Agent.pathPending)
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
            DiedPartical();
            bool isWalking = IsChasing;

            Animator.SetBool(IsWalking, isWalking);

            foundEnemyInChaseRange = false;

            for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
            {
                var enemy = UnitsManager.Instance.enemies[i];
                float distanceToEnemy = ReturnDistance(enemy);

                if (distanceToEnemy < chaseDistance)
                {
                    foundEnemyInChaseRange = true;

                    if (distanceToEnemy > attackRange)
                    {
                        IsChasing = true;
                        FaceTarget(enemy);
                        Agent.SetDestination(enemy.position);
                    }
                    else
                    {
                        IsChasing = false;
                        FaceTarget(enemy);
                        PerformAttack(enemy.gameObject);
                    }
                }
            }

            if (!foundEnemyInChaseRange)
            {
                SetFormation();
                IsChasing = false;
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
                foundEnemyInChaseRange = true;
                FaceTarget(transform);
                _collider.isTrigger = true;
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, chaseDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRange);
        }
    }
}