using Scirpts.Animation;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerAttack : BaseAttack
    {
        private AnimationController _animatorController;
        private StatsManager _statsManager;
        public ParticleSystem _swordPartical;

        private void Start()
        {
            _animatorController = GetComponent<AnimationController>();
            _statsManager = GetComponent<StatsManager>();
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
            _statsManager.TakeDamage(target, _statsManager.Damage);
            StartCoroutine(ResetAttackCooldown());
        }
    }
}