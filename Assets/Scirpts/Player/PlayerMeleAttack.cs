using System;
using System.Collections.Generic;
using Scirpts.Animation;
using Scirpts.Enemy;
using Scirpts.Movement;
using UnityEditor.Animations;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerMeleAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackCooldown = 0.5f;

        private AnimationController _animatorController;
        private bool _canAttack = true;
        
        private void Start()
        {
            _animatorController = GetComponent<AnimationController>();
            FindEnemies();
        }

        private void FindEnemies()
        {
          
            GameObject[] enemyObjects =GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemyObject in enemyObjects)
            {
                EnemyManager.Instance.enemies.Add(enemyObject.transform);
            }
        }

        private void UpdateEnemies()
        {
            EnemyManager.Instance.enemies.RemoveAll(enemy => enemy == null);
        }

        private void Update()
        {
            UpdateEnemies();

            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToPlayer <= attackRange && _canAttack)
                {
                    Attack();
                }
            }
        }

        private void Attack()
        {
            _canAttack = false;
            _animatorController.TriggerAnimation("IsAttack");
            Invoke("ResetAttackCooldown", attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            _canAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                Gizmos.DrawWireSphere(enemy.position, attackRange);
            }
        }
    }
}