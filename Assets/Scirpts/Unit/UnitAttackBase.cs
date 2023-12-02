using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace Scirpts.Enemy
{
    public abstract class UnitAttackBase : MonoBehaviour
    {
        [SerializeField] protected float attackRange = 5f;
        [SerializeField] protected float chaseDistance = 5f;
        [SerializeField] protected float attackCooldown = 0.5f;
        [SerializeField] protected float walkSpeed = 5f;
        
        protected NavMeshAgent Agent;
        protected Animator Animator;
        protected Stats Stats;
        protected bool IsChasing = false;
        protected Vector3 OriginalPosition;
        
        protected bool CanAttack = true;
        protected bool _isAttacking = false;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        protected virtual void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Stats = GetComponent<Stats>();
            Animator = GetComponent<Animator>();
            walkSpeed = Agent.speed;
            OriginalPosition = transform.position;
        }

        protected virtual void Update()
        {
            CheckStatus();
            UpdateAnimatorVariables();
            
            if (_isAttacking)
            {
                Agent.velocity = Vector3.zero;
                Agent.isStopped = true;
            }
            else
            {
                Agent.isStopped = false;
            }
        }

        protected abstract void CheckStatus();

        protected float ReturnDistance(Transform target)
        {
            return (transform.position - target.position).sqrMagnitude;
        }

        protected void FaceTarget(Transform target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        
        void UpdateAnimatorVariables()
        {
            bool isWalking = IsChasing || (Agent.remainingDistance > 0.1f && Agent.destination != OriginalPosition);
            Animator.SetBool(IsWalking, isWalking);
        }

        protected void PerformAttack(GameObject target)
        {
            if (!CanAttack) return;

            CanAttack = false;
            IsChasing = false;
            Stats.TakeDamage(target, Stats.damage);
            Animator.SetTrigger(IsAttack);
            StartCoroutine(ResetAttackCooldown());
        }
        
        protected IEnumerator ResetAttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
        }
    }
}