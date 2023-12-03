using Scirpts.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Scirpts.Unit
{
    public class FriendlyMeleeAttack : UnitAttackBase
    {
        private const string PlayerFollowTag = "PlayerFollow";
        private const string PlayerRotationTag = "PlayerRotation";

        private Transform _playerFollow;
        private Transform _playerRotation;

        private FormationBase _formation;

        public FormationBase Formation
        {
            get
            {
                if (_formation == null) _formation = GetComponent<FormationBase>();
                return _formation;
            }
            set => _formation = value;
        }

        protected override void Start()
        {
            base.Start();
            _playerFollow = GameObject.FindWithTag(PlayerFollowTag)?.transform;
            _playerRotation = GameObject.FindWithTag(PlayerRotationTag)?.transform;
        }

        private void SetFormation()
        {
            foreach (Vector3 point in FriendlyUnitManager.Instance.points)
            {
                Agent.SetDestination(_playerFollow.position + point);
                FaceTarget(_playerRotation);
            }

            IsChasing = !(Agent.remainingDistance < 0.1f);
        }

        protected override void CheckStatus()
        {
            bool foundEnemyInChaseRange = false;

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
                        _isAttacking = false;
                    }
                    else
                    {
                        IsChasing = false;
                        FaceTarget(enemy);
                        PerformAttack(enemy.gameObject);
                        _isAttacking = true;
                    }
                }
            }

            if (!foundEnemyInChaseRange)
            {
                SetFormation();
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