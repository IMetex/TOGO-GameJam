using System;
using System.Collections.Generic;
using Scirpts.Animation;
using UnityEngine;

namespace Scirpts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private bool _canAttack;
        private float attackCooldown = 0.5f;
        public AnimationController _animationController;
        public float attackRange = 5f;
        public float yOffset;

        private void Update()
        {
            if (CanAttack())
            {
                Attack();
                _animationController.TriggerAnimation("IsAttack");
            }
        }

        private bool CanAttack()
        {
            RaycastHit hit;
            
            Vector3 raycastDirection = transform.forward; // Use the player's forward direction

            Debug.DrawRay(transform.position + transform.up * yOffset, raycastDirection * attackRange, Color.red);

            if (Physics.Raycast(transform.position + transform.up * yOffset, raycastDirection * attackRange, out hit,
                    attackRange))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit: " + hit.collider.gameObject.name);
                    return true;
                }
            }
            else
            {
                Debug.Log("No hit");
            }

            return false;
        }

        private void Attack()
        {
            //_canAttack = false;
            _animationController.TriggerAnimation("IsAttack");
           // Invoke("ResetAttackCooldown", attackCooldown);
        }

        private void ResetAttackCooldown()
        {
            _canAttack = true;
        }
    }
}