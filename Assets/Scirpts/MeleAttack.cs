using System.Collections;
using UnityEngine;

namespace Scirpts
{
    public abstract class MeleeAttack : MonoBehaviour
    {
        [SerializeField] protected float attackRange = 2f;
        [SerializeField] protected float attackCooldown = 0.5f;

        protected bool CanAttack = true;

        protected abstract void PerformAttack();

        protected IEnumerator ResetAttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}