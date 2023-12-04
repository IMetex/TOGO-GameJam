using Scirpts.Animation;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerBaseAttack : BaseAttack
    {
        private AnimationController _animatorController;
        private Stats _stats;
        public ParticleSystem _swordPartical;

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

                float distanceToPlayer = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToPlayer <= attackRange && CanAttack)
                {
                    PerformAttack(enemy.gameObject);
                }
            }
        }
        
        protected override void PerformAttack(GameObject target)
        {
            if (!CanAttack) return;

            CanAttack = false;
            _animatorController.TriggerAnimation("IsAttack");
            _swordPartical.Play();
            _stats.TakeDamage(target, _stats.Damage);
            StartCoroutine(ResetAttackCooldown());
        }
    }
}