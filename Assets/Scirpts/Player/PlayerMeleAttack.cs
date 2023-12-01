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

        private void Start()
        {
            _animatorController = GetComponent<AnimationController>();
        }

        private void Update()
        {
            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToPlayer <= attackRange && CanAttack)
                {
                    PerformAttack();
                }
            }
        }

        protected override void PerformAttack()
        {
            if (!CanAttack) return;
            
            CanAttack = false;
            _animatorController.TriggerAnimation("IsAttack");
            StartCoroutine(ResetAttackCooldown());
        }
    }
}