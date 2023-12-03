using System.Collections;
using UnityEngine;

namespace Scirpts
{
    public abstract class PlayerBaseAttack : MonoBehaviour
    {
        [SerializeField] protected float attackRange = 2f;
        [SerializeField] protected float attackCooldown = 0.5f;

        protected bool CanAttack = true;

        protected abstract void PerformAttack(GameObject target);

        protected IEnumerator ResetAttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
        }
    }
}