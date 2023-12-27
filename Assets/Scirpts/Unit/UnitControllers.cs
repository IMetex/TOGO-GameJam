using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Scirpts.Enemy
{
    public abstract class UnitControllers : MonoBehaviour
    {
        [SerializeField] public float attackRange = 5f;
        [SerializeField] public float chaseDistance = 5f;
        [SerializeField] protected float attackCooldown = 0.5f;
        [SerializeField] protected float walkSpeed = 5f;

        public NavMeshAgent agent;
        protected Animator _animation;
        protected StatsManager StatsManager;
        public bool isChasing = false;
        protected Vector3 OriginalPosition;
        private bool CanAttack { get; set; } = true;
        protected bool IsAttacking { get; set; } = false;


        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        protected virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            StatsManager = GetComponent<StatsManager>();
            _animation = GetComponent<Animator>();
            walkSpeed = agent.speed;
            OriginalPosition = transform.position;
        }

        protected virtual void Update()
        {
            CheckStatus();

            if (IsAttacking)
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }

        protected abstract void CheckStatus();

        protected float ReturnDistance(Transform target)
        {
            return Vector3.Distance(transform.position, target.position);
        }

        public void FaceTarget(Transform target)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        protected void PerformAttack(GameObject target)
        {
            if (!CanAttack) return;

            CanAttack = false;
            IsAttacking = true;
            _animation.SetBool(IsWalking, false);
            StatsManager.TakeDamage(target, StatsManager.Damage);
            _animation.SetTrigger(IsAttack);
            StartCoroutine(ResetAttackCooldown());
        }

        protected IEnumerator ResetAttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            CanAttack = true;
            IsAttacking = false;
        }
    }
}