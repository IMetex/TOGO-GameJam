using System;
using System.Collections.Generic;
using Scirpts.Animation;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerMeleeAttack : MeleeAttack
    {
        private AnimationController _animatorController;
        private Stats _stats;

        private void Start()
        {
            _animatorController = GetComponent<AnimationController>();
            _stats = GetComponent<Stats>();
        }

        private void Update()
        {
            for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
            {
                var enemy = UnitsManager.Instance.enemies[i];
                var enemyHealth = enemy.GetComponent<Stats>()._health;

                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToPlayer <= attackRange && CanAttack)
                {
                    PerformAttack(enemy.gameObject);
                }

                if (enemyHealth <= 0)
                {
                    UnitsManager.Instance.enemies.RemoveAt(i);
                    Destroy(enemy.gameObject);
                }
            }
        }
        
        protected override void PerformAttack(GameObject target)
        {
            if (!CanAttack) return;

            CanAttack = false;
            _animatorController.TriggerAnimation("IsAttack");
            _stats.TakeDamage(target, _stats.damage);
            StartCoroutine(ResetAttackCooldown());
        }
    }
}