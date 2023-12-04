using System;
using Cinemachine;
using Scirpts.Enemy;
using UnityEngine;

namespace Scirpts.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        public string playerTag = "Player";

        public Animator animator;

        public int distanceRange = 10;
        
        public CinemachineVirtualCamera mainCam;
        public CinemachineVirtualCamera warCam;

        private void Start()
        {
            mainCam.Priority = 1;
        }


        private void Update()
        {
            for (int i = UnitsManager.Instance.enemies.Count - 1; i >= 0; i--)
            {
                var enemy = UnitsManager.Instance.enemies[i];

                float distanceToEnemy = Vector3.Distance(enemy.position, transform.position + Vector3.up);

                if (distanceToEnemy <= distanceRange)
                {
                    
                }
                else
                {
                    
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                animator.Play("WarCamera");
                mainCam.Priority = 0;
                warCam.Priority = 1;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                animator.Play("MainCamera");
                warCam.Priority = 0;
                mainCam.Priority = 1;
            }
        }
    }
}